using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using System;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using BRCSystem.ClassLibrary.Authentication.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using BRCSystem.ClassLibrary.Data;

namespace BRCSystem.ClassLibrary.Logs
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string? requestBody = null;

            // Captura o corpo da solicitação

            if (context.Request.ContentType != null && (context.Request.ContentType.Contains("application/json") || context.Request.ContentType.Contains("multipart/form-data")))
            {
                if (context.Request.ContentType.Contains("multipart/form-data"))
                {

                    if (context.Request.HasFormContentType)
                    {
                        var form = await context.Request.ReadFormAsync();

                        // Verifique se a solicitação possui um campo específico com o JSON
                        if (form.ContainsKey("product"))
                        {
                            requestBody = form["product"];
                        }
                    }

                }
                else
                {
                    var requestStream = context.Request.Body;
                    using (var memoryStream = new MemoryStream())
                    {
                        await requestStream.CopyToAsync(memoryStream);
                        requestBody = Encoding.UTF8.GetString(memoryStream.ToArray());
                        context.Request.Body = new MemoryStream(memoryStream.ToArray());
                    }
                }
            }


            // Continua o pipeline de solicitação
            await _next(context);

            if (context.Request.Method == "POST" || context.Request.Method == "PUT" || context.Request.Method == "DELETE" || context.Response.StatusCode == 401)
            {

                var loggedUser = context.Items["User"] as User;
                var requestTime = DateTime.UtcNow;

                using (var scope = context.RequestServices.CreateScope())
                {
                    var serviceProvider = scope.ServiceProvider;
                    var dbContext = serviceProvider.GetRequiredService<DataContext>();

                    string clientIp = context.Request.Headers["X-Forwarded-For"];
                    if (string.IsNullOrEmpty(clientIp))
                    {
                        clientIp = context.Connection.RemoteIpAddress.ToString();
                    }

                    if (requestBody != null)
                    {
                        JToken json = JToken.Parse(requestBody);

                        // Verifica se o objeto contém a chave "password ou PasswordHash"
                        if (json is JObject jsonObject)
                        {
                            string[] keysToRemove = { "password", "PasswordHash", "passwordHash", "confirmPasswordHash" };

                            foreach (var key in keysToRemove)
                                if (jsonObject.ContainsKey(key)) jsonObject.Remove(key);

                            requestBody = jsonObject.ToString();
                        }
                    }


                    var logEntry = new Log
                    {
                        UserName = loggedUser != null ? loggedUser.Username : null,
                        RequestTime = requestTime,
                        ClientIp = clientIp,
                        RequestMethod = context.Request.Method,
                        RequestPath = context.Request.Path,
                        RequestBody = requestBody,
                        ResponseStatus = context.Response.StatusCode.ToString()
                    };

                    dbContext.Logs.Add(logEntry);

                    _logger.LogInformation(
                        "Log Entry: UserName={UserName}, RequestTime={RequestTime}, ClientIp={ClientIp}, RequestMethod={RequestMethod}, RequestPath={RequestPath}, RequestBody={RequestBody}, ResponseStatus={ResponseStatus}",
                        logEntry.UserName,
                        logEntry.RequestTime,
                        logEntry.ClientIp,
                        logEntry.RequestMethod,
                        logEntry.RequestPath,
                        logEntry.RequestBody,
                        logEntry.ResponseStatus
                    );

                    try
                    {
                        await dbContext.SaveChangesAsync();

                        if (dbContext.Logs.Any(x => x.RequestTime < DateTime.UtcNow.AddDays(-30)))
                        {
                            dbContext.Logs.RemoveRange(dbContext.Logs.Where(x => x.RequestTime < DateTime.UtcNow.AddDays(-30)).ToList());
                        }

                        try
                        {
                            await dbContext.SaveChangesAsync();

                        }
                        catch (DbUpdateException ex)
                        {
                            _logger.LogError(ex.InnerException, "Error while cleaning logs");
                        }

                    }
                    catch (DbUpdateException ex)
                    {
                        _logger.LogError(ex.InnerException, "Error while saving changes to the database");
                    }

                    
                }
            }
        }
    }

}

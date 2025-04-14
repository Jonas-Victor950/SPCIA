using BRCSystem.ClassLibrary.Data;
using BRCSystem.ClassLibrary.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BRCSystem.ClassLibrary.Authorization;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, DataContext dataContext, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = jwtUtils.ValidateJwtToken(token);
        if (userId != null)
        {
            // attach user to context on successful jwt validation
            context.Items["User"] = dataContext.Users.Find(userId.Value);
        }

        await _next(context);
    }
}
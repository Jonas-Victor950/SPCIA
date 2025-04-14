using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace BRCSystem.ClassLibrary.Helpers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case UnauthorizedException e:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case ForbiddenException e:
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        break;
                    case NotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case NoContentException e:
                        response.StatusCode = (int)HttpStatusCode.NoContent;
                        break;
                    case ConflictException e:
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;
                    case MethodNotAllowed e:
                        response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                        break;
                    case BadGateway e:
                        response.StatusCode = (int)HttpStatusCode.BadGateway;
                        break;
                    case DoneStatusCode e:
                        response.StatusCode = 1001;
                        break;
                    case CancelStatusCode e:
                        response.StatusCode = 1002;
                        break;
                    case UserUnauthorized e:
                        response.StatusCode = 1003;
                        break;
                    case BlockedException e:
                        response.StatusCode = 1004;
                        break;
                    case LotStepDataResetBlockedException e:
                        response.StatusCode = 1005;
                        break;
                    case ProductTypePrefixListException e:
                        response.StatusCode = 1006;
                        break;
                    case ProcessStepTimeInvalid e:
                        response.StatusCode = 1007;
                        break;
                    case PrintLabelNotFoundOrAvailable e:
                        response.StatusCode = 1008;
                        break;
                    case PrintLabelQtyNotFound e:
                        response.StatusCode = 1009;
                        break;
                    case InvalidPassWord e:
                        response.StatusCode = 1010;
                        break;
                    case ResultIsEmpty e:
                        response.StatusCode = 1011;
                        break;
                    case CannotDelete e:
                        response.StatusCode = 1012;
                        break;
                    case CannotUpdate e:
                        response.StatusCode = 1013;
                        break;
                    case ProductDesable e: 
                        response.StatusCode = 1014;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}

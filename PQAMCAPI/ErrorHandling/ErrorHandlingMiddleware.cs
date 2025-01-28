using System.Net;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Authorization;

using API.Classes;

namespace ErrorHandling
{
    [AllowAnonymous]
    public static class ErrorHandlingMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, bool isDevEnvironment)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        if (contextFeature.Error is MyAPIException)
                        {
                            context.Response.ContentType = System.Net.Mime.MediaTypeNames.Text.Plain;
                            context.Response.StatusCode = StatusCodes.Status409Conflict;
                            ErrorDetails ErrDetails = new ErrorDetails()
                            {
                                StatusCode = StatusCodes.Status409Conflict,
                                Message = contextFeature.Error.Message
                            };

                            await context.Response.WriteAsync(ErrDetails.ToString());
                        }
                        else
                        {
                            context.Response.ContentType = "application/json";
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                            ErrorDetails ErrDetails = new ErrorDetails()
                            {
                                StatusCode = context.Response.StatusCode,
                                //Message = contextFeature.Error.Message
                            };

                            //if (contextFeature.Error.InnerException != null)
                            //    ErrDetails.InnerMessage = contextFeature.Error.InnerException.Message;

                            if (isDevEnvironment)
                            {
                                ErrDetails.Message = contextFeature.Error.Message;
                                if (contextFeature.Error.InnerException != null)
                                    ErrDetails.InnerMessage = contextFeature.Error.InnerException.Message;
                            }
                            else
                            {
                                ErrDetails.Message = "Internal Server Error";
                                ErrDetails.InnerMessage = null;
                            }

                            await context.Response.WriteAsync(ErrDetails.ToString());
                        }
                    }
                });
            });
        }
    }
}
using System;
using StackoveflowClone.Exceptions;

namespace StackoveflowClone.Middleweare
{
	public class ErrorHandlingMiddleweare : IMiddleware
	{
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }

            catch (DirectoryNotFoundException notfound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notfound.Message);
            }

            catch (BadRequestException notFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }

            catch (ForbiddenAccessException forbidden)
            {
                context.Response.StatusCode = 403;
            }

            catch (Exception ex)
            {
                //_logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }

        }
    }
}


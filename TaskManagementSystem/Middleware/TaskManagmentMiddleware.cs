using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using System.Net;
using System.Text.Json;
using TaskManagementSystem.Exception;

namespace TaskManagementSystem.Middleware
{
    public class TaskManagmentMiddleware 
    {
        private readonly RequestDelegate _next;
        private static Logger log = LogManager.GetCurrentClassLogger();

        public TaskManagmentMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception error)
            {
                log.Error(error.ToString());
                var response = context.Response;
                response.ContentType = "application/json";
                string result = null;
                switch (error)
                {
                    case TaskManagmentException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        result = JsonSerializer.Serialize(new { message = error?.Message });
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        //I don't want to show internal error
                        result = JsonSerializer.Serialize(new { message = "Please check documentation. " });
                        break;
                }
                await response.WriteAsync(result);
            }
        }
    }
}

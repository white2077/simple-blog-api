using AspNetCoreRestfulApi.Core.CustomException;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreRestfulApi.Core.BaseModel;
using System.Net;
using AspNetCoreRestfulApi.Data;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreRestfulApi.Core.RestExceptionAdvice
{
    public class HttpResponseExceptionFilter(AppDbContext dbContext) : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) {
            var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (dbContext.TokenBlackLists.AnyAsync(x => x.Token == token).Result)
            {
                var responseError = new ResponseError(HttpStatusCode.Unauthorized, "Token is blacklisted");
                context.Result = new ObjectResult(responseError)
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //catch HttpResponseException
            if (context.Exception is HttpResponseException httpResponseException)
            {
                context.Result = new ObjectResult(
                new ResponseError(httpResponseException.StatusCode, httpResponseException.Value))
                {
                    StatusCode = httpResponseException.StatusCode
                };

                context.ExceptionHandled = true;
            }

            //catch all exception fukin 500 =))
            else if (context.Exception is Exception exception)
            {
                context.Result = new ObjectResult(
               new ResponseError(HttpStatusCode.InternalServerError, exception.Message))
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
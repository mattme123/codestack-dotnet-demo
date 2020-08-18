using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace codestackdemo.Extensions.Exceptions
{
    public class ExceptionService
    {
        public static Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return WriteException(context, ex);
        }

        public static Task HandleCustomException(HttpContext context, ExceptionModel ex)
        {
            context.Response.Clear();
            context.Response.ContentType = ex.ContentType;
            context.Response.StatusCode = ex.StatusCode;
            return WriteException(context, ex);
        }

        public static Task WriteException(HttpContext context, dynamic ex)
        {
            return context.Response.WriteAsync(new ErrorDetail
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.InnerException?.Message ?? ex.Message
            }.ToJSONString());
        }
    }
}

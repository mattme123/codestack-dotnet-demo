using System;

namespace codestackdemo.Extensions.Exceptions
{
    public class ExceptionModel : Exception
    {
        public int StatusCode { get; set; }
        public string ContentType { get; set; } = "application/json";
        public ExceptionModel(int statusCode, string message) : base(message) => StatusCode = statusCode;
    }
}

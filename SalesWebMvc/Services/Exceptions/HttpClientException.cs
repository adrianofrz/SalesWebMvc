using System;

namespace SalesWebMvc.Services.Exceptions
{
    public class HttpClientException : ApplicationException
    {
        public HttpClientException(string message) : base(message) { }
    }
}

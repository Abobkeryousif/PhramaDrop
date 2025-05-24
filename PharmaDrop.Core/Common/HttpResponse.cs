using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Core.Common
{
    public class HttpResponse<T> where T : class
    {
        public HttpResponse(HttpStatusCode statusCode , string message)
        {
            StatusCode = (int)statusCode;
            Message = message;
        }

        public HttpResponse(HttpStatusCode statusCode , string message ,T date )
        {
            StatusCode = (int)statusCode;
            Message = message;
            Data = date;
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}

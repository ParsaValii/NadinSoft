using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace NadinSoft.Domain.Exeptions
{
    public class BaseException : Exception
    {
        public string Message;
        public HttpStatusCode StatusCode;

        public BaseException(string message, HttpStatusCode statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }
    }
}
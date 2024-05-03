using System.Net;

namespace NadinSoft.Domain.Exeptions
{
    public class BaseException : Exception
    {
        public string ErrorMessage;
        public HttpStatusCode StatusCode;

        public BaseException(string message, HttpStatusCode statusCode)
        {
            ErrorMessage = message;
            StatusCode = statusCode;
        }
    }
}
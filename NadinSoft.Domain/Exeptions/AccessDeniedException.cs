using System.Net;

namespace NadinSoft.Domain.Exeptions
{
    public class AccessDeniedException : BaseException
    {
        public AccessDeniedException(string message) : base(message, HttpStatusCode.Forbidden) { }
    }
}
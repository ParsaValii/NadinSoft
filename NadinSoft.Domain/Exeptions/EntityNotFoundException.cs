using System.Net;

namespace NadinSoft.Domain.Exeptions
{
    public class EntityNotFoundException : BaseException
    {
        public EntityNotFoundException(string message) : base(message, HttpStatusCode.NotFound) { }
    }
}
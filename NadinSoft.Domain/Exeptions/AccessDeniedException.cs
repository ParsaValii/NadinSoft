using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NadinSoft.Domain.Exeptions
{
    public class AccessDeniedException : BaseException
    {
        public AccessDeniedException(string message) : base(message, HttpStatusCode.Forbidden) { }
    }
}
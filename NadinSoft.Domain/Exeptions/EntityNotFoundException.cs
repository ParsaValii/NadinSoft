using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NadinSoft.Domain.Exeptions
{
    public class EntityNotFoundException : BaseException
    {
        public EntityNotFoundException(string message) : base(message, HttpStatusCode.NotFound) { }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NadinSoft.Domain.Dtos
{
    public class LoginUserDto
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
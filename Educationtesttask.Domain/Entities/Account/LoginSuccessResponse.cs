using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educationtesttask.Domain.Entities.Account
{
    public class LoginSuccessResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}

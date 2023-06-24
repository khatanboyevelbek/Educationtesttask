using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educationtesttask.Application.Interfaces
{
    public interface ISecurityPassword
    {
        string Encrypt(string password);
        bool Verify(string password, string passwordHash);
    }
}

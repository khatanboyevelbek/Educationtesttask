using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Teachers
{
    public class InvalidTeacherException : Xeption
    {
        public InvalidTeacherException()
            : base(message: "Teacher is invalid")
        { }
    }
}

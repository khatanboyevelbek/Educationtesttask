using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Teachers
{
    public class AlreadyExistTeacherException : Xeption
    {
        public AlreadyExistTeacherException()
            : base(message: "Teacher is alreay exist. Please try again")
        { }
    }
}

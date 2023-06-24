using Educationtesttask.Domain.Entities;

namespace Educationtesttask.Application.Interfaces
{
    public interface IAuthManager
    {
        string GenerateToken(Student student);
        string GenerateToken(Teacher teacher);
    }
}

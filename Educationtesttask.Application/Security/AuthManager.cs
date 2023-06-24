using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml;
using Educationtesttask.Application.Interfaces;
using Educationtesttask.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Educationtesttask.Application.Security
{
    public class AuthManager : IAuthManager
    {
        private readonly IConfiguration configuration;

        public AuthManager(IConfiguration configuration) =>
			this.configuration = configuration;

		public string GenerateToken(Student student)
        {
            var securityKey = 
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:ValidIssuer"]));

            var cridentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, student.Id.ToString()),
                new Claim(ClaimTypes.Email, student.Email),
                new Claim(ClaimTypes.Role, student.Role.ToString())
			};

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:ValidIssuer"],
                audience: configuration["Jwt:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: cridentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
		}

        public string GenerateToken(Teacher teacher)
        {
			var securityKey =
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:ValidIssuer"]));

			var cridentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, teacher.Id.ToString()),
				new Claim(ClaimTypes.Email, teacher.Email),
				new Claim(ClaimTypes.Role, teacher.Role.ToString())
			};

			var token = new JwtSecurityToken(
				issuer: configuration["Jwt:ValidIssuer"],
				audience: configuration["Jwt:ValidAudience"],
				claims: claims,
				expires: DateTime.Now.AddHours(5),
				signingCredentials: cridentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}

using Microsoft.Extensions.Configuration;
using PublishingCompany.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PublishingCompany.Domain;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Services
{
    public class UserService
    {
        private ApplicationContext ApplicationContext { get; set; }
        private IConfiguration Configuration { get; }

        public UserService(ApplicationContext applicationContext, IConfiguration configuration)
        {
            this.ApplicationContext = applicationContext;
            this.Configuration = configuration;
        }

        public string Login(string email, string password)
        {
            var user = ApplicationContext.UserDbSet.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
            if (user == null)
                return null;
            return CreateToken(user);
        }

        private string CreateToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(this.Configuration["Token:Secret"]);

            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Email));

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Audience = "PUBLISHINGCOMPANY-API",
                Issuer = "PUBLISHINGCOMPANY-API"
            };

            var secutiryToken = tokenHandler.CreateToken(tokenDescription);

            var token = tokenHandler.WriteToken(secutiryToken);

            return token;
        }
    }
}

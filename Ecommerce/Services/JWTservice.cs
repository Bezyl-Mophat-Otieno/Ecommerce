using Ecommerce.Models;
using Ecommerce.Services.Iservices;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Services
{
    public class JWTservice : IJwt
    {
        private readonly IConfiguration _config;

        public JWTservice(IConfiguration config)
        {

            _config = config;

            
        }
        public string GetToken(User user)
        {
            var SecretKey = _config.GetSection("JWToptions:SecretKey").Value;
            var Issuer = _config.GetSection("JWToptions:Issuer").Value;
            var Audience = _config.GetSection("JWToptions:Audience").Value;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // Adding the payload 

            List<Claim> Claims = new List<Claim>();
            Claims.Add(new Claim("Role", user.Role));
            Claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            Claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            Claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.Name));

            // Combining everything together [credentials , claims , Issuer and Audience]

            var tokendescriptor = new SecurityTokenDescriptor()
            {
                Issuer = Issuer,
                Audience = Audience,
                SigningCredentials = cred,
                Expires = DateTime.UtcNow.AddHours(3),
                Subject = new ClaimsIdentity(Claims)

            };

            var token = new JwtSecurityTokenHandler().CreateToken(tokendescriptor);

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}

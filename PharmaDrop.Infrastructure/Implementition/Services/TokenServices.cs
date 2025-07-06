using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PharmaDrop.Application.Contract.Services;
using PharmaDrop.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PharmaDrop.Infrastructure.Implementition.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _configuration;
        public TokenServices(IConfiguration configuration)=>
        _configuration = configuration;
        
        public string JwtBearerToken(User user)
        {
            var claim = new List<Claim>();
            claim.Add(new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()));
            claim.Add(new Claim(ClaimTypes.Name , user.UserName));
            claim.Add(new Claim(ClaimTypes.Email, user.Email));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
            var cerdintial = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                _configuration["Token:Issure"],
                _configuration["Token:Audince"],
                claim,
                signingCredentials:cerdintial,
                expires : DateTime.UtcNow.AddMinutes(59)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }

        public RefreshToken RefreshToken()
        {
            var random = new byte[64];
            using var Generator = new RNGCryptoServiceProvider();
            Generator.GetBytes(random);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(random),
                CreatedOn = DateTime.UtcNow,
                ExpierOn = DateTime.UtcNow.AddDays(1),
            };
        }
    }
}

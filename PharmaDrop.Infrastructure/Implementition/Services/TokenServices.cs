using PharmaDrop.Application.Contract.Services;
using PharmaDrop.Core.Entities;

namespace PharmaDrop.Infrastructure.Implementition.Services
{
    public class TokenServices : ITokenServices
    {
        public string JwtBearerToken(User user)
        {
            throw new NotImplementedException();
        }

        public RefreshToken RefreshToken()
        {
            throw new NotImplementedException();
        }
    }
}

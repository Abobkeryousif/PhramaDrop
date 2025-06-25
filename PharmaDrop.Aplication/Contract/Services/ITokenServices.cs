using PharmaDrop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Application.Contract.Services
{
    public interface ITokenServices
    {
        public string JwtBearerToken(User user);
        public RefreshToken RefreshToken();
    }
}

using PharmaDrop.Application.Contract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Aplication.Contract.Interfaces
{
    public interface IUnitofWork 
    {
        public IUserRepository UserRepository { get; }
        public IOtpRepository otpRepository { get; }

        public ICategoryRepository categoryRepository { get; }
    }
}

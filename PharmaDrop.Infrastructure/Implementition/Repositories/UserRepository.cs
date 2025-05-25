using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Core.Entities;
using PharmaDrop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Infrastructure.Implementition.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

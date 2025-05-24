using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Infrastructure.Implementition
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext _context;
        public UnitofWork(ApplicationDbContext context)
        {
            _context = context;
            UserRepository = new UserRepository(context);
        }
         public IUserRepository UserRepository { get; }
    }

   
}

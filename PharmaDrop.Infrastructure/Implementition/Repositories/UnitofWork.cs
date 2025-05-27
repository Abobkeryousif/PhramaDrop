using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Application.Contract.Interfaces;
using PharmaDrop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Infrastructure.Implementition.Repositories
{
    public class UnitofWork : IUnitofWork
        
    {
        private readonly ApplicationDbContext _context;
        public UnitofWork(ApplicationDbContext context)
        {
            _context = context;
            UserRepository = new UserRepository(context);
            otpRepository = new OtpRepository(context);
            categoryRepository = new CategoryRepository(context);
        }
         public IUserRepository UserRepository { get; }

        public IOtpRepository otpRepository {  get; }

        public ICategoryRepository categoryRepository {  get; }
    }

   
}

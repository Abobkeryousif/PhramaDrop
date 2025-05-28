using AutoMapper;
using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Application.Contract.Interfaces;
using PharmaDrop.Application.Contract.Services;
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
        private readonly IMapper _mapper;
        private readonly IImageServices _imageServices;

        public UnitofWork(ApplicationDbContext context, IMapper mapper, IImageServices imageServices)
        {
            _context = context;
            UserRepository = new UserRepository(context);
            otpRepository = new OtpRepository(context);
            categoryRepository = new CategoryRepository(context);
            productRepository = new ProductRepostory(context,mapper,imageServices);
            _mapper = mapper;
            _imageServices = imageServices;
        }
        public IUserRepository UserRepository { get; }

        public IOtpRepository otpRepository {  get; }

        public ICategoryRepository categoryRepository {  get; }

        public IProductRepository productRepository { get; }
    }

   
}

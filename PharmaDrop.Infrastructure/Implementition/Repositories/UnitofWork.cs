using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.Json;
using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Application.Contract.Interfaces;
using PharmaDrop.Application.Contract.Services;
using PharmaDrop.Infrastructure.Data;
using PharmaDrop.Infrastructure.Implementition.Services;
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
        private readonly IQRcodeServices _qRcodeServices;
        

        public UnitofWork(ApplicationDbContext context, IMapper mapper, IImageServices imageServices, IQRcodeServices qRcodeServices)
        {
            _context = context;
            UserRepository = new UserRepository(context);
            otpRepository = new OtpRepository(context);
            categoryRepository = new CategoryRepository(context);
            productRepository = new ProductRepostory(context, mapper, imageServices, qRcodeServices);
            refreshTokenRepository = new RefreshTokenRepository(context);
            _mapper = mapper;
            _imageServices = imageServices;
            _qRcodeServices = qRcodeServices;
            

        }
        public IUserRepository UserRepository { get; }

        public IOtpRepository otpRepository { get; }

        public ICategoryRepository categoryRepository { get; }

        public IProductRepository productRepository { get; }

        public IRefreshTokenRepository refreshTokenRepository {  get; }
        }

    }


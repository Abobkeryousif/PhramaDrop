﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PharmaDrop.Application.Contract.Interfaces;
using PharmaDrop.Application.Contract.Services;
using PharmaDrop.Application.DTOs;
using PharmaDrop.Core.Common;
using PharmaDrop.Core.Entities;
using PharmaDrop.Infrastructure.Data;
using PharmaDrop.Infrastructure.Implementition.Services;

namespace PharmaDrop.Infrastructure.Implementition.Repositories
{
    public class ProductRepostory : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IImageServices _imageServices;
        private readonly IQRcodeServices _qRcodeServices;
        public ProductRepostory(ApplicationDbContext context, IMapper mapper, IImageServices imageServices, IQRcodeServices qRcodeServices) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            _imageServices = imageServices;
            _qRcodeServices = qRcodeServices;
        }

        public async Task<bool> CreateAsync(ProductDto productDto)
        {
            if (productDto == null) return false;
            var product = mapper.Map<Product>(productDto);
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            var ImagePath = await _imageServices.AddImageAsync(productDto.Photo,productDto.Name);

            var photo = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = product.Id
            }).ToList();

            await context.AddRangeAsync(photo);
            await context.SaveChangesAsync();

            ProductDetailsQRcode details = new ProductDetailsQRcode
            {
                Name = product.Name,
                Price = product.NewPrice,
                Manufacturer = product.Manufacturer,
            };

            var QRcode = _qRcodeServices.GenerateQRcode(details);
            var result = ConvertBytesToBase64(QRcode);

            var Qr = new QRcode
            {
                ProductQRcode = result,
                ProductId = product.Id,
            };


            context.Add(Qr);
            context.SaveChanges();

            return true;
        }

        public async Task<List<Product>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 20)
        {
            //filter
            var product =  context.Products.AsQueryable();
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    product = product.Where(n=> n.Name.Contains(filterQuery));
                }
            }

            //sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase)) 
                {
                    product = isAscending ? product.OrderBy(n=> n.Name) : product.OrderByDescending(n=> n.Name);
                }

                else if (sortBy.Equals("NewPrice", StringComparison.OrdinalIgnoreCase)) 
                {
                    product = isAscending ? product.OrderBy(np => np.NewPrice) : product.OrderByDescending(np => np.NewPrice);
                }
            }

            //Pagenation
            var skipProduct = (pageNumber - 1) * pageSize;
             

            return await product.Skip(skipProduct).Take(pageSize).ToListAsync();
        }

        public async Task<bool> UpdateAsync(UpdateProductDto updateProductDto)
        {
            if (updateProductDto == null) return false;

            var product = await context.Products.Include(c=> c.Category).FirstOrDefaultAsync(p=> p.Id == updateProductDto.Id);
            if (product == null) return false;

            mapper.Map(updateProductDto, product);  
            var FindPhoto = await context.Photos.Where(p=> p.ProductId == updateProductDto.Id).ToListAsync();

            foreach(var item in FindPhoto) 
            {
                _imageServices.DeleteImageAsync(item.ImageName);
            }
            context.Photos.RemoveRange(FindPhoto);

            var imagePath = await _imageServices.AddImageAsync(updateProductDto.Photo , updateProductDto.Name);
            var photo = imagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = updateProductDto.Id
            }).ToList();
                
            await context.AddRangeAsync(photo);
            await context.SaveChangesAsync();
            return true;
        }

        private string ConvertBytesToBase64(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return string.Empty;

            return Convert.ToBase64String(byteArray);
        }

        private object? GetPropertyValue(Product product, string propertyName)
        {
            return typeof(Product).GetProperty(propertyName)?.GetValue(product, null);
        }
    }
}









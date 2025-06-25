using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Application.DTOs;
using PharmaDrop.Core.Common;
using PharmaDrop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Application.Contract.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<bool> CreateAsync(ProductDto productDto);
        Task<bool> UpdateAsync(UpdateProductDto updateProductDto);
        Task<List<Product>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, 
            int pageNumber = 1, int pageSize = 20);
    }
}

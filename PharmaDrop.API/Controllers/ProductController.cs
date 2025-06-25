using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Application.Contract.Interfaces;
using PharmaDrop.Application.DTOs;
using PharmaDrop.Application.Feature.Command.Products;
using PharmaDrop.Application.Feature.Query.Products;
using PharmaDrop.Core.Common;
using PharmaDrop.Core.Entities;

namespace PharmaDrop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IUnitofWork _unitofWork;
        public ProductController(ISender sender, IUnitofWork unitofWork)
        {
            _sender = sender;
            _unitofWork = unitofWork;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CraeteAsync(ProductDto productDto) =>
            Ok(await _sender.Send(new CreateProductCommand(productDto)));

        [HttpGet]
        
        public async Task<IActionResult> GetAllAsync(string? filterOn , string? filterQuery,string? sortBy, bool? isAscending,
            int pageNumber=1,int pageSize=20) 
        {
            var result = await _unitofWork.productRepository.GetAllAsync(filterOn, filterQuery,sortBy,isAscending ?? true,pageNumber,pageSize);
            return Ok(result);
        } 

        [HttpGet("Get-By-Name")]
        public async Task<IActionResult> GetByNameProductAsync(GetByNameProductDto productDto) =>
            Ok(await _sender.Send(new GetByNameProductQuery(productDto)));

        [HttpGet("Get-By-Id")]
        public async Task<IActionResult> GetByIdProductAsync(int Id) =>
            Ok(await _sender.Send(new GetByIdProductQuery(Id)));

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync(UpdateProductDto UpdateproductDto) =>
            Ok(await _sender.Send(new UpdateProductCommand(UpdateproductDto)));

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync(int Id) =>
            Ok(await _sender.Send(new DeleteProductCommand(Id)));






    }
}

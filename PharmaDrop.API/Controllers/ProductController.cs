using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaDrop.Application.DTOs;
using PharmaDrop.Application.Feature.Command.Products;
using PharmaDrop.Application.Feature.Query.Products;
using PharmaDrop.Core.Common;

namespace PharmaDrop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductController(ISender sender)=>
        _sender = sender;

        [HttpPost("Create")]
        public async Task<IActionResult> CraeteAsync(ProductDto productDto) =>
            Ok(await _sender.Send(new CreateProductCommand(productDto)));

        [HttpGet]
        public async Task<IActionResult> GetProductsAllAsync([FromQuery]QueryPramater pramater) =>
            Ok(await _sender.Send(new GetProductsQuery(pramater)));

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

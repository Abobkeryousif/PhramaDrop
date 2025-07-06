using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaDrop.Application.Contract.Services;
using PharmaDrop.Application.DTOs;
using PharmaDrop.Application.Feature.Command.Categories;
using PharmaDrop.Application.Feature.Command.Categorys;
using PharmaDrop.Application.Feature.Query.Categories;
using PharmaDrop.Application.Feature.Query.Categories.PharmaDrop.Application.Feature.Query.Categories;

namespace PharmaDrop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ISender _sender;

        public CategoryController(ISender sender)=>
            _sender = sender;

        
        [HttpPost("Create")]
        public async Task<IActionResult> CreateCategoryAsync(CategoryDto categoryDto) =>
            Ok(await _sender.Send(new CreateCategoryCommand(categoryDto)));

        [HttpGet]
        public async Task<IActionResult> GetAllCategoryAsync() =>
            Ok(await _sender.Send(new GetCategoriesQuery()));

        [HttpGet("Get-By-Id")]
        public async Task<IActionResult> GetByIdCategoryAsync(int Id) 
        {
            return Ok(await _sender.Send(new GetByIdCategoryQuery(Id)));
        }

        [HttpPut("Update")]

        public async Task<IActionResult> UpdateCategoryAsync(int Id, CategoryDto categoryDto) =>
            Ok(await _sender.Send(new UpdateCategoryCommand(Id,categoryDto)));

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCategoryAsync(int Id) =>
            Ok(await _sender.Send(new DeleteCategoryCommand(Id)));
 }
}

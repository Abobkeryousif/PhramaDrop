using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaDrop.Aplication.DTOs;
using PharmaDrop.Aplication.Feature.Command.Users;
using PharmaDrop.Application.Feature.Command.Users;
using PharmaDrop.Application.Feature.Query.Users;

namespace PharmaDrop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;
        public UserController(ISender sender)=>
        _sender = sender;

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(UserDto userDto) =>
        Ok(await _sender.Send(new RegisterUserCommand(userDto)));

        [HttpGet]
        public async Task<IActionResult> GetAllUser() =>
            Ok(await _sender.Send(new GetUsersQuery()));

        [HttpGet("Get-By-Id")]
        public async Task<IActionResult> GetByIdAsync(int Id) =>
            Ok(await _sender.Send(new GetByIdUserQuery(Id)));

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync(int Id) =>
            Ok(await _sender.Send(new DeleteUserCommand(Id)));

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync(int Id, UpdateUserDto updateUserDto) =>
            Ok(await _sender.Send(new UpdateUserCommand(Id,updateUserDto)));

            
    }

}

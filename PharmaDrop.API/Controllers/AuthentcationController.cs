using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaDrop.Aplication.DTOs;
using PharmaDrop.Application.Feature.Command.Authentication;

namespace PharmaDrop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentcationController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthentcationController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginUserDto loginUserDto) =>
          Ok(await _sender.Send(new LoginCommand(loginUserDto)));

    }
}

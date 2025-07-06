using MediatR;
using Microsoft.Extensions.Logging;
using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Aplication.DTOs;
using PharmaDrop.Application.Contract.Interfaces;
using PharmaDrop.Application.Contract.Services;
using PharmaDrop.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Application.Feature.Command.Authentication
{
    public record LoginCommand(LoginUserDto LoginUserDto) : IRequest<HttpResponse<AuthenticationModel>>;

    public class LoginCommandHandler : IRequestHandler<LoginCommand, HttpResponse<AuthenticationModel>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ITokenServices _tokenServices;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public LoginCommandHandler(IUnitofWork unitofWork, ITokenServices tokenServices, IRefreshTokenRepository refreshTokenRepository)
        {
            _unitofWork = unitofWork;
            _tokenServices = tokenServices;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<HttpResponse<AuthenticationModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var userLog = await _unitofWork.UserRepository.FirstOrDefaultAsync(u=> u.Email == request.LoginUserDto.Email);
            if (userLog == null) 
            {
                return new HttpResponse<AuthenticationModel>(HttpStatusCode.NotFound, "Email or Password Is Invalid");
            }

            var checkPassword = BCrypt.Net.BCrypt.Verify(request.LoginUserDto.Password , userLog.Password);
            if (!checkPassword)
            {
                return new HttpResponse<AuthenticationModel>(HttpStatusCode.NotFound, "Email or Password Is Invalid");
            }

            if (userLog.IsActive == false) 
            { 
                return new HttpResponse<AuthenticationModel>(HttpStatusCode.BadRequest, "Plaese Compelet Rigster To Login");
            }

            var accessToken = _tokenServices.JwtBearerToken(userLog);

            //var userRefreshToken = await _refreshTokenRepository.FirstOrDefaultAsync(t => t.userId == userLog.Id, o => o.OrderByDescending(m => m.CreatedOn));

            //if (userRefreshToken.IsActive)
            //{
            //    return new HttpResponse<AuthenticationModel>(HttpStatusCode.OK, $"Welcome MR: {userLog.UserName}", new AuthenticationModel
            //    {
            //        AccessToken = accessToken,
            //        RefreshToken = userRefreshToken.Token,
            //        ExpierOn = userRefreshToken.ExpierOn,
            //    });
            //}
            var refreshToken = _tokenServices.RefreshToken();
            //refreshToken.userId = userLog.Id;

            //await _refreshTokenRepository.CreateAsync(refreshToken);
            return new HttpResponse<AuthenticationModel>(HttpStatusCode.OK, $"Seccuss Login MR:{userLog.UserName}", new AuthenticationModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpierOn = refreshToken.ExpierOn,
            });
        }
    }
}

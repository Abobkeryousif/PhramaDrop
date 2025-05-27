using AutoMapper;
using MediatR;
using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Aplication.DTOs;
using PharmaDrop.Application.Contract.Interfaces;
using PharmaDrop.Application.Contract.Services;
using PharmaDrop.Core.Common;
using PharmaDrop.Core.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PharmaDrop.Aplication.Feature.Command.Users
{
    public record RegisterUserCommand(UserDto userDto) : IRequest<HttpResponse<UserDto>>;
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, HttpResponse<UserDto>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly ISendEmailServices _sendEmail;
 
        
        public RegisterUserCommandHandler(IUnitofWork unitofWork, IMapper mapper, ISendEmailServices sendEmail)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
            _sendEmail = sendEmail;
    
        }

        public async Task<HttpResponse<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var isExist = await _unitofWork.UserRepository.IsExist(e=> e.Email == request.userDto.Email);
            if (isExist)
                return new HttpResponse<UserDto>(HttpStatusCode.BadRequest,"This User Already Added");

            var user = _mapper.Map<User>(request.userDto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _unitofWork.UserRepository.CreateAsync(user);

            Random random = new Random();
            int otp = random.Next(0, 99999);

            var confiermOTP = new OTP
            {
                Id = Guid.NewGuid(),
                IsUsed = false,
                UserEmail = user.Email,
                ExpirationOn = DateTime.Now.AddMinutes(5),
                otp = otp.ToString("00000"),
            };
            
            _sendEmail.SendEmail(user.Email , "Confierm Account", $"OTP Code: {confiermOTP.otp}");
            await _unitofWork.otpRepository.CreateAsync(confiermOTP);


            return new HttpResponse<UserDto>(HttpStatusCode.OK,$"We Send OTP In Your Email Plaese Confierm It {user.UserName}",request.userDto);

        }
    }
}

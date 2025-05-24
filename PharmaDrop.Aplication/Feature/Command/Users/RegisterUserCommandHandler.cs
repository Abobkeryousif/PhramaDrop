using AutoMapper;
using MediatR;
using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Aplication.DTOs;
using PharmaDrop.Core.Common;
using PharmaDrop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Aplication.Feature.Command.Users
{
    public record RegisterUserCommand(UserDto userDto) : IRequest<HttpResponse<UserDto>>;
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, HttpResponse<UserDto>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        public RegisterUserCommandHandler(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }

        public async Task<HttpResponse<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var isExist = await _unitofWork.UserRepository.IsExist(e=> e.Email == request.userDto.Email);
            if (isExist)
                return new HttpResponse<UserDto>(HttpStatusCode.BadRequest,"This User Already Added");

            var user = _mapper.Map<User>(request.userDto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _unitofWork.UserRepository.CreateAsync(user);

            return new HttpResponse<UserDto>(HttpStatusCode.OK,$"Seccuss Add User {user.UserName}",request.userDto);

        }
    }
}

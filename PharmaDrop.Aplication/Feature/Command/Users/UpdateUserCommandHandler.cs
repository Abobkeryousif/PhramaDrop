using AutoMapper;
using BCrypt.Net;
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

namespace PharmaDrop.Application.Feature.Command.Users
{
    public record UpdateUserCommand(int Id , UpdateUserDto updateUser) : IRequest<HttpResponse<User>>;
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, HttpResponse<User>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        public UpdateUserCommandHandler(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }

        public async Task<HttpResponse<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitofWork.UserRepository.FirstOrDefaultAsync(u=> u.Id == request.Id);
            if (user == null)
                return new HttpResponse<User>(HttpStatusCode.NotFound,$"Not Found With ID: {request.Id}");

            user = _mapper.Map<User>(request.updateUser);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _unitofWork.UserRepository.UpdateAsync(user);

            return new HttpResponse<User>(HttpStatusCode.OK,"Seccuss Update Opratoin",user);
        }
    }
}

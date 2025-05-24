using MediatR;
using PharmaDrop.Aplication.Contract.Interfaces;
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
    public record DeleteUserCommand(int Id) : IRequest<HttpResponse<string>>;
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, HttpResponse<string>>
    {
        private readonly IUnitofWork _unitofWork;
        public DeleteUserCommandHandler(IUnitofWork unitofWork)=>
            _unitofWork = unitofWork;
        
        public async Task<HttpResponse<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitofWork.UserRepository.FirstOrDefaultAsync(u=> u.Id == request.Id);
            if (user == null)
                return new HttpResponse<string>(HttpStatusCode.NotFound,$"Not Found With ID: {request.Id}");

            await _unitofWork.UserRepository.DeleteAsync(user);
            return new HttpResponse<string>(HttpStatusCode.OK,"Seccuss Delete Opration",user.UserName);
        }
    }
}

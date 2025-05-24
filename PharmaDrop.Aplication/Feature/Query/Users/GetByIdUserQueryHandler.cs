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

namespace PharmaDrop.Application.Feature.Query.Users
{
    public record GetByIdUserQuery(int Id) : IRequest<HttpResponse<GetUserDto>>;
    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, HttpResponse<GetUserDto>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        public GetByIdUserQueryHandler(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }

        public async Task<HttpResponse<GetUserDto>> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitofWork.UserRepository.FirstOrDefaultAsync(u=> u.Id == request.Id);
            if (user == null)
                return new HttpResponse<GetUserDto>(HttpStatusCode.NotFound,$"Not Found With ID: {request.Id}");

            var UserDto = _mapper.Map<GetUserDto>(user);
            return new HttpResponse<GetUserDto>(HttpStatusCode.OK,"Seccuss Opration",UserDto);
        }
    }
}

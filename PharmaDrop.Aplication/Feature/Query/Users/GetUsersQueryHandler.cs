using AutoMapper;
using MediatR;
using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Aplication.DTOs;
using PharmaDrop.Core.Common;

using System.Net;


namespace PharmaDrop.Application.Feature.Query.Users
{
    public record GetUsersQuery : IRequest<HttpResponse<List<GetUserDto>>>;
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, HttpResponse<List<GetUserDto>>>
    {
        private readonly IUnitofWork _unitofWork;


        public GetUsersQueryHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;

        }
        public async Task<HttpResponse<List<GetUserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitofWork.UserRepository.GetAllAsync(u=> u.IsActive);
            if (users.Count == 0)
                return new HttpResponse<List<GetUserDto>>(HttpStatusCode.NotFound,"Not Found Any User");

            var UserDto = users.Select(u=> new GetUserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Phone = u.Phone,
                BirthDay = u.BirthDay,
                IsActive = u.IsActive               
            }).ToList();
            

            return new HttpResponse<List<GetUserDto>>(HttpStatusCode.OK,"Seccuss Opration",UserDto);
        }
    }
}

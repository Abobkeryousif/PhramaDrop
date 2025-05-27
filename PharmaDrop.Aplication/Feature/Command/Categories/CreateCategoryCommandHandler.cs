using MediatR;
using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Application.DTOs;
using PharmaDrop.Core.Common;
using PharmaDrop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Application.Feature.Command.Categorys
{
    public record CreateCategoryCommand(CategoryDto categoryDto) : IRequest<HttpResponse<Category>>;
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, HttpResponse<Category>>
    {
        private readonly IUnitofWork _unitofWork;

        public CreateCategoryCommandHandler(IUnitofWork unitofWork)=>
        _unitofWork = unitofWork;
        

        public async Task<HttpResponse<Category>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var IsExist = await _unitofWork.categoryRepository.IsExist(_=> _.Name.ToLower() == request.categoryDto.Name.ToLower());
            if (IsExist)
                return new HttpResponse<Category>(HttpStatusCode.BadRequest,"This Category Already Added! ");

            var catgory = new Category { Name = request.categoryDto.Name };
            await _unitofWork.categoryRepository.CreateAsync(catgory);

            return new HttpResponse<Category>(HttpStatusCode.OK,"Seccuss Opration",catgory);
        }
    }
}

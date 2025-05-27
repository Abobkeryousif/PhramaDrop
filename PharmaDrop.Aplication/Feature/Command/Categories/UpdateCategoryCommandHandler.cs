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

namespace PharmaDrop.Application.Feature.Command.Categories
{
    public record UpdateCategoryCommand(int Id , CategoryDto categoryDto) : IRequest<HttpResponse<Category>>;
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, HttpResponse<Category>>
    {
        private readonly IUnitofWork _unitofWork;

        public UpdateCategoryCommandHandler(IUnitofWork unitofWork) =>
            _unitofWork = unitofWork;
        public async Task<HttpResponse<Category>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitofWork.categoryRepository.FirstOrDefaultAsync(c=> c.Id == request.Id);
            if (category == null)
                return new HttpResponse<Category>(HttpStatusCode.NotFound,$"Not Found With ID: {request.Id}");

            category.Name = request.categoryDto.Name;
            await _unitofWork.categoryRepository.UpdateAsync(category);

            return new HttpResponse<Category>(HttpStatusCode.OK,"Seccuss Update Opration",category);
        }
    }
}

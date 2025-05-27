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

namespace PharmaDrop.Application.Feature.Command.Categories
{
    public record DeleteCategoryCommand(int Id) : IRequest<HttpResponse<Category>>;
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, HttpResponse<Category>>
    {
        private readonly IUnitofWork _unitofWork;

        public DeleteCategoryCommandHandler(IUnitofWork unitofWork)=>
        _unitofWork = unitofWork;
        public async Task<HttpResponse<Category>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitofWork.categoryRepository.FirstOrDefaultAsync(c=> c.Id == request.Id);
            if (category == null)
                return new HttpResponse<Category>(HttpStatusCode.NotFound,$"Not With ID: {request.Id}");

            await _unitofWork.categoryRepository.DeleteAsync(category);

            return new HttpResponse<Category>(HttpStatusCode.OK,"Seccuss Delete Opration",category);
        }
    }
}

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

namespace PharmaDrop.Application.Feature.Query.Categories
{
    public record GetByIdCategoryQuery(int Id) : IRequest<HttpResponse<Category>>;
    public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQuery, HttpResponse<Category>>
    {
        private readonly IUnitofWork _unitofWork;

        public GetByIdCategoryQueryHandler(IUnitofWork unitofWork)=>
        _unitofWork = unitofWork;
        public async Task<HttpResponse<Category>> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _unitofWork.categoryRepository.FirstOrDefaultAsync(c=> c.Id == request.Id);
            if (category == null)
                return new HttpResponse<Category>(HttpStatusCode.NotFound,$"Not Found With ID: {request.Id}");

            return new HttpResponse<Category>(HttpStatusCode.OK,"Seccuss Opration",category);
        }
    }
}

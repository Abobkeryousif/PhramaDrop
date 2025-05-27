using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public record GetCategoriesQuery : IRequest<HttpResponse<List<Category>>>;
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, HttpResponse<List<Category>>>
    {
        private readonly IUnitofWork _unitofWork;
        public GetCategoriesQueryHandler(IUnitofWork unitofWork)=>
        _unitofWork = unitofWork;
        
        public async Task<HttpResponse<List<Category>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var category = await _unitofWork.categoryRepository.GetAllAsync();
            if (category.Count == 0)
                return new HttpResponse<List<Category>>(HttpStatusCode.NotFound,"Not Found Any Category");

            return new HttpResponse<List<Category>>(HttpStatusCode.OK,"Seccuss Opration",category);

        }
    }
}

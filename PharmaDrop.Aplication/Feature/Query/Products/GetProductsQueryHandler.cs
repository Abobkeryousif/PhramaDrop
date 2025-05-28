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

namespace PharmaDrop.Application.Feature.Query.Products
{
    public record GetProductsQuery : IRequest<HttpResponse<List<Product>>>;
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, HttpResponse<List<Product>>>
    {
        private readonly IUnitofWork _unitofWork;
        public GetProductsQueryHandler(IUnitofWork unitofWork)=>
        _unitofWork = unitofWork;
        
        public async Task<HttpResponse<List<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitofWork.productRepository.GetAllAsync();
            if (product.Count == 0)
                return new HttpResponse<List<Product>>(HttpStatusCode.NotFound,"Not Found Any Product");

            return new HttpResponse<List<Product>>(HttpStatusCode.OK,"Seccuss Opration",product);
        }
    }
}

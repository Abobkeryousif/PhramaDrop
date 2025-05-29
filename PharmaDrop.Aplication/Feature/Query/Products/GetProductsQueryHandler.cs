using MediatR;
using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Core.Common;
using PharmaDrop.Core.Entities;
using System.Net;


namespace PharmaDrop.Application.Feature.Query.Products
{
    public record GetProductsQuery(QueryPramater Pramater) : IRequest<HttpResponse<List<Product>>>;
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, HttpResponse<List<Product>>>
    {
        private readonly IUnitofWork _unitofWork;
        public GetProductsQueryHandler(IUnitofWork unitofWork)=>
        _unitofWork = unitofWork;
        
        public async Task<HttpResponse<List<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitofWork.productRepository.GetAllAsync(request.Pramater);
            if (product.Count == 0)
                return new HttpResponse<List<Product>>(HttpStatusCode.NotFound,"Not Found Any Product");

            return new HttpResponse<List<Product>>(HttpStatusCode.OK,"Seccuss Opration",product);
        }
    }
}

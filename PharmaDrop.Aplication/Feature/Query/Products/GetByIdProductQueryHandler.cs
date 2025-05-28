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
    public record GetByIdProductQuery(int Id) : IRequest<HttpResponse<Product>>;
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, HttpResponse<Product>>
    {
        private readonly IUnitofWork _unitofWork;
        public GetByIdProductQueryHandler(IUnitofWork unitofWork)=>
        _unitofWork = unitofWork;
        
        public async Task<HttpResponse<Product>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitofWork.productRepository.FirstOrDefaultAsync(p=> p.Id == request.Id);
            if (product == null)
                return new HttpResponse<Product>(HttpStatusCode.NotFound,$"Not Found With ID: {request.Id}");

            return new HttpResponse<Product>(HttpStatusCode.OK, "Seccuss Opration", product);
        }
    }
}

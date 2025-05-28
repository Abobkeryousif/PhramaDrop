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

namespace PharmaDrop.Application.Feature.Query.Products
{
    public record GetByNameProductQuery(GetByNameProductDto productDto) : IRequest<HttpResponse<Product>>;
    public class GetByNameProductQueryHandler : IRequestHandler<GetByNameProductQuery, HttpResponse<Product>>
    {
        private readonly IUnitofWork _unitofWork;
        public GetByNameProductQueryHandler(IUnitofWork unitofWork)=>
        _unitofWork = unitofWork;
        public async Task<HttpResponse<Product>> Handle(GetByNameProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitofWork.productRepository.FirstOrDefaultAsync(n=> n.Name.ToLower() == request.productDto.Name.ToLower());
            if (product == null)
                return new HttpResponse<Product>(HttpStatusCode.NotFound,$"Not Found With Name: {request.productDto.Name}");

            return new HttpResponse<Product>(HttpStatusCode.OK,"Seccuss Opration",product);

        }
    }
}

using MediatR;
using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Application.Feature.Command.Products
{
    public record DeleteProductCommand(int Id) : IRequest<HttpResponse<string>>;
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, HttpResponse<string>>
    {
        private readonly IUnitofWork _unitofWork;
        public DeleteProductCommandHandler(IUnitofWork unitofWork)=>
        _unitofWork = unitofWork;
        public async Task<HttpResponse<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitofWork.productRepository.FirstOrDefaultAsync(p=> p.Id == request.Id);
            if (product == null)
                return new HttpResponse<string>(HttpStatusCode.NotFound,$"Not Found With ID: {request.Id}");

            await _unitofWork.productRepository.DeleteAsync(product);
            return new HttpResponse<string>(HttpStatusCode.OK, "Seccuss Delete Opration",product.Name);
        }
    }
}

using MediatR;
using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Application.DTOs;
using PharmaDrop.Core.Common;
using System.Net;


namespace PharmaDrop.Application.Feature.Command.Products
{
    public record CreateProductCommand(ProductDto productDto) : IRequest<HttpResponse<ProductDto>>;
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, HttpResponse<ProductDto>>
    {
        private readonly IUnitofWork _unitofWork;
        public CreateProductCommandHandler(IUnitofWork unitofWork)=>
        _unitofWork = unitofWork;
    
        public async Task<HttpResponse<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var IsExist = await _unitofWork.productRepository.IsExist(n=> n.Name.ToLower() == request.productDto.Name.ToLower());
            if (IsExist)
                return new HttpResponse<ProductDto>(HttpStatusCode.BadRequest,"This Product Already Added");

            await _unitofWork.productRepository.CreateAsync(request.productDto);
            return new HttpResponse<ProductDto>(HttpStatusCode.OK,"Seccuss Create Opration",request.productDto);
        }
    }
}

using AutoMapper;
using MediatR;
using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Application.DTOs;
using PharmaDrop.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Application.Feature.Command.Products
{
    public record UpdateProductCommand(UpdateProductDto updateProduct) : IRequest<HttpResponse<UpdateProductDto>>;
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, HttpResponse<UpdateProductDto>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }

        public async Task<HttpResponse<UpdateProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
           
            await _unitofWork.productRepository.UpdateAsync(request.updateProduct);
            return new HttpResponse<UpdateProductDto>(HttpStatusCode.OK,"Seccuss Update Opration",request.updateProduct);
        }
    }
}

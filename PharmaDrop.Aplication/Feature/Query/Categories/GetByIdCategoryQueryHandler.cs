using MediatR;
using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Core.Common;
using PharmaDrop.Core.Entities;
using System.Net;


namespace PharmaDrop.Application.Feature.Query.Categories
{
    using Microsoft.Extensions.Logging;

    namespace PharmaDrop.Application.Feature.Query.Categories
    {
        public record GetByIdCategoryQuery(int Id) : IRequest<HttpResponse<Category>>;

        public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQuery, HttpResponse<Category>>
        {
            private readonly IUnitofWork _unitofWork;
            private readonly ILogger<GetByIdCategoryQueryHandler> _logger;

            public GetByIdCategoryQueryHandler(IUnitofWork unitofWork, ILogger<GetByIdCategoryQueryHandler> logger)
            {
                _unitofWork = unitofWork;
                _logger = logger;
            }

            public async Task<HttpResponse<Category>> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var category = await _unitofWork.categoryRepository.FirstOrDefaultAsync(c => c.Id == request.Id);

                    if (category == null)
                    {
                        _logger.LogWarning("Category with ID {Id} was not found.", request.Id);
                        return new HttpResponse<Category>(HttpStatusCode.NotFound, "Not Found Id");
                    }

                    _logger.LogInformation("Successfully retrieved category with ID {Id}.", request.Id);
                    return new HttpResponse<Category>(HttpStatusCode.OK, "Success Operation", category);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while retrieving category with ID {Id}", request.Id);
                    return new HttpResponse<Category>(HttpStatusCode.InternalServerError, "Something went wrong while processing your request.");
                }
            }
        } }}
    


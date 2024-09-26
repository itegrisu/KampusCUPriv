using AutoMapper;
using MediatR;
using X = Domain.Entities.StockManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.StockManagementRepos.StockCardImageRepo;
using Application.Features.StockManagementFeatures.StockCardImages.Rules;

namespace Application.Features.StockManagementFeatures.StockCardImages.Queries.GetByGid
{
    public class GetByGidStockCardImageQuery : IRequest<GetByGidStockCardImageResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidStockCardImageQueryHandler : IRequestHandler<GetByGidStockCardImageQuery, GetByGidStockCardImageResponse>
        {
            private readonly IMapper _mapper;
            private readonly IStockCardImageReadRepository _stockCardImageReadRepository;
            private readonly StockCardImageBusinessRules _stockCardImageBusinessRules;

            public GetByGidStockCardImageQueryHandler(IMapper mapper, IStockCardImageReadRepository stockCardImageReadRepository, StockCardImageBusinessRules stockCardImageBusinessRules)
            {
                _mapper = mapper;
                _stockCardImageReadRepository = stockCardImageReadRepository;
                _stockCardImageBusinessRules = stockCardImageBusinessRules;
            }

            public async Task<GetByGidStockCardImageResponse> Handle(GetByGidStockCardImageQuery request, CancellationToken cancellationToken)
            {
                X.StockCardImage? stockCardImage = await _stockCardImageReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken,include:x=>x.Include(x=>x.StockCardFK));
                
                await _stockCardImageBusinessRules.StockCardImageShouldExistWhenSelected(stockCardImage);

                GetByGidStockCardImageResponse response = _mapper.Map<GetByGidStockCardImageResponse>(stockCardImage);
                return response;
            }
        }
    }
}
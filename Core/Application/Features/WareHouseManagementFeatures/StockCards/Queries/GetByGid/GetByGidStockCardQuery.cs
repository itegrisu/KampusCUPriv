using Application.Features.WarehouseManagementFeatures.StockCards.Rules;
using Application.Repositories.WarehouseManagementRepos.StockCardRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.WarehouseManagements;

namespace Application.Features.WarehouseManagementFeatures.StockCards.Queries.GetByGid
{
    public class GetByGidStockCardQuery : IRequest<GetByGidStockCardResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidStockCardQueryHandler : IRequestHandler<GetByGidStockCardQuery, GetByGidStockCardResponse>
        {
            private readonly IMapper _mapper;
            private readonly IStockCardReadRepository _stockCardReadRepository;
            private readonly StockCardBusinessRules _stockCardBusinessRules;

            public GetByGidStockCardQueryHandler(IMapper mapper, IStockCardReadRepository stockCardReadRepository, StockCardBusinessRules stockCardBusinessRules)
            {
                _mapper = mapper;
                _stockCardReadRepository = stockCardReadRepository;
                _stockCardBusinessRules = stockCardBusinessRules;
            }

            public async Task<GetByGidStockCardResponse> Handle(GetByGidStockCardQuery request, CancellationToken cancellationToken)
            {
                X.StockCard? stockCard = await _stockCardReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.MeasureTypeFK).Include(x => x.StockCategoryFK));

                await _stockCardBusinessRules.StockCardShouldExistWhenSelected(stockCard);

                GetByGidStockCardResponse response = _mapper.Map<GetByGidStockCardResponse>(stockCard);
                return response;
            }
        }
    }
}
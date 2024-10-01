using Application.Features.WareHouseManagementFeatures.StockMovements.Rules;
using Application.Repositories.WarehouseManagementRepos.StockMovementRepo;
using AutoMapper;
using Domain.Entities.WarehouseManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WareHouseManagementFeatures.StockMovements.Queries.GetByGid
{
    public class GetByGidStockMovementQuery : IRequest<GetByGidStockMovementResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidStockMovementQueryHandler : IRequestHandler<GetByGidStockMovementQuery, GetByGidStockMovementResponse>
        {
            private readonly IMapper _mapper;
            private readonly IStockMovementReadRepository _stockMovementReadRepository;
            private readonly StockMovementBusinessRules _stockMovementBusinessRules;

            public GetByGidStockMovementQueryHandler(IMapper mapper, IStockMovementReadRepository stockMovementReadRepository, StockMovementBusinessRules stockMovementBusinessRules)
            {
                _mapper = mapper;
                _stockMovementReadRepository = stockMovementReadRepository;
                _stockMovementBusinessRules = stockMovementBusinessRules;
            }

            public async Task<GetByGidStockMovementResponse> Handle(GetByGidStockMovementQuery request, CancellationToken cancellationToken)
            {
                StockMovement? stockMovement = await _stockMovementReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken,
                    include: i => i.Include(i => i.StockCardFK).Include(i => i.NextWarehouseFK).Include(i => i.PreviousWarehouseFK));
                await _stockMovementBusinessRules.StockMovementShouldExistWhenSelected(stockMovement);

                GetByGidStockMovementResponse response = _mapper.Map<GetByGidStockMovementResponse>(stockMovement);
                return response;
            }
        }
    }
}
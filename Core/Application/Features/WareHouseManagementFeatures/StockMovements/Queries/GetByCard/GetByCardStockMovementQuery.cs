using Application.Helpers.PaginationHelpers;
using Application.Repositories.WarehouseManagementRepos.StockMovementRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.WarehouseManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Features.WareHouseManagementFeatures.StockMovements.Queries.GetByCard
{
    public class GetByCardStockMovementQuery : IRequest<GetListResponse<GetByCardStockMovementListItemDto>>
    {
        public Guid StockCardGid { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;

        public class GetByCardStockMovementQueryHandler : IRequestHandler<GetByCardStockMovementQuery, GetListResponse<GetByCardStockMovementListItemDto>>
        {
            private readonly IStockMovementReadRepository _stockMovementReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<StockMovement, GetByCardStockMovementListItemDto> _noPagination;

            public GetByCardStockMovementQueryHandler(IStockMovementReadRepository stockMovementReadRepository, IMapper mapper, NoPagination<StockMovement, GetByCardStockMovementListItemDto> noPagination)
            {
                _stockMovementReadRepository = stockMovementReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByCardStockMovementListItemDto>> Handle(GetByCardStockMovementQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidStockCardFK == request.StockCardGid,
                        includes: new Expression<Func<StockMovement, object>>[]
                        {
                            x => x.StockCardFK,
                            x => x.PreviousWarehouseFK,
                            x => x.NextWarehouseFK
                        });
                }

                IPaginate<StockMovement> stockMovements = await _stockMovementReadRepository.GetListAllAsync(
               index: request.PageIndex,
               size: request.PageSize,
               include: x => x.Include(x => x.StockCardFK).Include(x => x.PreviousWarehouseFK).Include(x => x.NextWarehouseFK),
                predicate: x => x.GidStockCardFK == request.StockCardGid,
               cancellationToken: cancellationToken
                );

                GetListResponse<GetByCardStockMovementListItemDto> response = _mapper.Map<GetListResponse<GetByCardStockMovementListItemDto>>(stockMovements);
                return response;

            }
        }
    }
}
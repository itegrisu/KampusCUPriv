using Application.Helpers.PaginationHelpers;
using Application.Repositories.StockManagementRepos.StockMovementRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.StockManagements;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.StockManagements;

namespace Application.Features.StockManagementFeatures.StockMovements.Queries.GetList;

public class GetListStockMovementQuery : IRequest<GetListResponse<GetListStockMovementListItemDto>>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public int Type { get; set; }

    public class GetListStockMovementQueryHandler : IRequestHandler<GetListStockMovementQuery, GetListResponse<GetListStockMovementListItemDto>>
    {
        private readonly IStockMovementReadRepository _stockMovementReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.StockMovement, GetListStockMovementListItemDto> _noPagination;

        public GetListStockMovementQueryHandler(IStockMovementReadRepository stockMovementReadRepository, IMapper mapper, NoPagination<X.StockMovement, GetListStockMovementListItemDto> noPagination)
        {
            _stockMovementReadRepository = stockMovementReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListStockMovementListItemDto>> Handle(GetListStockMovementQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<StockMovement, bool>> predicate;
            if (request.Type == 1)//Product Entry Sayfasý
            {
                predicate = x => x.TransactionDate <= request.EndDate && x.TransactionDate >= request.StartDate
                && (x.OperationType == EnumOperationType.StockEntry);
            }
            else if (request.Type == 2)//Product Output Sayfasý
            {
                predicate = x => x.TransactionDate <= request.EndDate && x.TransactionDate >= request.StartDate
                && (x.OperationType == EnumOperationType.StockOut);
            }
            else // Stock Relocation
            {
                predicate = x => x.TransactionDate <= request.EndDate && x.TransactionDate >= request.StartDate
                && (x.OperationType == EnumOperationType.StockMovement);
            }


            if (request.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    predicate: predicate,
                     includes: new Expression<Func<StockMovement, object>>[]
                        {
                            x => x.StockCardFK,
                            x => x.PreviousWarehouseFK,
                            x => x.NextWarehouseFK,
                        }
                    );

            IPaginate<X.StockMovement> stockMovements = await _stockMovementReadRepository.GetListAllAsync(
                index: request.PageIndex,
                size: request.PageSize,
                predicate: predicate,
                include: x => x.Include(x => x.StockCardFK).Include(x => x.PreviousWarehouseFK).Include(x => x.NextWarehouseFK),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStockMovementListItemDto> response = _mapper.Map<GetListResponse<GetListStockMovementListItemDto>>(stockMovements);
            return response;
        }
    }
}
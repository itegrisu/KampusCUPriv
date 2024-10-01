using Application.Helpers.PaginationHelpers;
using Application.Repositories.WarehouseManagementRepos.StockCardRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.WarehouseManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.WarehouseManagements;

namespace Application.Features.WarehouseManagementFeatures.StockCards.Queries.GetList;

public class GetListStockCardQuery : IRequest<GetListResponse<GetListStockCardListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListStockCardQueryHandler : IRequestHandler<GetListStockCardQuery, GetListResponse<GetListStockCardListItemDto>>
    {
        private readonly IStockCardReadRepository _stockCardReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.StockCard, GetListStockCardListItemDto> _noPagination;

        public GetListStockCardQueryHandler(IStockCardReadRepository stockCardReadRepository, IMapper mapper, NoPagination<X.StockCard, GetListStockCardListItemDto> noPagination)
        {
            _stockCardReadRepository = stockCardReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListStockCardListItemDto>> Handle(GetListStockCardQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                   includes: new Expression<Func<StockCard, object>>[]
                   {
                      x=>x.MeasureTypeFK,
                      x=>x.StockCategoryFK
                   });
            }

            IPaginate<X.StockCard> stockCards = await _stockCardReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                 include: x => x.Include(x => x.MeasureTypeFK).Include(x => x.StockCategoryFK)
            );

            GetListResponse<GetListStockCardListItemDto> response = _mapper.Map<GetListResponse<GetListStockCardListItemDto>>(stockCards);
            return response;
        }
    }
}
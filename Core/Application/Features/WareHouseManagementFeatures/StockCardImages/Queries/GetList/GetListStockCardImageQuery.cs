using Application.Helpers.PaginationHelpers;
using Application.Repositories.WarehouseManagementRepos.StockCardImageRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.WarehouseManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Features.WareHouseManagementFeatures.StockCardImages.Queries.GetList;

public class GetListStockCardImageQuery : IRequest<GetListResponse<GetListStockCardImageListItemDto>>
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public Guid StockCardGid { get; set; }

    public class GetListStockCardImageQueryHandler : IRequestHandler<GetListStockCardImageQuery, GetListResponse<GetListStockCardImageListItemDto>>
    {
        private readonly IStockCardImageReadRepository _stockCardImageReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<StockCardImage, GetListStockCardImageListItemDto> _noPagination;

        public GetListStockCardImageQueryHandler(IStockCardImageReadRepository stockCardImageReadRepository, IMapper mapper, NoPagination<StockCardImage, GetListStockCardImageListItemDto> noPagination)
        {
            _stockCardImageReadRepository = stockCardImageReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListStockCardImageListItemDto>> Handle(GetListStockCardImageQuery request, CancellationToken cancellationToken)
        {
            if (request.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                    predicate: x => x.StockCardFK.Gid == request.StockCardGid,
                    orderBy: x => x.RowNo,
                  includes: new Expression<Func<StockCardImage, object>>[]
                  {
                       x => x.StockCardFK,
                  });
            }


            IPaginate<StockCardImage> stockCardImages = await _stockCardImageReadRepository.GetListAsync(
                index: request.PageIndex,
                size: request.PageSize,
                cancellationToken: cancellationToken,
                orderBy: o => o.OrderBy(o => o.RowNo),
                predicate: x => x.StockCardFK.Gid == request.StockCardGid,
                include: x => x.Include(x => x.StockCardFK)
            );

            GetListResponse<GetListStockCardImageListItemDto> response = _mapper.Map<GetListResponse<GetListStockCardImageListItemDto>>(stockCardImages);
            return response;
        }
    }
}
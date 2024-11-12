using Application.Repositories.WarehouseManagementRepos.StockMovementRepo;
using Core.Application.Responses;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WareHouseManagementFeatures.StockCards.Queries.GetByWarehouse
{
    public class GetByWarehouseStockQuery : IRequest<GetListResponse<GetByWarehouseStockListItemDto>>
    {
        public Guid WarehouseGid { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;

        public class GetByWarehouseStockQueryHandler : IRequestHandler<GetByWarehouseStockQuery, GetListResponse<GetByWarehouseStockListItemDto>>
        {
            private readonly IStockMovementReadRepository _stockMovementReadRepository;

            public GetByWarehouseStockQueryHandler(IStockMovementReadRepository stockMovementReadRepository)
            {
                _stockMovementReadRepository = stockMovementReadRepository;
            }

            public async Task<GetListResponse<GetByWarehouseStockListItemDto>> Handle(GetByWarehouseStockQuery request, CancellationToken cancellationToken)
            {
                var stockMovements = await _stockMovementReadRepository.GetAll()
                    .Where(x => (x.GidPreviousWarehouseFK == request.WarehouseGid || x.GidNextWarehouseFK == request.WarehouseGid))
                    .Include(x => x.StockCardFK)
                    .Include(x => x.NextWarehouseFK).Include(x => x.PreviousWarehouseFK)
                    .Include(x => x.StockCardFK).ThenInclude(x => x.StockCategoryFK)
                    .Include(x => x.StockCardFK).ThenInclude(x => x.MeasureTypeFK)
                    .ToListAsync(cancellationToken);

                if (!stockMovements.Any())
                {
                    return new()
                    {
                        Items = new List<GetByWarehouseStockListItemDto>(),
                        Count = 0,
                        HasNext = false,
                        HasPrevious = false,
                        Index = request.PageIndex,
                        Pages = 0,
                        Size = request.PageSize
                    };
                }

                var stockCards = stockMovements
                    .GroupBy(x => x.GidStockCardFK)
                    .Select(x => x.FirstOrDefault())
                    .ToList();

                var stockCardDtos = new List<GetByWarehouseStockListItemDto>();

                foreach (var item in stockCards)
                {
                    // Girişler: Depoya yapılan stok girişleri veya relocation
                    var entries = stockMovements
                        .Where(a => a.GidStockCardFK == item.GidStockCardFK &&
                            (a.OperationType == EnumOperationType.StokGirisi && a.GidNextWarehouseFK == request.WarehouseGid ||
                             a.OperationType == EnumOperationType.StokHareketi && a.GidNextWarehouseFK == request.WarehouseGid))
                        .Sum(x => x.Amount);

                    // Çıkışlar: Depodan yapılan stok çıkışları veya relocation
                    var outputs = stockMovements
                        .Where(a => a.GidStockCardFK == item.GidStockCardFK &&
                            (a.OperationType == EnumOperationType.StokCikisi && a.GidPreviousWarehouseFK == request.WarehouseGid ||
                             a.OperationType == EnumOperationType.StokHareketi && a.GidPreviousWarehouseFK == request.WarehouseGid))
                        .Sum(x => x.Amount);

                    var stockCardDto = new GetByWarehouseStockListItemDto
                    {
                        Gid = item.GidStockCardFK,
                        GidStockCategoryFK = item.StockCardFK.GidStockCategoryFK,
                        StockCategoryFKName = item.StockCardFK.StockCategoryFK.Name,
                        GidMeasureFK = item.StockCardFK.GidMeasureFK,
                        MeasureTypeFKName = item.StockCardFK.MeasureTypeFK.Name,
                        StockCode = item.StockCardFK.StockCode,
                        StockName = item.StockCardFK.StockName,
                        Description = item.StockCardFK.Description,
                        Count = entries - outputs // Mevcut stok adedi.
                    };

                    stockCardDtos.Add(stockCardDto);
                }

                // Sayfalama işlemi
                var pagedStockCards = stockCardDtos
                    .Skip(request.PageIndex * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                var totalPages = (int)Math.Ceiling(stockCardDtos.Count / (double)request.PageSize);
                var hasNext = request.PageIndex < totalPages - 1;

                if (request.PageIndex == -1)
                    return new()
                    {
                        Items = stockCardDtos,
                        Count = stockCardDtos.Count,
                        HasNext = false,
                        HasPrevious = false,
                        Index = -1,
                        Pages = 1,
                        Size = stockCardDtos.Count
                    };
                else return new()
                {
                    Items = pagedStockCards,
                    Count = stockCardDtos.Count,
                    HasNext = hasNext,
                    HasPrevious = request.PageIndex > 0,
                    Index = request.PageIndex,
                    Pages = totalPages,
                    Size = request.PageSize
                };
            }
        }
    }
}
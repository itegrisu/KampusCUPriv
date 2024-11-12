using Application.Repositories.WarehouseManagementRepos.StockMovementRepo;
using Core.Application.Responses;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WareHouseManagementFeatures.StockCards.Queries.GetBySearch
{
    public class GetBySearchStockQuery : IRequest<GetListResponse<GetBySearchStockListItemDto>>
    {
        public Guid StockCardGid { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;

        public class GetBySearchStockQueryHandler : IRequestHandler<GetBySearchStockQuery, GetListResponse<GetBySearchStockListItemDto>>
        {
            private readonly IStockMovementReadRepository _stockMovementReadRepository;

            public GetBySearchStockQueryHandler(IStockMovementReadRepository stockMovementReadRepository)
            {
                _stockMovementReadRepository = stockMovementReadRepository;
            }

            public async Task<GetListResponse<GetBySearchStockListItemDto>> Handle(GetBySearchStockQuery request, CancellationToken cancellationToken)
            {
                // İlgili StockCard için tüm stok hareketlerini çekiyoruz
                var stockMovements = await _stockMovementReadRepository.GetAll()
                    .Where(x => x.GidStockCardFK == request.StockCardGid)
                    .Include(x => x.StockCardFK)
                    .Include(x => x.NextWarehouseFK)
                    .Include(x => x.PreviousWarehouseFK)
                    .ToListAsync();

                // Eğer stok hareketi yoksa boş bir sonuç döndürülüyor
                if (!stockMovements.Any())
                {
                    return new()
                    {
                        Items = new List<GetBySearchStockListItemDto>(),
                        Count = 0,
                        HasNext = false,
                        HasPrevious = false,
                        Index = 0,
                        Pages = 0,
                        Size = 0
                    };
                }

                // Tüm depoları getiriyoruz
                var allWarehouses = stockMovements
                .SelectMany(x => new[] { x.NextWarehouseFK, x.PreviousWarehouseFK })
                .Where(warehouse => warehouse != null) // Null değerleri filtreliyoruz
                .Distinct()
                .ToList();


                var stockSearchResults = new List<GetBySearchStockListItemDto>();

                // Her depo için giriş-çıkış hesaplaması
                foreach (var warehouse in allWarehouses)
                {
                    int totalEntries = 0;
                    int totalExits = 0;

                    // Girişler: Depoya yapılan stok girişleri ve relocation girişleri
                    totalEntries = stockMovements
                        .Where(x =>
                            x.GidNextWarehouseFK == warehouse.Gid && x.OperationType == EnumOperationType.StokGirisi ||
                            x.GidNextWarehouseFK == warehouse.Gid && x.OperationType == EnumOperationType.StokHareketi)
                        .Sum(x => x.Amount);

                    // Çıkışlar: Depodan yapılan stok çıkışları ve relocation çıkışları
                    totalExits = stockMovements
                        .Where(x =>
                            x.GidPreviousWarehouseFK == warehouse.Gid && x.OperationType == EnumOperationType.StokCikisi ||
                            x.GidPreviousWarehouseFK == warehouse.Gid && x.OperationType == EnumOperationType.StokHareketi)
                        .Sum(x => x.Amount);

                    // Mevcut stok miktarını hesaplıyoruz
                    var currentStockCount = totalEntries - totalExits;

                    // Eğer stok miktarı sıfırdan küçükse atlanıyor
                    if (currentStockCount <= 0) continue;

                    // GetBySearchStockListItemDto nesnesi oluşturuluyor.
                    var stockSearchResult = new GetBySearchStockListItemDto
                    {
                        StockName = stockMovements.First().StockCardFK.StockName, // Stok adı
                        WarehouseName = warehouse.Name, // Depo adı
                        Count = currentStockCount // Mevcut stok adedi
                    };

                    stockSearchResults.Add(stockSearchResult);
                }

                // Sayfalama için sonuçları ayıklıyoruz
                var pagedResults = stockSearchResults
                    .Skip(request.PageIndex * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                var totalPages = (int)Math.Ceiling(stockSearchResults.Count / (double)request.PageSize);
                var hasNext = request.PageIndex < totalPages - 1;

                if (request.PageIndex == -1)
                {
                    return new()
                    {
                        Items = stockSearchResults,
                        Count = stockSearchResults.Count,
                        HasNext = false,
                        HasPrevious = false,
                        Index = -1,
                        Pages = 1,
                        Size = stockSearchResults.Count
                    };
                }

                return new()
                {
                    Items = pagedResults,
                    Count = stockSearchResults.Count,
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
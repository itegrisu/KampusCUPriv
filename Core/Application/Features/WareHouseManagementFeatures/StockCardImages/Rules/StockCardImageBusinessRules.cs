using Application.Features.WareHouseManagementFeatures.StockCardImages.Constants;
using Application.Repositories.WarehouseManagementRepos.StockCardRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.WarehouseManagements;

namespace Application.Features.WareHouseManagementFeatures.StockCardImages.Rules;

public class StockCardImageBusinessRules : BaseBusinessRules
{
    public Guid GidStockCardFK { get; set; }

    private readonly IStockCardReadRepository _stockCardReadRepository;


    public async Task StockCardImageShouldExistWhenSelected(StockCardImage? item)
    {
        if (item == null)
            throw new BusinessException(StockCardImagesBusinessMessages.StockCardImageNotExists);
    }

    public async Task StockCardShouldExistWhenSelected(Guid gid)
    {
        StockCard? stockCard = await _stockCardReadRepository.GetAsync(predicate: x => x.Gid == gid);
        if (stockCard == null)
            throw new BusinessException(StockCardImagesBusinessMessages.StockCardNotExists);
    }
}
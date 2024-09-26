using Application.Features.StockManagementFeatures.StockCardImages.Constants;
using Application.Repositories.StockManagementRepos.StockCardRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.StockManagements;

namespace Application.Features.StockManagementFeatures.StockCardImages.Rules;

public class StockCardImageBusinessRules : BaseBusinessRules
{
    public Guid GidStockCardFK { get; set; }

    private readonly IStockCardReadRepository _stockCardReadRepository;


    public async Task StockCardImageShouldExistWhenSelected(X.StockCardImage? item)
    {
        if (item == null)
            throw new BusinessException(StockCardImagesBusinessMessages.StockCardImageNotExists);
    }

    public async Task StockCardShouldExistWhenSelected(Guid gid)
    {
        X.StockCard? stockCard = await _stockCardReadRepository.GetAsync(predicate: x => x.Gid == gid);
        if (stockCard == null)
            throw new BusinessException(StockCardImagesBusinessMessages.StockCardNotExists);
    }
}
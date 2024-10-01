using Application.Features.WarehouseManagementFeatures.StockCards.Constants;
using Application.Repositories.DefinitionManagementRepos.MeasureTypeRepo;
using Application.Repositories.DefinitionManagementRepos.StockCategoryRepo;
using Application.Repositories.WarehouseManagementRepos.StockCardRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.WarehouseManagements;

namespace Application.Features.WarehouseManagementFeatures.StockCards.Rules;

public class StockCardBusinessRules : BaseBusinessRules
{
    //public Guid GidStockCategoryFK { get; set; } //should be exist
    //public Guid GidMeasureFK { get; set; } //should be exist
    //public string StockName { get; set; } //should be unique
    //public string? StockCode { get; set; } //should be unique

    private readonly IStockCategoryReadRepository _stockCategoryReadRepository;
    private readonly IMeasureTypeReadRepository _measureTypeReadRepository;
    private readonly IStockCardReadRepository _stockCardReadRepository;

    public StockCardBusinessRules(IStockCategoryReadRepository stockCategoryReadRepository, IMeasureTypeReadRepository measureTypeReadRepository, IStockCardReadRepository stockCardReadRepository)
    {
        _stockCategoryReadRepository = stockCategoryReadRepository;
        _measureTypeReadRepository = measureTypeReadRepository;
        _stockCardReadRepository = stockCardReadRepository;
    }

    public async Task StockCardShouldExistWhenSelected(X.StockCard? item)
    {
        if (item == null)
            throw new BusinessException(StockCardsBusinessMessages.StockCardNotExists);
    }

    public async Task StockCategorySouldExistWhenSelected(Guid gidStockCategoryFK)
    {
        var stockCategory = await _stockCategoryReadRepository.GetAsync(predicate: x => x.Gid == gidStockCategoryFK);
        if (stockCategory == null)
            throw new BusinessException(StockCardsBusinessMessages.StockCategoryNotExists);
    }

    public async Task MeasureShouldExistWhenSelected(Guid gidMeasureFK)
    {
        var measureType = await _measureTypeReadRepository.GetAsync(predicate: x => x.Gid == gidMeasureFK);
        if (measureType == null)
            throw new BusinessException(StockCardsBusinessMessages.MeasureTypeNotExists);
    }

    public async Task StockNameShouldUnique(string stockName, Guid? gid = null)
    {
        var stockCard = await _stockCardReadRepository.GetAsync(predicate: x => x.StockName == stockName && x.Gid != gid);
        if (stockCard != null)
            throw new BusinessException(StockCardsBusinessMessages.StockNameShouldUnique);
    }

    public async Task StockCodeShouldUnique(string? stockCode, Guid? gid = null)
    {
        var stockCard = stockCode == null ? null : await _stockCardReadRepository.GetAsync(predicate: x => x.StockCode == stockCode && x.Gid != gid);
        if (stockCard != null)
            throw new BusinessException(StockCardsBusinessMessages.StockCodeShouldUnique);
    }

}
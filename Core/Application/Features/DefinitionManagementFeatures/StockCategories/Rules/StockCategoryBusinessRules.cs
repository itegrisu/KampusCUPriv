using Application.Features.DefinitionManagementFeatures.StockCategories.Constants;
using Application.Repositories.DefinitionManagementRepos.StockCategoryRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.StockCategories.Rules;

public class StockCategoryBusinessRules : BaseBusinessRules
{
    //public string Name { get; set; } Name should be unique
    //public string? Code { get; set; } Code should be unique
    private readonly IStockCategoryReadRepository _stockCategoryRepository;
    public async Task StockCategoryShouldExistWhenSelected(X.StockCategory? item)
    {
        if (item == null)
            throw new BusinessException(StockCategoriesBusinessMessages.StockCategoryNotExists);
    }

    public async Task StockNameShouldBeUnique(string name, Guid? gid = null)
    {
        var stockCategory = await _stockCategoryRepository.GetAsync(predicate: x => x.Name == name && x.Gid != gid);
        if (stockCategory != null)
            throw new BusinessException(StockCategoriesBusinessMessages.StockCategoryNameShouldBeUnique);
    }

    public async Task StockCodeShouldBeUniqe(string? code, Guid? gid = null)
    {

        var stockCategory = code == null ? null : await _stockCategoryRepository.GetAsync(predicate: x => x.Code == code && x.Gid != gid);
        if (stockCategory != null)
            throw new BusinessException(StockCategoriesBusinessMessages.StockCategoryCodeShouldBeUnique);
    }
}
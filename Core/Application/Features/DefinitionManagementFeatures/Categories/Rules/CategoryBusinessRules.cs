using Application.Features.DefinitionFeatures.Categories.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionFeatures.Categories.Rules;

public class CategoryBusinessRules : BaseBusinessRules
{
    public async Task CategoryShouldExistWhenSelected(X.Category? item)
    {
        if (item == null)
            throw new BusinessException(CategoriesBusinessMessages.CategoryNotExists);
    }
}
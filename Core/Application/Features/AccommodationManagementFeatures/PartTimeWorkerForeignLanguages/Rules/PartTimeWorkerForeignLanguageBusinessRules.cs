using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Rules;

public class PartTimeWorkerForeignLanguageBusinessRules : BaseBusinessRules
{
    public async Task PartTimeWorkerForeignLanguageShouldExistWhenSelected(X.PartTimeWorkerForeignLanguage? item)
    {
        if (item == null)
            throw new BusinessException(PartTimeWorkerForeignLanguagesBusinessMessages.PartTimeWorkerForeignLanguageNotExists);
    }
}
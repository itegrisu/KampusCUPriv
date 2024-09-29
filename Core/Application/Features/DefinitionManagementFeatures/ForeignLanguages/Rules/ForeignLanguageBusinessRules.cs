using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Constants;
using Application.Repositories.DefinitionManagementRepos.ForeignLanguageRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.ForeignLanguages.Rules;

public class ForeignLanguageBusinessRules : BaseBusinessRules
{
    private readonly IForeignLanguageReadRepository _foreignLanguageReadRepository;

    public ForeignLanguageBusinessRules(IForeignLanguageReadRepository foreignLanguageReadRepository)
    {
        _foreignLanguageReadRepository = foreignLanguageReadRepository;
    }

    public async Task ForeignLanguageShouldExistWhenSelected(X.ForeignLanguage? item)
    {
        if (item == null)
            throw new BusinessException(ForeignLanguagesBusinessMessages.ForeignLanguageNotExists);
    }

    public async Task CheckForeignLanguageNameIsUnique(string foreignLanguageName, Guid? foreignLanguageGuid = null)
    {
        var foreignLanguage = await _foreignLanguageReadRepository.GetAsync(predicate: x => x.Name.ToLower() == foreignLanguageName.ToLower() && (foreignLanguageGuid == null || x.Gid != foreignLanguageGuid));
        if (foreignLanguage != null)
            throw new BusinessException(ForeignLanguagesBusinessMessages.ForeignLanguageIsAlreadyExists);
    }

    public async Task CheckForeignLanguageCodeIsUnique(string foreignLanguageCode, Guid? foreignLanguageGuid = null)
    {
        if (string.IsNullOrEmpty(foreignLanguageCode))
            return;

        var foreignLanguage = await _foreignLanguageReadRepository.GetAsync(predicate: x => x.LanguageCode.ToLower() == foreignLanguageCode.ToLower() && (foreignLanguageGuid == null || x.Gid != foreignLanguageGuid));
        if (foreignLanguage != null)
            throw new BusinessException(ForeignLanguagesBusinessMessages.ForeignLanguageCodeIsAlreadyExists);
    }
}
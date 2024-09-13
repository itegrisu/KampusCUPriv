using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Constants;
using Application.Repositories.DefinitionManagementRepos.ForeignLanguageRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Rules;

public class PersonnelForeignLanguageBusinessRules : BaseBusinessRules
{

    private readonly IForeignLanguageReadRepository _languageReadRepository;
    private readonly IUserReadRepository _personnelReadRepository;

    public PersonnelForeignLanguageBusinessRules(IForeignLanguageReadRepository languageReadRepository, IUserReadRepository personnelReadRepository)
    {
        _languageReadRepository = languageReadRepository;
        _personnelReadRepository = personnelReadRepository;
    }

    public async Task PersonnelForeignLanguageShouldExistWhenSelected(X.PersonnelForeignLanguage? item)
    {
        if (item == null)
            throw new BusinessException(PersonnelForeignLanguagesBusinessMessages.PersonnelForeignLanguageNotExists);
    }

    public async Task LanguageShouldExistWhenSelected(Guid gid)
    {
        var language = await _languageReadRepository.GetAsync(predicate: x => x.Gid == gid);
        if (language == null)
            throw new BusinessException(PersonnelForeignLanguagesBusinessMessages.LanguageNotExists);
    }

    public async Task PersonnelShouldExistWhenSelected(Guid gid)
    {
        var personnel = await _personnelReadRepository.GetAsync(predicate: x => x.Gid == gid);
        if (personnel == null)
            throw new BusinessException(PersonnelForeignLanguagesBusinessMessages.PersonnelNotExists);
    }


}
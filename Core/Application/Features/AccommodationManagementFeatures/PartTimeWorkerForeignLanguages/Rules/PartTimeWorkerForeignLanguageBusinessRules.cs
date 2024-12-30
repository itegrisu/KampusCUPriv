using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Constants;
using Application.Repositories.AccommodationManagements.PartTimeWorkerForeignLanguageRepo;
using Application.Repositories.AccommodationManagements.PartTimeWorkerRepo;
using Application.Repositories.DefinitionManagementRepos.ForeignLanguageRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Rules;

public class PartTimeWorkerForeignLanguageBusinessRules : BaseBusinessRules
{

    private readonly IPartTimeWorkerForeignLanguageReadRepository _partTimeWorkerForeignLanguageReadRepository;
    private readonly IPartTimeWorkerReadRepository _partTimeWorkerReadRepository;
    private readonly IForeignLanguageReadRepository _foreignLanguageReadRepository;

    public PartTimeWorkerForeignLanguageBusinessRules(IPartTimeWorkerForeignLanguageReadRepository partTimeWorkerForeignLanguageReadRepository, IPartTimeWorkerReadRepository partTimeWorkerReadRepository, IForeignLanguageReadRepository foreignLanguageReadRepository)
    {
        _partTimeWorkerForeignLanguageReadRepository = partTimeWorkerForeignLanguageReadRepository;
        _partTimeWorkerReadRepository = partTimeWorkerReadRepository;
        _foreignLanguageReadRepository = foreignLanguageReadRepository;
    }

    public async Task PartTimeWorkerForeignLanguageShouldExistWhenSelected(X.PartTimeWorkerForeignLanguage? item)
    {
        if (item == null)
            throw new BusinessException(PartTimeWorkerForeignLanguagesBusinessMessages.PartTimeWorkerForeignLanguageNotExists);
    }

    public async Task PartTimeWorkerAlreadyExist(Guid partTimeWorkerGid)
    {
        if (await _partTimeWorkerReadRepository.GetByGidAsync(partTimeWorkerGid) == null)
            throw new BusinessException(PartTimeWorkerForeignLanguagesBusinessMessages.PartTimeWorkerNotExists);

    }

    public async Task ForeignLanguageAlreadyExist(Guid foreignLanguageGid)
    {
        if (await _foreignLanguageReadRepository.GetByGidAsync(foreignLanguageGid) == null)
            throw new BusinessException(PartTimeWorkerForeignLanguagesBusinessMessages.ForeignLangugeNotExists);
    }

    public async Task IsForeignLanguageAlreadyAdded(Guid partTimeWorkerGid, Guid foreignLanguageGid)
    {
        if (await _partTimeWorkerForeignLanguageReadRepository.GetAsync(x => x.GidPartTimeWorkerFK == partTimeWorkerGid && x.GidForeignLanguageFK == foreignLanguageGid) != null)
            throw new BusinessException(PartTimeWorkerForeignLanguagesBusinessMessages.ForeignLanguageAlreadyExist);
    }

}
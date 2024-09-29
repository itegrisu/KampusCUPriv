using Application.Features.DefinitionManagementFeatures.MeasureTypes.Constants;
using Application.Repositories.DefinitionManagementRepos.MeasureTypeRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.MeasureTypes.Rules;

public class MeasureTypeBusinessRules : BaseBusinessRules
{
    public string OlcuAdi { get; set; }

    private readonly IMeasureTypeReadRepository _measureTypeReadRepository;

    public MeasureTypeBusinessRules(IMeasureTypeReadRepository measureTypeReadRepository)
    {
        _measureTypeReadRepository = measureTypeReadRepository;
    }

    public async Task MeasureTypeShouldExistWhenSelected(X.MeasureType? item)
    {
        if (item == null)
            throw new BusinessException(MeasureTypesBusinessMessages.MeasureTypeNotExists);
    }

    public async Task CheckMeasureTypeNameIsUnique(string measureTypeName, Guid? measureTypeGuid = null)
    {
        var measureType = await _measureTypeReadRepository.GetAsync(predicate: x => x.Name.ToLower() == measureTypeName.ToLower() && (measureTypeGuid == null || x.Gid != measureTypeGuid));
        if (measureType != null)
            throw new BusinessException(MeasureTypesBusinessMessages.MeasureTypeIsAlreadyExists);
    }
}
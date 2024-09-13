using Application.Features.DefinitionManagementFeatures.OtoBrands.Constants;
using Application.Repositories.DefinitionManagementRepos.OtoBrandRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.OtoBrands.Rules;

public class OtoBrandBusinessRules : BaseBusinessRules
{
    public string AracMarkaAdi { get; set; }
    private readonly IOtoBrandReadRepository _otoBrandReadRepository;

    public OtoBrandBusinessRules(IOtoBrandReadRepository otoBrandReadRepository)
    {
        _otoBrandReadRepository = otoBrandReadRepository;
    }

    public async Task OtoBrandShouldExistWhenSelected(X.OtoBrand? item)
    {
        if (item == null)
            throw new BusinessException(OtoBrandsBusinessMessages.OtoBrandNotExists);
    }

    public async Task OtoBrandNameIsUnique(string otoBrandName, Guid? otoBrandGuid = null)
    {
        var otoBrand = await _otoBrandReadRepository.GetAsync(predicate: x => x.AracMarkaAdi.ToLower() == otoBrandName.ToLower() && (otoBrandGuid == null || x.Gid != otoBrandGuid));
        if (otoBrand != null)
            throw new BusinessException(OtoBrandsBusinessMessages.OtoBrandIsAlreadyExists);
    }
}
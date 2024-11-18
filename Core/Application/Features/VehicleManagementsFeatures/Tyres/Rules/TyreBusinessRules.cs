using Application.Features.VehicleManagementFeatures.Tyres.Constants;
using Application.Repositories.VehicleManagementsRepos.TyreRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.Tyres.Rules;

public class TyreBusinessRules : BaseBusinessRules
{
    private readonly ITyreReadRepository _readRepository;

    public TyreBusinessRules(ITyreReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task TyreShouldExistWhenSelected(X.Tyre? item)
    {
        if (item == null)
            throw new BusinessException(TyresBusinessMessages.TyreNotExists);
    }

    public async Task IsAllreadyExist(string tyreNo)
    {
        var tyreType = await _readRepository.GetSingleAsync(x => x.TyreNo == tyreNo);
        if(tyreType != null)
        {
            throw new BusinessException(TyresBusinessMessages.TyreNoAllreadyExist);
        }
    }
}
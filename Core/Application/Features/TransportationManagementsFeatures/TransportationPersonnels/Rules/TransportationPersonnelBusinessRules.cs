using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Constants;
using Application.Repositories.TransportationRepos.TransportationPersonnelRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationPersonnels.Rules;

public class TransportationPersonnelBusinessRules : BaseBusinessRules
{
    private readonly ITransportationPersonnelReadRepository _transportationPersonnelReadRepository;

    public TransportationPersonnelBusinessRules(ITransportationPersonnelReadRepository transportationPersonnelReadRepository)
    {
        _transportationPersonnelReadRepository = transportationPersonnelReadRepository;
    }

    public async Task TransportationPersonnelShouldExistWhenSelected(X.TransportationPersonnel? item)
    {
        if (item == null)
            throw new BusinessException(TransportationPersonnelsBusinessMessages.TransportationPersonnelNotExists);
    }

    public async Task PersonnelAllreadyExist(Guid gid)
    {
        var exist = await _transportationPersonnelReadRepository.GetSingleAsync(x => x.GidStaffPersonnelFK == gid);
        if(exist != null)
            throw new BusinessException(TransportationPersonnelsBusinessMessages.PersonnelAllreadyExist);
    }
}
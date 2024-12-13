using Application.Features.TransportationManagementFeatures.TransportationServices.Constants;
using Application.Repositories.TransportationRepos.TransportationGroupRepo;
using Application.Repositories.TransportationRepos.TransportationPassengerRepo;
using Application.Repositories.TransportationRepos.TransportationPersonnelRepo;
using Application.Repositories.TransportationRepos.TransportationServiceRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Enums;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationServices.Rules;

public class TransportationServiceBusinessRules : BaseBusinessRules
{
    private readonly ITransportationServiceReadRepository _transportationServiceReadRepository;
    private readonly ITransportationGroupReadRepository _transportationGroupReadRepository;
    private readonly ITransportationPersonnelReadRepository _transportationPersonnelReadRepository;
    private readonly ITransportationPassengerReadRepository _transportationPassengerReadRepository;

    public TransportationServiceBusinessRules(ITransportationServiceReadRepository transportationServiceReadRepository, ITransportationGroupReadRepository transportationGroupReadRepository, ITransportationPersonnelReadRepository transportationPersonnelReadRepository, ITransportationPassengerReadRepository transportationPassengerReadRepository)
    {
        _transportationServiceReadRepository = transportationServiceReadRepository;
        _transportationGroupReadRepository = transportationGroupReadRepository;
        _transportationPersonnelReadRepository = transportationPersonnelReadRepository;
        _transportationPassengerReadRepository = transportationPassengerReadRepository;
    }

    public async Task TransportationServiceShouldExistWhenSelected(X.TransportationService? item)
    {
        if (item == null)
            throw new BusinessException(TransportationServicesBusinessMessages.TransportationServiceNotExists);
    }

    public async Task TransportationServiceShouldReport(Guid gid)
    {
        X.TransportationService? item = await _transportationServiceReadRepository.GetAsync(predicate: x => x.TransportationServiceStatus == EnumTransportationServiceStatus.Gecerli && x.RefNoTransportation != null && x.Gid == gid);
        if (item == null)
            throw new BusinessException(TransportationServicesBusinessMessages.TransportationServiceNotReported);
    }

    public async Task GroupShouldReport(Guid gid)
    {
        var item = await _transportationGroupReadRepository.GetAsync(predicate: x => x.GidTransportationServiceFK == gid && x.RefNoTransportationGroup != null);
        if (item == null)
            throw new BusinessException(TransportationServicesBusinessMessages.GroupNotReported);
    }

    public async Task PersonnelShouldReport(Guid gid)
    {
        var item = await _transportationPersonnelReadRepository.GetListAsync(predicate: x => x.GidTransportationServiceFK == gid);
        var personnel = item.Items.Where(x => x.StaffStatus == EnumStaffStatus.Gecerli).FirstOrDefault();
        if (personnel == null)
            throw new BusinessException(TransportationServicesBusinessMessages.PersonnelNotReported);
    }

    public async Task PassengerShouldReport(Guid gid)
    {
        var item = await _transportationPassengerReadRepository.GetListAsync(predicate: x => x.GidTransportationGroupFK == gid);
        var passenger = item.Items.Where(x => x.PassengerStatus == EnumPassengerStatus.Gecerli).FirstOrDefault();
        if (passenger == null)
            throw new BusinessException(TransportationServicesBusinessMessages.PassengerNotReported);
    }

}
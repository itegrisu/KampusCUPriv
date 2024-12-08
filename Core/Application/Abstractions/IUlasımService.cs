using Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetByGid;
using Domain.Entities.TransportationManagements;

namespace Application.Abstractions
{
    public interface IUlasımService
    {
        Task TestService();
        Task<string> IpListesiAsync();
        Task<string> SeferEkleAsync(TransportationService transportationService);
        Task<string> SeferGuncelleAsync(TransportationService transportationService, long seferRefNumber);
        Task<string> SeferIptalAsync(long seferRefNumber);
        Task<string> GrupEkleAsync(TransportationGroup transportationGroup);
        Task<string> GrupIptalAsync(long seferRefNumber, long grupRefNumber);
        Task<string> PersonelEkleAsync(List<TransportationPersonnel> transportationPersonnel, long seferRefNumber);
        Task<string> PersonelIptalAsync(TransportationPersonnel transportationPersonnel, long seferRefNumber);
        Task<string> YolcuEkleAsync(List<TransportationPassenger> transportationPassengers, long seferRefNumber, long grupRefNumber);
        Task<string> YolcuIptalAsync(long seferRefNumber, long RefNoTransportationPassenger);
    }
}

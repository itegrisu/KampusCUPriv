using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Constants;
using Application.Repositories.SupplierManagementRepos.SCCompanyRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Rules;

public class ReservationHotelStaffBusinessRules : BaseBusinessRules
{
    private readonly ISCCompanyReadRepository _sCCompanyReadRepository;

    public ReservationHotelStaffBusinessRules(ISCCompanyReadRepository sCCompanyReadRepository)
    {
        _sCCompanyReadRepository = sCCompanyReadRepository;
    }

    public async Task ReservationHotelStaffShouldExistWhenSelected(X.ReservationHotelStaff? item)
    {
        if (item == null)
            throw new BusinessException(ReservationHotelStaffsBusinessMessages.ReservationHotelStaffNotExists);
    }

    public async Task HotelShouldExist(Guid hotelGid)
    {
        if (await _sCCompanyReadRepository.GetByGidAsync(hotelGid) == null)
            throw new BusinessException(ReservationHotelStaffsBusinessMessages.HotelNotExists);
    }

}
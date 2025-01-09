using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Constants;
using Application.Repositories.AccommodationManagements.ReservationHotelStaffRepo;
using Application.Repositories.SupplierManagementRepos.SCCompanyRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Rules;

public class ReservationHotelStaffBusinessRules : BaseBusinessRules
{
    private readonly ISCCompanyReadRepository _sCCompanyReadRepository;
    private readonly IReservationHotelStaffReadRepository _reservationHotelStaffReadRepository;

    public ReservationHotelStaffBusinessRules(ISCCompanyReadRepository sCCompanyReadRepository, IReservationHotelStaffReadRepository reservationHotelStaffReadRepository)
    {
        _sCCompanyReadRepository = sCCompanyReadRepository;
        _reservationHotelStaffReadRepository = reservationHotelStaffReadRepository;
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

    public async Task GsmNoAlreadyExist(string gsmNo)
    {
        if (await _reservationHotelStaffReadRepository.GetSingleAsync(x => x.GsmNo == gsmNo) != null)
            throw new BusinessException(ReservationHotelStaffsBusinessMessages.GsmNoAlreadyExists);
    }


}
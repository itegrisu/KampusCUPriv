using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Rules;

public class ReservationHotelStaffBusinessRules : BaseBusinessRules
{
    public async Task ReservationHotelStaffShouldExistWhenSelected(X.ReservationHotelStaff? item)
    {
        if (item == null)
            throw new BusinessException(ReservationHotelStaffsBusinessMessages.ReservationHotelStaffNotExists);
    }
}
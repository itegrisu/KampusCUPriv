using Domain.Entities.AccommodationManagements;
using Domain.Entities.GeneralManagements;

namespace Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(User user, int minute);
        Token CreateAccessTokenForPartTime(PartTimeWorker partTimeWorker, int minute);
        Token CreateAccessTokenForWorker(ReservationHotelStaff reservationHotelStaff, int minute);
    }
}

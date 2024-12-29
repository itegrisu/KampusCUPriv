using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.SupplierCustomerManagements;

namespace Domain.Entities.AccommodationManagements
{
    public class ReservationHotel : BaseEntity
    {
        public Guid GidReservationFK { get; set; }
        public Reservation ReservationFK { get; set; }
        public Guid GidHotelFK { get; set; }
        public SCCompany SCCompanyFK { get; set; }
        public Guid GidBuyCurrencyTypeFK { get; set; }
        public Currency BuyCurrencyFK { get; set; }
        public Guid GidSellCurrencyTypeFK { get; set; }
        public Currency SellCurrencyFK { get; set; }

        public ICollection<ReservationDetail>? ReservationDetails { get; set; }
        public ICollection<ReservationHotelPartTimeWorker>? ReservationHotelPartTimeWorkers { get; set; }
    }
}

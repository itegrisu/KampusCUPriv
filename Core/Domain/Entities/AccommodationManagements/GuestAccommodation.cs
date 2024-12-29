using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.SupplierCustomerManagements;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AccommodationManagements
{
    public class GuestAccommodation : BaseEntity
    {
        public Guid GidHotelFK { get; set; }
        public SCCompany SCCompanyFK { get; set; }
        public Guid? GidBuyCurrencyFK { get; set; }
        public Currency? BuyCurrencyFK { get; set; }
        public Guid? GidSellCurrencyFK { get; set; }
        public Currency? SellCurrencyFK { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Institution { get; set; }
        public int GuestCount { get; set; }
        public string? Description { get; set; }
        public EnumGuestAccommodationStatus GuestAccommodationStatus { get; set; }
        public decimal? BuyPrice { get; set; }
        public decimal? SellPrice { get; set; }

        public ICollection<GuestAccommodationPerson>? GuestAccommodationPersons { get; set; }
        public ICollection<GuestAccommodationRoom>? GuestAccommodationRooms { get; set; }
    }
}

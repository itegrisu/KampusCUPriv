using Core.Entities;
using Domain.Entities.DefinitionManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OfferManagements
{
    public class OfferTransaction : BaseEntity
    {

        public Guid GidOfferFK { get; set; }
        public Offer OfferFK { get; set; }
        public Guid GidCurrencyFK { get; set; }
        public Currency CurrencyFK { get; set; }
        public string OfferId { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public DateTime? OfferDeadline { get; set; }
        public string? Document { get; set; }
        public string? Description { get; set; }


    }
}

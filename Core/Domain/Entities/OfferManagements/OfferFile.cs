using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OfferManagements
{
    public class OfferFile : BaseEntity
    {

        public Guid GidOfferFK { get; set; }
        public Offer OfferFK { get; set; }

        public string Title { get; set; } = string.Empty;
        public string? Document { get; set; }
        public string? Description { get; set; }


    }
}

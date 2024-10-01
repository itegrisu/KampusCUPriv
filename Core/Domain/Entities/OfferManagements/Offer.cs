using Core.Entities;
using Domain.Enums;

namespace Domain.Entities.OfferManagements
{
    public class Offer : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;
        public EnumOfferStatus OfferStatus { get; set; }

        public ICollection<OfferFile>? OfferFiles { get; set; }
        public ICollection<OfferTransaction>? OfferTransactions { get; set; }

    }
}

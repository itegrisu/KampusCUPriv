using Core.Application.Responses;

namespace Application.Features.OfferManagementFeatures.OfferTransactions.Queries.GetByGid
{
    public class GetByGidOfferTransactionResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidOfferFK { get; set; }
        public string OfferFKTitle { get; set; }
        public Guid GidCurrencyFK { get; set; }
        public string CurrencyFKName { get; set; }
        public string CurrencyFKSymbol { get; set; }
        public string OfferId { get; set; }
        public decimal Total { get; set; }
        public DateTime OfferDeadline { get; set; }
        public string? Document { get; set; }
        public string? Description { get; set; }

    }
}
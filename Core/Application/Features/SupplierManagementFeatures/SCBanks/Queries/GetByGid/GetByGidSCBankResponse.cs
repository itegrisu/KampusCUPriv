using Core.Application.Responses;

namespace Application.Features.SupplierCustomerManagementFeatures.SCBanks.Queries.GetByGid
{
    public class GetByGidSCBankResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidSCCompanyFK { get; set; }
        public string SCCompanyFKCompanyName { get; set; }
        public Guid GidCurrencyFK { get; set; }
        public string CurrencyFKDovizAdi { get; set; }

        public string Bank { get; set; }
        public string BranchName { get; set; }
        public string? BranchCode { get; set; }
        public string AccountNumber { get; set; }
        public string? IbanNo { get; set; }
        public string? SwiftNo { get; set; }

    }
}
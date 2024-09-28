using Core.Application.Responses;

namespace Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Queries.GetByGid
{
    public class GetByGidSCWorkHistoryResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidSCCompanyFK { get; set; }
        public string SCCompanyFKCompanyName { get; set; }
        public string Title { get; set; }
        public string? Detail { get; set; }
        public DateTime WorkDate { get; set; }
        public string? WorkFile { get; set; }

    }
}
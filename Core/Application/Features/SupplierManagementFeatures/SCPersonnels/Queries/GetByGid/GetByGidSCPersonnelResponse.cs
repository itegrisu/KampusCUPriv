using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Queries.GetByGid
{
    public class GetByGidSCPersonnelResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidSCCompanyFK { get; set; }
        public string SCCompanyFKCompanyName { get; set; }
        public Guid GidPersonnelFK { get; set; }
        public string UserFKFullName { get; set; }
        public string UserFKAvatar { get; set; }
        public EnumSCPersonnelLoginStatus SCPersonnelLoginStatus { get; set; }

    }
}
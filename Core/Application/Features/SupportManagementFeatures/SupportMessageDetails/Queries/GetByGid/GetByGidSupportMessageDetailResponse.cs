using Core.Application.Responses;
using Domain.Entities.GeneralManagements;
using Domain.Entities.SupportManagements;

namespace Application.Features.SupportManagementFeatures.SupportMessageDetails.Queries.GetByGid
{
    public class GetByGidSupportMessageDetailResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidMessageFK { get; set; }
        public Guid GidReadUserFK { get; set; }
        public string UserFKFullName { get; set; }
        public string UserFKAvatar { get; set; }

        public DateTime ReadDate { get; set; }
        public string ReadIp { get; set; }

    }
}
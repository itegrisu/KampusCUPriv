using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.SupportManagementFeatures.SupportRequests.Queries.GetByGid
{
    public class GetByGidSupportRequestResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string Title { get; set; }
        public string UserFKFullName { get; set; }
        public string UserFKAvatar {  get; set; }
        public EnumSupportStatus SupportStatus { get; set; }
        public EnumPriorityType PriorityType { get; set; }
        public EnumSupportType SupportType { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
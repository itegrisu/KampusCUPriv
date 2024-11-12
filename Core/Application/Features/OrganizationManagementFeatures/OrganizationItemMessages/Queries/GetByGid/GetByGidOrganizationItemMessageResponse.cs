using Core.Application.Responses;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Queries.GetByGid
{
    public class GetByGidOrganizationItemMessageResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidOrganizationItemFK { get; set; }
        public string OrganizationItemFKItemName { get; set; }
        public Guid GidSendMessageUserFK { get; set; }
        public string UserFKFullName { get; set; }

        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
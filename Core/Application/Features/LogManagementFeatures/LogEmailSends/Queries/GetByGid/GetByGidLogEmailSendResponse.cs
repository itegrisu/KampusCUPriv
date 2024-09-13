using Core.Application.Responses;

namespace Application.Features.LogManagementFeatures.LogEmailSends.Queries.GetByGid
{
    public class GetByGidLogEmailSendResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidUserFK { get; set; }
        public string UserFKFullName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

    }
}
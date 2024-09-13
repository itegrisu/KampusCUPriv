using Core.Application.Responses;

namespace Application.Features.LogManagementFeatures.LogUserPageVisitActions.Queries.GetByGid
{
    public class GetByGidLogUserPageVisitActionResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidUserFK { get; set; }
        public string UserFKFullName { get; set; }
        public string? IpAddress { get; set; }
        public string PageInfo { get; set; }
        public string Operation { get; set; }
        public string JSonData { get; set; }

    }
}
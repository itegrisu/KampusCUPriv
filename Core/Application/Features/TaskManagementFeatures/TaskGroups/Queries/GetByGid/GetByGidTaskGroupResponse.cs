using Core.Application.Responses;

namespace Application.Features.TaskManagementFeatures.TaskGroups.Queries.GetByGid
{
    public class GetByGidTaskGroupResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string GroupName { get; set; }
        public int Count { get; set; }
    }
}
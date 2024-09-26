using Core.Application.Responses;

namespace Application.Features.TaskManagementFeatures.TaskManagers.Queries.GetByGid
{
    public class GetByGidTaskManagerResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string UserFKGid { get; set; }
        public string UserFKFullName { get; set; }
        public string UserFKAvatar { get; set; }
    }
}
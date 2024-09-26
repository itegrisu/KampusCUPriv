using Core.Application.Responses;

namespace Application.Features.TaskManagementFeatures.TaskGroupUsers.Queries.GetByGid
{
    public class GetByGidTaskGroupUserResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid UserFKGid { get; set; }
        public string UserFKFullName { get; set; }
        public string UserFKIdNumber { get; set; }
        public string UserFKAvatar { get; set; }
    }
}
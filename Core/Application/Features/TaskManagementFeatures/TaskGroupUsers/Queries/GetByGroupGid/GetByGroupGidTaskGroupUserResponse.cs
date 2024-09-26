using Core.Application.Dtos;

namespace Application.Features.TaskManagementFeatures.TaskGroupUsers.Queries.GetByGroupGid
{
    public class GetByGroupGidTaskGroupUserResponse : IDto
    {
        public string Gid { get; set; }
        public Guid UserFKGid { get; set; }
        public string TaskGroupFKGid { get; set; }
        public string UserFKFullName { get; set; }
        public string UserFKIdNumber { get; set; }
        public string UserFKAvatar { get; set; }
    }
}

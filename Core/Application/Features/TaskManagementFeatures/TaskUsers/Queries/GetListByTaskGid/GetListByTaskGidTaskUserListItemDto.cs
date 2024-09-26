using Core.Application.Dtos;

namespace Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetListByTaskGid
{
    public class GetListByTaskGidTaskUserListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public string UserFKFullName { get; set; }
        public string UserFKAvatar { get; set; }
        public string UserFKGid { get; set; }
    }
}

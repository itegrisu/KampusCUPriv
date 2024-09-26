using Core.Application.Dtos;

namespace Application.Features.TaskManagementFeatures.TaskComments.Queries.GetByTaskGid
{
    public class GetByTaskGidTaskCommentResponse : IDto
    {

        public Guid Gid { get; set; }
        public string UserFKFullName { get; set; }
        public string UserFKAvatar { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }

    }
}
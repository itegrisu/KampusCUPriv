using Core.Application.Responses;

namespace Application.Features.TaskManagementFeatures.TaskComments.Queries.GetByGid;

public class GetByGidTaskCommentResponse : IResponse
{
    public Guid Gid { get; set; }
    public string UserFKFullName { get; set; }
    public string UserFKAvatar { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
}
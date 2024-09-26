using Core.Application.Dtos;
using Domain.Entities.GeneralManagements;

namespace Application.Features.TaskManagementFeatures.TaskComments.Queries.GetList;

public class GetListTaskCommentListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidUserFK { get; set; }
    public User UserFK { get; set; }
    public Guid GidTaskFK { get; set; }
    public Task TaskFK { get; set; }
    public string Comment { get; set; }
}
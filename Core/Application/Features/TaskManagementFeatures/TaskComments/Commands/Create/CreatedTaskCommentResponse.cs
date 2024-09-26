using Application.Features.Base;
using Application.Features.TaskManagementFeatures.TaskComments.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.TaskManagementFeatures.TaskComments.Commands.Create;

public class CreatedTaskCommentResponse : BaseResponse, IResponse
{
    public GetByGidTaskCommentResponse Obj { get; set; }
}
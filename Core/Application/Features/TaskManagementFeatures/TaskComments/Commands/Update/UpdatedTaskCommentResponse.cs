using Application.Features.Base;
using Application.Features.TaskManagementFeatures.TaskComments.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.TaskManagementFeatures.TaskComments.Commands.Update;

public class UpdatedTaskCommentResponse : BaseResponse, IResponse
{
    public GetByGidTaskCommentResponse Obj { get; set; }
}
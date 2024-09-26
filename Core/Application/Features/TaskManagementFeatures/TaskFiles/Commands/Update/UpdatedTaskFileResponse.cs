using Application.Features.Base;
using Application.Features.TaskManagementFeatures.TaskFiles.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.TaskManagementFeatures.TaskFiles.Commands.Update;

public class UpdatedTaskFileResponse : BaseResponse, IResponse
{
    public GetByGidTaskFileResponse Obj { get; set; }
}
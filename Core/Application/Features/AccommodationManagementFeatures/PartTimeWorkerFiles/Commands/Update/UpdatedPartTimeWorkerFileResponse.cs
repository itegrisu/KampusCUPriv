using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Commands.Update;

public class UpdatedPartTimeWorkerFileResponse : BaseResponse, IResponse
{
    public GetByGidPartTimeWorkerFileResponse Obj { get; set; }
}
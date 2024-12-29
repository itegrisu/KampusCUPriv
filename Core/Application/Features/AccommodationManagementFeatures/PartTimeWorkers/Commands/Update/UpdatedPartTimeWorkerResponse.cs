using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Commands.Update;

public class UpdatedPartTimeWorkerResponse : BaseResponse, IResponse
{
    public GetByGidPartTimeWorkerResponse Obj { get; set; }
}
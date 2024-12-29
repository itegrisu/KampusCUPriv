using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Commands.Create;

public class CreatedPartTimeWorkerResponse : BaseResponse, IResponse
{
    public GetByGidPartTimeWorkerResponse Obj { get; set; }
}
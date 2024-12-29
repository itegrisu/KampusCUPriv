using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Commands.Create;

public class CreatedPartTimeWorkerFileResponse : BaseResponse, IResponse
{
    public GetByGidPartTimeWorkerFileResponse Obj { get; set; }
}
using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Commands.Create;

public class CreatedPartTimeWorkerForeignLanguageResponse : BaseResponse, IResponse
{
    public GetByGidPartTimeWorkerForeignLanguageResponse Obj { get; set; }
}
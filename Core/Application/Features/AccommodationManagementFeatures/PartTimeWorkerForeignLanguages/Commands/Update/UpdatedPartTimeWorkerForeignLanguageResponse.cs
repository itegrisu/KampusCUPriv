using Application.Features.Base;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Commands.Update;

public class UpdatedPartTimeWorkerForeignLanguageResponse : BaseResponse, IResponse
{
    public GetByGidPartTimeWorkerForeignLanguageResponse Obj { get; set; }
}
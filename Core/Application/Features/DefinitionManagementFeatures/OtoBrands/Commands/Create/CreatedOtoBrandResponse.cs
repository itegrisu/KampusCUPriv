using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.OtoBrands.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.DefinitionManagementFeatures.OtoBrands.Commands.Create;

public class CreatedOtoBrandResponse : BaseResponse, IResponse
{
    public GetByGidOtoBrandResponse Obj { get; set; }
}
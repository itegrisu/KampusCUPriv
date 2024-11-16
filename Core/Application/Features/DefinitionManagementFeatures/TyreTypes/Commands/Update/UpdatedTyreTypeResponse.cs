using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.TyreTypes.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.TyreTypes.Commands.Update;

public class UpdatedTyreTypeResponse : BaseResponse, IResponse
{
    public GetByGidTyreTypeResponse Obj { get; set; }
}
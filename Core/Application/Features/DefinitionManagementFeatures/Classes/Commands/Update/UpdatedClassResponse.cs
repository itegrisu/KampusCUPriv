using Application.Features.Base;
using Application.Features.DefinitionFeatures.Classes.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.DefinitionFeatures.Classes.Commands.Update;

public class UpdatedClassResponse : BaseResponse, IResponse
{
    public GetByGidClassResponse Obj { get; set; }
}
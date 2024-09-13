using Application.Features.AuthManagementFeatures.AuthPages.Queries.GetByGid;
using Application.Features.Base;
using Core.Application.Responses;


namespace Application.Features.AuthManagementFeatures.AuthPages.Commands.Create;

public class CreatedAuthPageResponse : BaseResponse, IResponse
{
    public GetByGidAuthPageResponse? Obj { get; set; }
}
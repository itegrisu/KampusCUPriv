using Application.Features.AuthManagementFeatures.AuthPages.Queries.GetByGid;
using Application.Features.Base;
using Core.Application.Responses;


namespace Application.Features.AuthManagementFeatures.AuthPages.Commands.Update;

public class UpdatedAuthPageResponse : BaseResponse, IResponse
{
    public GetByGidAuthPageResponse Obj { get; set; }
}
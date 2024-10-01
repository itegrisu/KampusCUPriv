using Application.Features.Base;
using Application.Features.OrganizationManagementFeatures.Organizations.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.OrganizationManagementFeatures.Organizations.Commands.Update;

public class UpdatedOrganizationResponse : BaseResponse, IResponse
{
    public GetByGidOrganizationResponse Obj { get; set; }
}
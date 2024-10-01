using Application.Features.Base;
using Application.Features.OrganizationManagementFeatures.OrganizationItems.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItems.Commands.Update;

public class UpdatedOrganizationItemResponse : BaseResponse, IResponse
{
    public GetByGidOrganizationItemResponse Obj { get; set; }
}
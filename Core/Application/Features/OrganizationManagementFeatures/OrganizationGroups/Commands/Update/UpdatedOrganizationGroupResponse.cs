using Application.Features.Base;
using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.OrganizationManagementFeatures.OrganizationGroups.Commands.Update;

public class UpdatedOrganizationGroupResponse : BaseResponse, IResponse
{
    public GetByGidOrganizationGroupResponse Obj { get; set; }
}
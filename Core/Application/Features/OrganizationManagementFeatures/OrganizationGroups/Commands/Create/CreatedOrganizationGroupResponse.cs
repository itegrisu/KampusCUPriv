using Application.Features.Base;
using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.OrganizationManagementFeatures.OrganizationGroups.Commands.Create;

public class CreatedOrganizationGroupResponse : BaseResponse, IResponse
{
    public GetByGidOrganizationGroupResponse Obj { get; set; }
}
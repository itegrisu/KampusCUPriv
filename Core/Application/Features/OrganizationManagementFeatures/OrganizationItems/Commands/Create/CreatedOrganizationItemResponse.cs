using Application.Features.Base;
using Application.Features.OrganizationManagementFeatures.OrganizationItems.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItems.Commands.Create;

public class CreatedOrganizationItemResponse : BaseResponse, IResponse
{
    public GetByGidOrganizationItemResponse Obj { get; set; }
}
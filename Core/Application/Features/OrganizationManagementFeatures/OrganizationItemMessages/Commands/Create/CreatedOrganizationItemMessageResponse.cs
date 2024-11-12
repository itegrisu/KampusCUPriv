using Application.Features.Base;
using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Commands.Create;

public class CreatedOrganizationItemMessageResponse : BaseResponse, IResponse
{
    public GetByGidOrganizationItemMessageResponse Obj { get; set; }
}
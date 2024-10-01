using Application.Features.Base;
using Application.Features.OrganizationManagementFeatures.Organizations.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.OrganizationManagementFeatures.Organizations.Commands.Create;

public class CreatedOrganizationResponse : BaseResponse, IResponse
{
    public GetByGidOrganizationResponse Obj { get; set; }
}
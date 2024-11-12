using Application.Features.Base;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.Create;

public class CreatedOrganizationItemFileResponse : BaseResponse, IResponse
{
    public GetByGidOrganizationItemFileResponse Obj { get; set; }
}
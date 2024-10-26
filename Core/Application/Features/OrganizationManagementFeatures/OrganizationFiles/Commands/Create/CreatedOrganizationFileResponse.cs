using Application.Features.Base;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.Create;

public class CreatedOrganizationFileResponse : BaseResponse, IResponse
{
    public GetByGidOrganizationFileResponse Obj { get; set; }
}
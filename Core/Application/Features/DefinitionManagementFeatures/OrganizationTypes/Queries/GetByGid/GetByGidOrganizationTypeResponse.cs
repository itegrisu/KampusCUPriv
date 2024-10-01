using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.OrganizationTypes.Queries.GetByGid
{
    public class GetByGidOrganizationTypeResponse : IResponse
    {
        public Guid Gid { get; set; }

public string Name { get; set; }

    }
}
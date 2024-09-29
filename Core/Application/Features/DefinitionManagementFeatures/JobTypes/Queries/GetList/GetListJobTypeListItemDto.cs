using Core.Application.Dtos;

namespace Application.Features.DefinitionManagementFeatures.JobTypes.Queries.GetList;

public class GetListJobTypeListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string Name { get; set; }


}
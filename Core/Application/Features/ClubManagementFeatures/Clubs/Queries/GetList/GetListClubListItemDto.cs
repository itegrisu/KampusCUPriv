using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.ClubFeatures.Clubs.Queries.GetList;

public class GetListClubListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid? GidManagerFK { get; set; }
    public string UserFKName { get; set; }
    public string UserFKLastName { get; set; }
    public Guid GidCategoryFK { get; set; }
    public string CategoryFKName { get; set; }
    public string Name { get; set; }
    public string? Logo { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }
    public int? MemberCount { get; set; }
    public int? EventCount { get; set; }
}
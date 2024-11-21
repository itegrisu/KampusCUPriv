using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.DefinitionManagementFeatures.Districts.Queries.GetList;

public class GetListDistrictListItemDto : IDto
{
    public Guid Gid { get; set; }
public Guid GidCityFK { get; set; }
public string CityFKName { get; set; }

public int DistrictCode { get; set; }
public string DistrictName { get; set; }


}
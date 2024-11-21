using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.Districts.Queries.GetByGid
{
    public class GetByGidDistrictResponse : IResponse
    {
        public Guid Gid { get; set; }
public Guid GidCityFK { get; set; }
public string CityFKName { get; set; }

public int DistrictCode { get; set; }
public string DistrictName { get; set; }

    }
}
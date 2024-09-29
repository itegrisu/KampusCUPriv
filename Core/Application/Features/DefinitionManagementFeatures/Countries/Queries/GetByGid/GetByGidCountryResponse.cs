using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.Countries.Queries.GetByGid
{
    public class GetByGidCountryResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string? PhoneCode { get; set; }
        public int RowNo { get; set; }

    }
}
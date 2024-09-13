using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.Countries.Queries.GetByGid
{
    public class GetByGidCountryResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string UlkeAdi { get; set; }
        public string UlkeKodu { get; set; }
        public string? TelefonKodu { get; set; }
        public int RowNo { get; set; }

    }
}
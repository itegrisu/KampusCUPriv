using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.Currencies.Queries.GetByGid
{
    public class GetByGidCurrencyResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string DovizAdi { get; set; }
        public string? DovizKodu { get; set; }
        public string? DovizSimgesi { get; set; }

    }
}
using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.Currencies.Queries.GetByGid
{
    public class GetByGidCurrencyResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }
        public string? Symbol { get; set; }

    }
}
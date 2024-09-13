using Core.Application.Responses;

namespace Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Queries.GetByGid
{
    public class GetByGidPersonnelAddressResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidPersonelFK { get; set; }
        public string UserFKTamAd { get; set; }
        public Guid GidSehirFK { get; set; }
        public string CityFKSehirAdi { get; set; }
        public string AdresBasligi { get; set; }
        public string Adres { get; set; }
        public string? Aciklama { get; set; }

    }
}
using Core.Application.Dtos;

namespace Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Queries.GetList;

public class GetListPersonnelAddressListItemDto : IDto
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
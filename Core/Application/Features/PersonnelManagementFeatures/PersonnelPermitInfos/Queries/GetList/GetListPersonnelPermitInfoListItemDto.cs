using Core.Application.Dtos;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Queries.GetList;

public class GetListPersonnelPermitInfoListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidPersonelFK { get; set; }
    public string UserFKTamAd { get; set; }
    public Guid GidPermitFK { get; set; }
    public string PermitTypeFKIzinAdi { get; set; }
    public DateTime IzinBaslamaTarihi { get; set; }
    public DateTime IzinBitisTarihi { get; set; }
    public string? Belge { get; set; }
    public string? Aciklama { get; set; }


}
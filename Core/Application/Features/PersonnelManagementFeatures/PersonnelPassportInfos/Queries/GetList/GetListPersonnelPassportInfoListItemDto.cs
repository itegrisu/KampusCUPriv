using Core.Application.Dtos;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Queries.GetList;

public class GetListPersonnelPassportInfoListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidPersonelFK { get; set; }
    public string UserFKTamAd { get; set; }
    public string PasaportNo { get; set; }
    public DateTime VerilisTarihi { get; set; }
    public DateTime GecerlilikTarihi { get; set; }
    public string? Belge { get; set; }
    public string? Aciklama { get; set; }


}
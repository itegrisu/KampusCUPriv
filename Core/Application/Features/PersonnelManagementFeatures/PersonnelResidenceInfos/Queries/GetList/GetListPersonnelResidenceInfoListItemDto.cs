using Core.Application.Dtos;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Queries.GetList;

public class GetListPersonnelResidenceInfoListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidPersonelFK { get; set; }
    public string UserFKTamAd { get; set; }
    public string OturumSeriNo { get; set; }
    public DateTime VerilisTarihi { get; set; }
    public DateTime GecerlilikTarihi { get; set; }
    public string? Belge { get; set; }
    public string? Aciklama { get; set; }


}
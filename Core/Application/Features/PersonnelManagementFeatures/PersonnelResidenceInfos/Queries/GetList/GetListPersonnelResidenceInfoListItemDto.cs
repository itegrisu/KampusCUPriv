using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Queries.GetList;

public class GetListPersonnelResidenceInfoListItemDto : IDto
{
    public Guid Gid { get; set; }
public Guid GidPersonelFK { get; set; }
public User UserFK { get; set; }

public string OturumSeriNo { get; set; }
public DateTime VerilisTarihi { get; set; }
public DateTime GecerlilikTarihi { get; set; }
public string? Belge { get; set; }
public string? Aciklama { get; set; }


}
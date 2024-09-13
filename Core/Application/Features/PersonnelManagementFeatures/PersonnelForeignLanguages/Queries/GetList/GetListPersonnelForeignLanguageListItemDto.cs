using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Queries.GetList;

public class GetListPersonnelForeignLanguageListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidPersonelFK { get; set; }
    public string UserFKTamAd { get; set; }
    public Guid GidLanguageFK { get; set; }
    public string ForeignLanguageFKDilAdi { get; set; }
    public EnumKonusmaDuzeyi KonusmaDuzeyi { get; set; }
    public EnumOkumaDuzeyi OkumaDuzeyi { get; set; }


}
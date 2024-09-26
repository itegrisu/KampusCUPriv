using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Queries.GetByGid
{
    public class GetByGidPersonnelForeignLanguageResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidPersonelFK { get; set; }
        public string UserFKTamAd { get; set; }
        public Guid GidLanguageFK { get; set; }
        public string ForeignLanguageFKDilAdi { get; set; }
        public EnumLanguageLevel KonusmaDuzeyi { get; set; }
        public EnumLanguageLevel OkumaDuzeyi { get; set; }

    }
}
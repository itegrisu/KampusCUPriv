using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Queries.GetByGid
{
    public class GetByGidPersonnelGraduatedSchoolResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidPersonelFK { get; set; }
        public string UserFKTamAd { get; set; }
        public EnumEgitimKurumuTuru EgitimKurumuTuru { get; set; }
        public string OkulBilgisi { get; set; }
        public string BolumBilgisi { get; set; }
        public int BaslamaYili { get; set; }
        public DateTime MezuniyetTarihi { get; set; }
        public string? Belge { get; set; }
        public string? Aciklama { get; set; }

    }
}
using Core.Entities;
using Domain.Entities.GeneralManagements;
using Domain.Enums;

namespace Domain.Entities.PersonnelManagements
{
    public class PersonnelGraduatedSchool : BaseEntity
    {
        public Guid GidPersonelFK { get; set; }
        public User UserFK { get; set; }
        public EnumEgitimKurumuTuru EgitimKurumuTuru { get; set; }
        public string OkulBilgisi { get; set; } = string.Empty;
        public string BolumBilgisi { get; set; } = string.Empty;
        public int BaslamaYili { get; set; }
        public DateTime? MezuniyetTarihi { get; set; }
        public string? Belge { get; set; }
        public string? Aciklama { get; set; }


    }
}

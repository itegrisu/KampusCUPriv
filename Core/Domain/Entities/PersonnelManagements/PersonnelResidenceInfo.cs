using Core.Entities;
using Domain.Entities.GeneralManagements;

namespace Domain.Entities.PersonnelManagements
{
    public class PersonnelResidenceInfo : BaseEntity
    {

        public Guid GidPersonelFK { get; set; }
        public User UserFK { get; set; }
        public string OturumSeriNo { get; set; } = string.Empty;
        public DateTime VerilisTarihi { get; set; }
        public DateTime GecerlilikTarihi { get; set; }
        public string? Belge { get; set; }
        public string? Aciklama { get; set; }

    }
}

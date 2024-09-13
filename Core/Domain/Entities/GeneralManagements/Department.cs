using Core.Entities;

namespace Domain.Entities.GeneralManagements
{

    public class Department : BaseEntity
    {
        public Guid GidAsilYoneticiFK { get; set; }
        public User AsilYoneticFK { get; set; }
        public Guid? GidYedekYoneticiFK { get; set; }
        public User? YedekYoneticiFK { get; set; }

        public string DepartmanAdi { get; set; } = string.Empty;
        public string? Detay { get; set; }

        public ICollection<DepartmentUser>? DepartmentUsers { get; set; }
    }
}

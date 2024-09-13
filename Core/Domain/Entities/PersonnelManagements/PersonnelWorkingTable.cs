using Core.Entities;
using Domain.Entities.GeneralManagements;

namespace Domain.Entities.PersonnelManagements
{
    public class PersonnelWorkingTable : BaseEntity
    {

        public Guid GidPersonelFK { get; set; }
        public User UserFK { get; set; }
        public DateTime IseBaslamaTarihi { get; set; }
        public DateTime? IstenCikisTarihi { get; set; }
    }
}

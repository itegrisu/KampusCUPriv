using Core.Entities;
using Domain.Entities.GeneralManagements;

namespace Domain.Entities.PersonnelManagements
{
    public class PersonnelWorkingTable : BaseEntity
    {

        public Guid GidPersonnelFK { get; set; }
        public User UserFK { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? ExitDate { get; set; }
    }
}

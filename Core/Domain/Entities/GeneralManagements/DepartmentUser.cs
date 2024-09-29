using Core.Entities;

namespace Domain.Entities.GeneralManagements
{
    public class DepartmentUser : BaseEntity
    {
        public Guid GidDepartmentFK { get; set; }
        public Department DepartmentFK { get; set; }
        public Guid GidPersonnelFK { get; set; }
        public User UserFK { get; set; }
    }
}

using Core.Entities;

namespace Domain.Entities.GeneralManagements
{
    public class DepartmentUser : BaseEntity
    {
        public Guid GidDepartmanFK { get; set; }
        public Department DepartmentFK { get; set; }
        public Guid GidPersonelFK { get; set; }
        public User UserFK { get; set; }
    }
}

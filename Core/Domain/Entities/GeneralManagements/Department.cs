using Core.Entities;

namespace Domain.Entities.GeneralManagements
{

    public class Department : BaseEntity
    {
        public Guid GidMainAdminFK { get; set; }
        public User MainAdminFK { get; set; }
        public Guid? GidCoAdminFK { get; set; }
        public User? CoAdminFK { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? Details { get; set; }

        public ICollection<DepartmentUser>? DepartmentUsers { get; set; }
    }
}

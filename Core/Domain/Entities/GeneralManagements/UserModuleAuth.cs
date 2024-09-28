using Core.Entities;
using Domain.Enums;

namespace Domain.Entities.GeneralManagements
{
    public class UserModuleAuth : BaseEntity
    {

        public Guid GidUserFK { get; set; }
        public User UserFK { get; set; }
        public EnumModuleType ModuleType { get; set; }


    }
}

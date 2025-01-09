using Core.Entities;
using Domain.Entities.GeneralManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DefinitionManagements
{
    public class Class : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<User>? Users { get; set; }
    }
}

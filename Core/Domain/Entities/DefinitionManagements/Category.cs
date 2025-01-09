using Core.Entities;
using Domain.Entities.ClubManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DefinitionManagements
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<Club>? Clubs { get; set; }
    }
}

using Domain.Entities.DefinitionManagements;
using Domain.Entities.GeneralManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.OtherDtos
{
    public class UserLevelDto
    {
     
        public Guid Gid { get; set; }
        public string LevelFKLevelName { get; set; }
    }
}

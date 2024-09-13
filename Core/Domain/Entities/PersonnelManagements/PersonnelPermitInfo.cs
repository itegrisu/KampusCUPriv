using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.GeneralManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.PersonnelManagements
{
    public class PersonnelPermitInfo : BaseEntity
    {
        public Guid GidPersonelFK { get; set; }
        public User UserFK { get; set; }
        public Guid GidPermitFK { get; set; }
        public PermitType PermitTypeFK { get; set; }
        public DateTime IzinBaslamaTarihi { get; set; }
        public DateTime IzinBitisTarihi { get; set; }
        public string? Belge { get; set; }
        public string? Aciklama { get; set; }


    }
}

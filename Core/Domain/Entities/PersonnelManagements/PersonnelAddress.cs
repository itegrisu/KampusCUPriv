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
    public class PersonnelAddress : BaseEntity
    {
        public Guid GidPersonelFK { get; set; }
        public User UserFK { get; set; }
        public Guid GidSehirFK { get; set; }
        public City CityFK { get; set; }
        public string AdresBasligi { get; set; } = string.Empty;
        public string Adres { get; set; } = string.Empty;
        public string? Aciklama { get; set; }


    }
}

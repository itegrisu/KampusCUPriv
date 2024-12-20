using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AccommodationManagements
{
    public class Guest : BaseEntity
    {
        public Guid? GidNationalityFK { get; set; }
        public Country? CountryFK { get; set; }
        public string IdNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surename { get; set; } = string.Empty;
        public string? Duty { get; set; }
        public string? Institution { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public EnumGender Gender { get; set; }
        public string? HesCode { get; set; }
        public string? Description { get; set; }

        public ICollection<AccommodationDate>? AccommodationDates { get; set; }
    }
}

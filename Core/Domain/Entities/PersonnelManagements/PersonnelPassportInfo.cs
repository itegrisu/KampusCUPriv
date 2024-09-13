using Core.Entities;
using Domain.Entities.GeneralManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.PersonnelManagements
{
    public class PersonnelPassportInfo : BaseEntity
    {
        public Guid GidPersonelFK { get; set; }
        public User UserFK { get; set; }
        public string PasaportNo { get; set; } = string.Empty;
        public DateTime VerilisTarihi { get; set; }
        public DateTime GecerlilikTarihi { get; set; }
        public string? Belge { get; set; }
        public string? Aciklama { get; set; }



    }

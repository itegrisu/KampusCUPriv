using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DefinitionManagements
{
    public class Currency : BaseEntity
    {
        public string DovizAdi { get; set; } = string.Empty;
        public string? DovizKodu { get; set; }
        public string? DovizSimgesi { get; set; }
    }
}

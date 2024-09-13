using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DefinitionManagements
{
    public class RoomType : BaseEntity
    {
        public string OdaTuru { get; set; } = string.Empty;
        public string OdaKodu { get; set; } = string.Empty;
        public int KisiSayisi { get; set; }
        public string? Aciklama { get; set; }
    }
}

using Core.Entities;
using Domain.Entities.ClubManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.GeneralManagements
{
    public class Admin : BaseEntity
    {
        public Guid GidClubFK { get; set; }
        public Club ClubFK { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

using Core.Entities;
using Domain.Entities.GeneralManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ClubManagements
{
    public class StudentClub : BaseEntity
    {
        public Guid GidUserFK { get; set; }
        public User UserFK { get; set; }
        public Guid GidClubFK { get; set; }
        public Club ClubFK { get; set; }
    }
}

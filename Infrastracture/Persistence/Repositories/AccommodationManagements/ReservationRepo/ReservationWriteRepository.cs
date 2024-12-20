using Application.Repositories.AccommodationManagements.ReservationRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.AccommodationManagements.ReservationRepo
{
    public class ReservationWriteRepository : WriteRepository<Reservation>, IReservationWriteRepository
    {
        private readonly Emasist2024Context _context;
        public ReservationWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

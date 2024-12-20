using Application.Repositories.AccommodationManagements.ReservationRoomRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.AccommodationManagements.ReservationRoomRepo
{
    public class ReservationRoomWriteRepository : WriteRepository<ReservationRoom>, IReservationRoomWriteRepository
    {
        private readonly Emasist2024Context _context;
        public ReservationRoomWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

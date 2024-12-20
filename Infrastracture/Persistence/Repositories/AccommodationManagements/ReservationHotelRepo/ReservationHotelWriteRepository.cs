using Application.Repositories.AccommodationManagements.ReservationHotelRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.AccommodationManagements.ReservationHotelRepo
{
    public class ReservationHotelWriteRepository : WriteRepository<ReservationHotel>, IReservationHotelWriteRepository
    {
        private readonly Emasist2024Context _context;
        public ReservationHotelWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

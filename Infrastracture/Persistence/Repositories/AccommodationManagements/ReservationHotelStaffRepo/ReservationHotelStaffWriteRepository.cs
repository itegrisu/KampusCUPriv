using Application.Repositories.AccommodationManagements.ReservationHotelStaffRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.AccommodationManagements.ReservationHotelStaffRepo
{
    public class ReservationHotelStaffWriteRepository : WriteRepository<ReservationHotelStaff>, IReservationHotelStaffWriteRepository
    {
        private readonly Emasist2024Context _context;
        public ReservationHotelStaffWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

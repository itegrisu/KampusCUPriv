using Application.Repositories.AccommodationManagements.GuestAccommodationRoomRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.AccommodationManagements.GuestAccommodationRoomRepo
{
    public class GuestAccommodationRoomWriteRepository : WriteRepository<GuestAccommodationRoom>, IGuestAccommodationRoomWriteRepository
    {
        private readonly Emasist2024Context _context;
        public GuestAccommodationRoomWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

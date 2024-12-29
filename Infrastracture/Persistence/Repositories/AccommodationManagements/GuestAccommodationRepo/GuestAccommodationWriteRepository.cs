using Application.Repositories.AccommodationManagements.GuestAccommodationRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.AccommodationManagements.GuestAccommodationRepo
{
    public class GuestAccommodationWriteRepository : WriteRepository<GuestAccommodation>, IGuestAccommodationWriteRepository
    {
        private readonly Emasist2024Context _context;
        public GuestAccommodationWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

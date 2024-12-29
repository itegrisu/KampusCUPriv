using Application.Repositories.AccommodationManagements.GuestAccommodationResultRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.AccommodationManagements.GuestAccommodationResultRepo
{
    public class GuestAccommodationResultWriteRepository : WriteRepository<GuestAccommodationResult>, IGuestAccommodationResultWriteRepository
    {
        private readonly Emasist2024Context _context;
        public GuestAccommodationResultWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

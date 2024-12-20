using Application.Repositories.AccommodationManagements.AccommodationDateRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.AccommodationManagements.AccommodationDateRepo
{
    public class AccommodationDateWriteRepository : WriteRepository<AccommodationDate>, IAccommodationDateWriteRepository
    {
        private readonly Emasist2024Context _context;
        public AccommodationDateWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

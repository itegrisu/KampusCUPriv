using Application.Repositories.AccommodationManagements.GuestRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.AccommodationManagements.GuestRepo
{
    public class GuestWriteRepository : WriteRepository<Guest>, IGuestWriteRepository
    {
        private readonly Emasist2024Context _context;
        public GuestWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

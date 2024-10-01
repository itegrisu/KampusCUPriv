using Application.Repositories.OfferManagementRepos.OfferRepo;
using Core.Repositories.Concretes;
using Domain.Entities.OfferManagements;
using Persistence.Context;

namespace Persistence.Repositories.OfferManagementRepos.OfferRepo
{
    public class OfferWriteRepository : WriteRepository<Offer>, IOfferWriteRepository
    {
        private readonly Emasist2024Context _context;
        public OfferWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

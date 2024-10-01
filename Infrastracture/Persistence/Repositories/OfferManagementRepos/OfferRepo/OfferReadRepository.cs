using Application.Repositories.OfferManagementRepos.OfferRepo;
using Core.Repositories.Concretes;
using Domain.Entities.OfferManagements;
using Persistence.Context;

namespace Persistence.Repositories.OfferManagementRepos.OfferRepo
{

    public class OfferReadRepository : ReadRepository<Offer>, IOfferReadRepository
    {
        private readonly Emasist2024Context _context;
        public OfferReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

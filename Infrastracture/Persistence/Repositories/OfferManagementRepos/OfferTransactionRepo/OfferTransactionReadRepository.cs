using Application.Repositories.OfferManagementRepos.OfferTransactionRepo;
using Core.Repositories.Concretes;
using Domain.Entities.OfferManagements;
using Persistence.Context;

namespace Persistence.Repositories.OfferManagementRepos.OfferTransactionRepo
{

    public class OfferTransactionReadRepository : ReadRepository<OfferTransaction>, IOfferTransactionReadRepository
    {
        private readonly Emasist2024Context _context;
        public OfferTransactionReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

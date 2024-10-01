using Application.Repositories.OfferManagementRepos.OfferTransactionRepo;
using Core.Repositories.Concretes;
using Domain.Entities.OfferManagements;
using Persistence.Context;

namespace Persistence.Repositories.OfferManagementRepos.OfferTransactionRepo
{
    public class OfferTransactionWriteRepository : WriteRepository<OfferTransaction>, IOfferTransactionWriteRepository
    {
        private readonly Emasist2024Context _context;
        public OfferTransactionWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

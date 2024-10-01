using Application.Repositories.OfferManagementRepos.OfferFileRepo;
using Core.Repositories.Concretes;
using Domain.Entities.OfferManagements;
using Persistence.Context;

namespace Persistence.Repositories.OfferManagementRepos.OfferFileRepo
{

    public class OfferFileReadRepository : ReadRepository<OfferFile>, IOfferFileReadRepository
    {
        private readonly Emasist2024Context _context;
        public OfferFileReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

using Application.Repositories.SupportManagementRepos.SupportMessageDetailRepo;
using Core.Repositories.Concretes;
using Domain.Entities.SupportManagements;
using Persistence.Context;

namespace Persistence.Repositories.SupportManagementRepos.SupportMessageDetailRepo
{

    public class SupportMessageDetailReadRepository : ReadRepository<SupportMessageDetail>, ISupportMessageDetailReadRepository
    {
        private readonly Emasist2024Context _context;
        public SupportMessageDetailReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

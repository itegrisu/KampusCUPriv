using Application.Repositories.SupportManagementRepos.SupportMessageRepo;
using Core.Repositories.Concretes;
using Domain.Entities.SupportManagements;
using Persistence.Context;

namespace Persistence.Repositories.SupportManagementRepos.SupportMessageRepo
{
    public class SupportMessageWriteRepository : WriteRepository<SupportMessage>, ISupportMessageWriteRepository
    {
        private readonly Emasist2024Context _context;
        public SupportMessageWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

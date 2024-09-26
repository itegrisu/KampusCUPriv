using Application.Repositories.SupportManagementRepos.SupportMessageDetailRepo;
using Core.Repositories.Concretes;
using Domain.Entities.SupportManagements;
using Persistence.Context;

namespace Persistence.Repositories.SupportManagementRepos.SupportMessageDetailRepo
{
    public class SupportMessageDetailWriteRepository : WriteRepository<SupportMessageDetail>, ISupportMessageDetailWriteRepository
    {
        private readonly Emasist2024Context _context;
        public SupportMessageDetailWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

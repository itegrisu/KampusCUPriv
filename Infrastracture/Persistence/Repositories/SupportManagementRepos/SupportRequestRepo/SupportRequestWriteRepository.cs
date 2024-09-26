using Application.Repositories.SupportManagementRepos.SupportRequestRepo;
using Core.Repositories.Concretes;
using Domain.Entities.SupportManagements;
using Persistence.Context;

namespace Persistence.Repositories.SupportManagementRepos.SupportRequestRepo
{
    public class SupportRequestWriteRepository : WriteRepository<SupportRequest>, ISupportRequestWriteRepository
    {
        private readonly Emasist2024Context _context;
        public SupportRequestWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

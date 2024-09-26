using Application.Repositories.SupportManagementRepos.SupportRequestRepo;
using Core.Repositories.Concretes;
using Domain.Entities.SupportManagements;
using Persistence.Context;

namespace Persistence.Repositories.SupportManagementRepos.SupportRequestRepo
{

    public class SupportRequestReadRepository : ReadRepository<SupportRequest>, ISupportRequestReadRepository
    {
        private readonly Emasist2024Context _context;
        public SupportRequestReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

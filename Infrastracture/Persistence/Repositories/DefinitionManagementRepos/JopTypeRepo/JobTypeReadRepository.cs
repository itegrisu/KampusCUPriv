using Application.Repositories.DefinitionManagementRepos.JopTypeRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.JopTypeRepo
{

    public class JobTypeReadRepository : ReadRepository<JobType>, IJobTypeReadRepository
    {
        private readonly Emasist2024Context _context;
        public JobTypeReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

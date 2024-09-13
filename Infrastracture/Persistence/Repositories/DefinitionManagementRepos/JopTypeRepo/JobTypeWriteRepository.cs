using Application.Repositories.DefinitionManagementRepos.JopTypeRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.JopTypeRepo
{
    public class JobTypeWriteRepository : WriteRepository<JobType>, IJobTypeWriteRepository
    {
        private readonly Emasist2024Context _context;
        public JobTypeWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

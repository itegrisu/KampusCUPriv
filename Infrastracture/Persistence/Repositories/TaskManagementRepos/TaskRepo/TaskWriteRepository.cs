using Application.Repositories.TaskManagementRepos.TaskRepo;
using Core.Repositories.Concretes;
using Persistence.Context;
using T = Domain.Entities.TaskManagements;

namespace Persistence.Repositories.TaskManagementRepos.TaskRepo
{
    public class TaskWriteRepository : WriteRepository<T.Task>, ITaskWriteRepository
    {
        private readonly Emasist2024Context _context;
        public TaskWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

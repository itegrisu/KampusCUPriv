using Application.Repositories.TaskManagementRepos.TaskRepo;
using Core.Repositories.Concretes;
using Persistence.Context;
using T = Domain.Entities.TaskManagements;

namespace Persistence.Repositories.TaskManagementRepos.TaskRepo
{
    public class TaskReadRepository : ReadRepository<T.Task>, ITaskReadRepository
    {
        private readonly Emasist2024Context _context;
        public TaskReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

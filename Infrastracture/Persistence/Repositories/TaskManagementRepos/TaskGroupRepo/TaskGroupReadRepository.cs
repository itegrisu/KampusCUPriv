using Application.Repositories.TaskManagementRepos.TaskGroupRepo;
using Core.Repositories.Concretes;
using Domain.Entities.TaskManagements;
using Persistence.Context;

namespace Persistence.Repositories.TaskManagementRepos.TaskGroupRepo
{
    public class TaskGroupReadRepository : ReadRepository<TaskGroup>, ITaskGroupReadRepository
    {
        private readonly Emasist2024Context _context;
        public TaskGroupReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

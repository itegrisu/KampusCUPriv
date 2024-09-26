using Application.Repositories.TaskManagementRepos.TaskManagerRepo;
using Core.Repositories.Concretes;
using Domain.Entities.TaskManagements;
using Persistence.Context;

namespace Persistence.Repositories.TaskManagementRepos.TaskManagerRepo
{
    public class TaskManagerReadRepository : ReadRepository<TaskManager>, ITaskManagerReadRepository
    {
        private readonly Emasist2024Context _context;
        public TaskManagerReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

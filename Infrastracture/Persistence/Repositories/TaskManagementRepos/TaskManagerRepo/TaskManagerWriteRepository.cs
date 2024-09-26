using Application.Repositories.TaskManagementRepos.TaskManagerRepo;
using Core.Repositories.Concretes;
using Domain.Entities.TaskManagements;
using Persistence.Context;

namespace Persistence.Repositories.TaskManagementRepos.TaskManagerRepo
{
    public class TaskManagerWriteRepository : WriteRepository<TaskManager>, ITaskManagerWriteRepository
    {
        private readonly Emasist2024Context _context;
        public TaskManagerWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

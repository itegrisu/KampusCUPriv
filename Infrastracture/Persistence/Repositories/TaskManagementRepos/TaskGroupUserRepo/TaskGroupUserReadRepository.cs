using Application.Repositories.TaskManagementRepos.TaskGroupUserRepo;
using Core.Repositories.Concretes;
using Domain.Entities.TaskManagements;
using Persistence.Context;

namespace Persistence.Repositories.TaskManagementRepos.TaskGroupUserRepo
{
    public class TaskGroupUserReadRepository : ReadRepository<TaskGroupUser>, ITaskGroupUserReadRepository
    {
        private readonly Emasist2024Context _context;
        public TaskGroupUserReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

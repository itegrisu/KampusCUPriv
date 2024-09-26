using Application.Repositories.TaskManagementRepos.TaskUserRepo;
using Core.Repositories.Concretes;
using Domain.Entities.TaskManagements;
using Persistence.Context;

namespace Persistence.Repositories.TaskManagementRepos.TaskUserRepo
{
    public class TaskUserReadRepository : ReadRepository<TaskUser>, ITaskUserReadRepository
    {
        private readonly Emasist2024Context _context;
        public TaskUserReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

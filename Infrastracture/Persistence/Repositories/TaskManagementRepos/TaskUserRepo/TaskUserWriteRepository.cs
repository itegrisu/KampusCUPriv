using Application.Repositories.TaskManagementRepos.TaskUserRepo;
using Core.Repositories.Concretes;
using Domain.Entities.TaskManagements;
using Persistence.Context;

namespace Persistence.Repositories.TaskManagementRepos.TaskUserRepo
{
    public class TaskUserWriteRepository : WriteRepository<TaskUser>, ITaskUserWriteRepository
    {
        private readonly Emasist2024Context _context;
        public TaskUserWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

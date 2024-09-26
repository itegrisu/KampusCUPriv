using Application.Repositories.TaskManagementRepos.TaskFileRepo;
using Core.Repositories.Concretes;
using Domain.Entities.TaskManagements;
using Persistence.Context;

namespace Persistence.Repositories.TaskManagementRepos.TaskFileRepo
{
    public class TaskFileReadRepository : ReadRepository<TaskFile>, ITaskFileReadRepository
    {
        private readonly Emasist2024Context _context;
        public TaskFileReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

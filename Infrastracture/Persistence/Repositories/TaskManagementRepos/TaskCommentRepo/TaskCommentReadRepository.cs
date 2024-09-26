using Application.Repositories.TaskManagementRepos.TaskCommentRepo;
using Core.Repositories.Concretes;
using Domain.Entities.TaskManagements;
using Persistence.Context;

namespace Persistence.Repositories.TaskManagementRepos.TaskCommentRepo
{
    public class TaskCommentReadRepository : ReadRepository<TaskComment>, ITaskCommentReadRepository
    {
        private readonly Emasist2024Context _context;
        public TaskCommentReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

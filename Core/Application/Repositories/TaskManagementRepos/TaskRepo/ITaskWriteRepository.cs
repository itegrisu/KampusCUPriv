using Core.Repositories.Abstracts;
using T = Domain.Entities.TaskManagements;

namespace Application.Repositories.TaskManagementRepos.TaskRepo
{
    public interface ITaskWriteRepository : IWriteRepository<T.Task>
    {

    }
}

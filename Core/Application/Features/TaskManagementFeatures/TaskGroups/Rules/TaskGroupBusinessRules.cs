using Application.Features.TaskManagementFeatures.TaskGroups.Constants;
using Application.Repositories.TaskManagementRepos.TaskGroupRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.TaskManagements;

namespace Application.Features.TaskManagementFeatures.TaskGroups.Rules;

public class TaskGroupBusinessRules : BaseBusinessRules
{
    private readonly ITaskGroupReadRepository _taskGroupReadRepository;

    public TaskGroupBusinessRules(ITaskGroupReadRepository taskGroupReadRepository)
    {
        _taskGroupReadRepository = taskGroupReadRepository;
    }

    public async Task TaskGroupShouldExistWhenSelected(Guid taskGroupGid)
    {
        if (await _taskGroupReadRepository.GetAsync(predicate: tg => tg.Gid == taskGroupGid) == null)
            throw new BusinessException(TaskGroupsBusinessMessages.TaskGroupNotExists);
    }

    public async Task<bool> HasTaskGroupUser(X.TaskGroup taskGroup)
    {
        if (taskGroup.TaskGroupUsers.Where(x => x.DataState == Core.Enum.DataState.Active).Count() > 0)
            return true;

        return false;
    }

}
using Application.Features.TaskManagementFeatures.TaskGroups.Constants;
using Application.Features.TaskManagementFeatures.TaskManagers.Constants;
using Application.Repositories.TaskManagementRepos.TaskManagerRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using T = Domain.Entities.TaskManagements;
using X = Domain.Entities.TaskManagements;
using Domain.Entities.GeneralManagements;
using Application.Repositories.GeneralManagementRepos.UserRepo;

namespace Application.Features.TaskManagementFeatures.TaskManagers.Rules;

public class TaskManagerBusinessRules : BaseBusinessRules
{
    private readonly ITaskManagerReadRepository _taskManagerReadRepository;
    private readonly IUserReadRepository _userReadRepository;

    public TaskManagerBusinessRules(ITaskManagerReadRepository taskManagerReadRepository, IUserReadRepository userReadRepository)
    {
        _taskManagerReadRepository = taskManagerReadRepository;
        _userReadRepository = userReadRepository;
    }

    public async Task TaskManagerShouldExistWhenSelected(Guid taskManagerGid)
    {
        if (await _taskManagerReadRepository.GetAsync(predicate: tm => tm.Gid == taskManagerGid) == null)
            throw new BusinessException(TaskManagersBusinessMessages.TaskManagerNotExists);
    }

    public async Task TaskGroupShouldExistWhenSelected(X.TaskGroup? item)
    {
        if (item == null)
            throw new BusinessException(TaskManagersBusinessMessages.TaskGroupNotExists);
    }

    public async Task IsTaskManagerExist(Guid gidUserFK)
    {
        if (await _taskManagerReadRepository.GetSingleAsync(x => x.GidUserFK == gidUserFK && x.DataState == Core.Enum.DataState.Active) != null)
            throw new Exception(TaskManagersBusinessMessages.TaskManagerAlreadyExist);
    }

    public async Task UserShouldExistWhenSelected(Guid userGid)
    {
        if (await _userReadRepository.GetAsync(predicate: u => u.Gid == userGid) == null)
            throw new BusinessException(TaskManagersBusinessMessages.UserNotExists);
    }

}
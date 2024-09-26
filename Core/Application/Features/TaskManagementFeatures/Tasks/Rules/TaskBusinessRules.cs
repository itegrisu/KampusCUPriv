using Application.Features.TaskManagementFeatures.TaskManagers.Constants;
using Application.Features.TaskManagementFeatures.Tasks.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.TaskManagementRepos.TaskManagerRepo;
using Application.Repositories.TaskManagementRepos.TaskRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.GeneralManagements;
using T = Domain.Entities.TaskManagements;

namespace Application.Features.TaskManagementFeatures.Tasks.Rules;

public class TaskBusinessRules : BaseBusinessRules
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly ITaskReadRepository _taskReadRepository;
    private readonly ITaskManagerReadRepository _taskManagerReadRepository;

    public TaskBusinessRules(IUserReadRepository userReadRepository, ITaskReadRepository taskReadRepository, ITaskManagerReadRepository taskManagerReadRepository)
    {
        _userReadRepository = userReadRepository;
        _taskReadRepository = taskReadRepository;
        _taskManagerReadRepository = taskManagerReadRepository;
    }

    public async Task TaskShouldExistWhenSelected(Guid taskGid)
    {
        if (await _taskReadRepository.GetAsync(predicate: t => t.Gid == taskGid) == null)
            throw new BusinessException(TasksBusinessMessages.TaskNotExists);
    }
    public async Task UserShouldExistWhenSelected(Guid userGid)
    {
        if (await _userReadRepository.GetAsync(predicate: u => u.Gid == userGid) == null)
            throw new BusinessException(TasksBusinessMessages.UserNotExists);
    }

    public async Task isTaskManager(Guid userGid)
    {
        User user = await _userReadRepository.GetAsync(predicate: u => u.Gid == userGid);
        if (user == null)
            throw new BusinessException(TasksBusinessMessages.UserNotExists);

        var taskManagers = _taskManagerReadRepository.GetAll();
        if (taskManagers.Any(tm => tm.GidUserFK == user.Gid))
            return;
        else
            throw new BusinessException(TaskManagersBusinessMessages.UserIsNotTaskManager);
    }

}
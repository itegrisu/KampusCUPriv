using Application.Features.TaskManagementFeatures.Tasks.Constants;
using Application.Features.TaskManagementFeatures.TaskUsers.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.TaskManagementRepos.TaskRepo;
using Application.Repositories.TaskManagementRepos.TaskUserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.GeneralManagements;
using X = Domain.Entities.TaskManagements;

namespace Application.Features.TaskManagementFeatures.TaskUsers.Rules;

public class TaskUserBusinessRules : BaseBusinessRules
{
    private readonly ITaskUserReadRepository _taskUserReadRepository;
    private readonly IUserReadRepository _userReadRepository;
    private readonly ITaskReadRepository _taskReadRepository;

    public TaskUserBusinessRules(ITaskUserReadRepository taskUserReadRepository, IUserReadRepository userReadRepository, ITaskReadRepository taskReadRepository)
    {
        _taskUserReadRepository = taskUserReadRepository;
        _userReadRepository = userReadRepository;
        _taskReadRepository = taskReadRepository;
    }

    public async Task TaskUserShouldExistWhenSelected(Guid taskUserGid)
    {
        if (await _taskUserReadRepository.GetAsync(predicate: tu => tu.Gid == taskUserGid) == null)
            throw new BusinessException(TaskUsersBusinessMessages.TaskUserNotExists);
    }

    public async Task UserShouldExistWhenSelected(Guid userGid)
    {
        if (await _userReadRepository.GetAsync(predicate: u => u.Gid == userGid) == null)
            throw new BusinessException(TaskUsersBusinessMessages.UserNotExists);
    }

    public async Task TaskShouldExistWhenSelected(Guid taskGid)
    {
        if (await _taskReadRepository.GetAsync(predicate: t => t.Gid == taskGid) == null)
            throw new BusinessException(TaskUsersBusinessMessages.TaskNotExists);
    }

    public async Task TaskUserShouldNotExistWhenSelected(Guid userGid, Guid taskGid)
    {
        if (await _taskUserReadRepository.GetAsync(predicate: tu => tu.GidUserFK == userGid && tu.GidTaskFK == taskGid) != null)
            throw new BusinessException(TaskUsersBusinessMessages.TaskUserAlreadyAssigned);
    }

}
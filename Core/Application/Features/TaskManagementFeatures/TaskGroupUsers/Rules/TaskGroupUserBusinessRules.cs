using Application.Features.TaskManagementFeatures.TaskGroups.Constants;
using Application.Features.TaskManagementFeatures.TaskGroupUsers.Constants;
using Application.Features.TaskManagementFeatures.Tasks.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.TaskManagementRepos.TaskGroupRepo;
using Application.Repositories.TaskManagementRepos.TaskGroupUserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.GeneralManagements;
using X = Domain.Entities.TaskManagements;

namespace Application.Features.TaskManagementFeatures.TaskGroupUsers.Rules;

public class TaskGroupUserBusinessRules : BaseBusinessRules
{
    private readonly ITaskGroupUserReadRepository _taskGroupUserReadRepository;
    private readonly ITaskGroupReadRepository _taskGroupReadRepository;
    private readonly IUserReadRepository _userReadRepository;


    public TaskGroupUserBusinessRules(ITaskGroupUserReadRepository taskGroupUserReadRepository, ITaskGroupReadRepository taskGroupReadRepository, IUserReadRepository userReadRepository)
    {
        _taskGroupUserReadRepository = taskGroupUserReadRepository;
        _taskGroupReadRepository = taskGroupReadRepository;
        _userReadRepository = userReadRepository;
    }

    public async Task TaskGroupUserShouldExistWhenSelected(Guid taskGroupUserGid)
    {
        if (await _taskGroupUserReadRepository.GetAsync(predicate: tgu => tgu.Gid == taskGroupUserGid) == null)
            throw new BusinessException(TaskGroupUsersBusinessMessages.TaskGroupUserNotExists);
    }

    public async Task TaskGroupShouldExistWhenSelected(Guid taskGroupGid)
    {
        if (await _taskGroupReadRepository.GetAsync(predicate: tg => tg.Gid == taskGroupGid) == null)
            throw new BusinessException(TaskGroupUsersBusinessMessages.TaskGroupNotExists);
    }

    public async Task GroupUserAlreadyExist(Guid GidUserFK, Guid GidTaskGroupFK)
    {
        int count = (await _taskGroupUserReadRepository.GetListAsync(predicate: x => x.GidUserFK == GidUserFK && x.GidTaskGroupFK == GidTaskGroupFK)).Count;
        if (count > 0)
            throw new BusinessException(TaskGroupUsersBusinessMessages.GroupUserAlreadyExist);
    }

    public async Task UserShouldExistWhenSelected(Guid userGid)
    {
        if (await _userReadRepository.GetAsync(predicate: u => u.Gid == userGid) == null)
            throw new BusinessException(TaskGroupUsersBusinessMessages.UserNotExists);

    }

    public async Task TaskShouldExistWhenSelected(X.Task? task)
    {
        if (task == null)
            throw new BusinessException(TaskGroupUsersBusinessMessages.TaskNotExists);
    }

}
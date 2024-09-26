using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Features.TaskManagementFeatures.TaskComments.Constants;
using Application.Features.TaskManagementFeatures.TaskUsers.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.TaskManagementRepos.TaskCommentRepo;
using Application.Repositories.TaskManagementRepos.TaskRepo;
using Application.Repositories.TaskManagementRepos.TaskUserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using T = Domain.Entities.TaskManagements;

namespace Application.Features.TaskManagementFeatures.TaskComments.Rules;

public class TaskCommentBusinessRules : BaseBusinessRules
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly ITaskReadRepository _taskReadRepository;
    private readonly ITaskCommentReadRepository _taskCommentReadRepository;
    private readonly ITaskUserReadRepository _taskUserReadRepository;

    public TaskCommentBusinessRules(IUserReadRepository userReadRepository, ITaskReadRepository taskReadRepository, ITaskCommentReadRepository taskCommentReadRepository, ITaskUserReadRepository taskUserReadRepository)
    {
        _userReadRepository = userReadRepository;
        _taskReadRepository = taskReadRepository;
        _taskCommentReadRepository = taskCommentReadRepository;
        _taskUserReadRepository = taskUserReadRepository;
    }

    public async Task TaskCommentShouldExistWhenSelected(Guid taskCommentGid)
    {
        if (await _taskCommentReadRepository.GetAsync(predicate: tc => tc.Gid == taskCommentGid) == null)
            throw new BusinessException(TaskCommentsBusinessMessages.TaskCommentNotExists);
    }

    public async Task TaskUserShouldExistWhenSelected(Guid taskUserGid)
    {
        if (await _taskUserReadRepository.GetAsync(predicate: x => x.Gid == taskUserGid) == null)
            throw new BusinessException(TaskCommentsBusinessMessages.TaskUserNotExists);
    }

    public async Task UserShouldExistWhenSelected(Guid userGid)
    {
        if (await _userReadRepository.GetAsync(predicate: u => u.Gid == userGid) == null)
            throw new BusinessException(TaskCommentsBusinessMessages.UserNotExists);
    }

    public async Task TaskShouldExistWhenSelected(Guid taskGid)
    {
        if (await _taskReadRepository.GetAsync(predicate: x => x.Gid == taskGid) == null)
            throw new BusinessException(TaskCommentsBusinessMessages.TaskNotFound);
    }

}
using Application.Features.TaskManagementFeatures.Tasks.Constants;
using Application.Repositories.GeneralManagementRepos.UserModuleAuthRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.TaskManagementRepos.TaskRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.GeneralManagements;

namespace Application.Features.TaskManagementFeatures.Tasks.Rules;

public class TaskBusinessRules : BaseBusinessRules
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly ITaskReadRepository _taskReadRepository;
    private readonly IUserModuleAuthReadRepository _userModuleAuthReadRepository;

    public TaskBusinessRules(IUserReadRepository userReadRepository, ITaskReadRepository taskReadRepository, IUserModuleAuthReadRepository userModuleAuthReadRepository)
    {
        _userReadRepository = userReadRepository;
        _taskReadRepository = taskReadRepository;
        _userModuleAuthReadRepository = userModuleAuthReadRepository;
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

    public async Task IsTaskModuleAuth(Guid userGid)
    {
        UserModuleAuth? userModuleAuth = await _userModuleAuthReadRepository.GetAsync(predicate: x => x.GidUserFK == userGid && x.ModuleType == Domain.Enums.EnumModuleType.Gorev);

        if (userModuleAuth == null)
            throw new BusinessException(TasksBusinessMessages.UserNotAuthorized);

    }

}
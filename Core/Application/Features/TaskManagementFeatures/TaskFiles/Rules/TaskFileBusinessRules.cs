using Application.Abstractions.Storage;
using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Features.TaskManagementFeatures.TaskComments.Constants;
using Application.Features.TaskManagementFeatures.TaskFiles.Constants;
using Application.Features.TaskManagementFeatures.Tasks.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.TaskManagementRepos.TaskFileRepo;
using Application.Repositories.TaskManagementRepos.TaskRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.GeneralManagements;
using Microsoft.AspNetCore.Http;
using X = Domain.Entities.TaskManagements;

namespace Application.Features.TaskManagementFeatures.TaskFiles.Rules;

public class TaskFileBusinessRules : BaseBusinessRules
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly ITaskReadRepository _taskReadRepository;
    private readonly ITaskFileReadRepository _taskFileReadRepository;
    private readonly IFileTypeCheckService _fileTypeCheckService;

    public TaskFileBusinessRules(IUserReadRepository userReadRepository, ITaskReadRepository taskReadRepository, ITaskFileReadRepository taskFileReadRepository, IFileTypeCheckService fileTypeCheckService)
    {
        _userReadRepository = userReadRepository;
        _taskReadRepository = taskReadRepository;
        _taskFileReadRepository = taskFileReadRepository;
        _fileTypeCheckService = fileTypeCheckService;
    }

    public async Task TaskFileShouldExistWhenSelected(Guid taskFileGid)
    {
        if (await _taskFileReadRepository.GetAsync(predicate: x => x.Gid == taskFileGid) == null)
            throw new BusinessException(TaskFilesBusinessMessages.TaskFileNotExists);
    }

    public async Task UserShouldExistWhenSelected(Guid userGid)
    {
        if (await _userReadRepository.GetAsync(predicate: u => u.Gid == userGid) == null)
            throw new BusinessException(TaskFilesBusinessMessages.UserNotExists);
    }

    public async Task TaskShouldExistWhenSelected(Guid taskGid)
    {
        if (await _taskReadRepository.GetAsync(predicate: x => x.Gid == taskGid) == null)
            throw new BusinessException(TaskFilesBusinessMessages.TaskNotFound);
    }

    public async Task FileTypeCheck(string extension, string[] allowedExtensions, IFormFileCollection formFiles)
    {
        if (!await _fileTypeCheckService.CheckFileType(extension, allowedExtensions) || !await _fileTypeCheckService.CheckFileType(extension, formFiles[0]))
            throw new BusinessException(UsersBusinessMessages.IncorrectAvatarImageMessage);
    }

}
using Application.Abstractions.Storage;
using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Features.SupportManagementFeatures.SupportMessages.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.SupportManagementRepos.SupportRequestRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Microsoft.AspNetCore.Http;
using X = Domain.Entities.SupportManagements;

namespace Application.Features.SupportManagementFeatures.SupportMessages.Rules;

public class SupportMessageBusinessRules : BaseBusinessRules
{

    private readonly IUserReadRepository _userReadRepository;
    private readonly ISupportRequestReadRepository _supportRequestReadRepository;
    private readonly IFileTypeCheckService _fileTypeCheckService;

    public SupportMessageBusinessRules(IUserReadRepository userReadRepository, ISupportRequestReadRepository supportRequestReadRepository, IFileTypeCheckService fileTypeCheckService)
    {
        _userReadRepository = userReadRepository;
        _supportRequestReadRepository = supportRequestReadRepository;
        _fileTypeCheckService = fileTypeCheckService;
    }

    public async Task SupportMessageShouldExistWhenSelected(X.SupportMessage? item)
    {
        if (item == null)
            throw new BusinessException(SupportMessagesBusinessMessages.SupportMessageNotExists);
    }

    public async Task SupportRequestShouldExistWhenSelected(Guid gid)
    {
        if (await _supportRequestReadRepository.GetAsync(predicate: x => x.Gid == gid) == null)
            throw new BusinessException(SupportMessagesBusinessMessages.SupportRequestNotExists);
    }

    public async Task UserShouldExistWhenSelected(Guid gid)
    {
        if (await _userReadRepository.GetAsync(predicate: x => x.Gid == gid) == null)
            throw new BusinessException(SupportMessagesBusinessMessages.UserNotExists);
    }

    public async Task FileTypeCheck(string extension, string[] allowedExtensions, IFormFileCollection formFiles)
    {
        if (!await _fileTypeCheckService.CheckFileType(extension, allowedExtensions) || !await _fileTypeCheckService.CheckFileType(extension, formFiles[0]))
            throw new BusinessException(SupportMessagesBusinessMessages.IncorrectFileType);
    }


}
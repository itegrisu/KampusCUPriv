using Application.Abstractions.Storage;
using Application.Features.GeneralFeatures.Users.Constants;
using Application.Repositories.GeneralManagementRepo.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Microsoft.AspNetCore.Http;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralFeatures.Users.Rules;

public class UserBusinessRules : BaseBusinessRules
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IFileTypeCheckService _fileTypeCheckService;

    public UserBusinessRules(IUserReadRepository userReadRepository, IFileTypeCheckService fileTypeCheckService)
    {
        _userReadRepository = userReadRepository;
        _fileTypeCheckService = fileTypeCheckService;
    }
    public async Task UserShouldExistWhenSelected(X.User? item)
    {
        if (item == null)
            throw new BusinessException(UsersBusinessMessages.UserNotExists);
    }

    public async Task FileTypeCheck(string extension, string[] allowedExtensions, IFormFileCollection formFiles)
    {
        if (!await _fileTypeCheckService.CheckFileType(extension, allowedExtensions) || !await _fileTypeCheckService.CheckFileType(extension, formFiles[0]))
            throw new BusinessException(UsersBusinessMessages.IncorrectAvatarImageMessage);
    }
    public async Task UserAlreadyExist(string email)
    {
        var user = await _userReadRepository.GetSingleAsync(u => u.Email == email);
        if (user != null)
            throw new BusinessException(UsersBusinessMessages.UserAlreadyExists);
    }
    public async Task EmailDomainCheck(string email)
    {
        if (!email.EndsWith("@cumhuriyet.edu.tr"))
            throw new BusinessException("Only email addresses with the domain @cumhuriyet.edu.com.tr can be registered.");
    }
    public async Task UserEmailShouldBeUniqueWhenUpdating(Guid userId, string newEmail)
    {
        var user = await _userReadRepository.GetSingleAsync(u => u.Email == newEmail && u.Gid != userId);
        if (user != null)
            throw new BusinessException(UsersBusinessMessages.UserAlreadyExists);
    }
}
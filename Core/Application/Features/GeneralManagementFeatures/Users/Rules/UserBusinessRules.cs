using Application.Abstractions.Storage;
using Application.Features.GeneralFeatures.Users.Constants;
using Application.Repositories.GeneralManagementRepo.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralFeatures.Users.Rules;

public class UserBusinessRules : BaseBusinessRules
{
    IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
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
}
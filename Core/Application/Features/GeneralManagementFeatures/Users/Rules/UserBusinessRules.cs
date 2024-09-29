using Application.Abstractions.Storage;
using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.GeneralManagements;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Features.GeneralManagementFeatures.Users.Rules;

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
    public async Task UserCustomIdShouldExistWhenSelected(Guid userGid)
    {
        var user = await _userReadRepository.GetListAllAsync(predicate: u => u.Gid == userGid);
        if (user.Items.Count <= 0)
            throw new BusinessException(UsersBusinessMessages.UserNotExists);
    }
    public async Task IdNumberAlreadyExists(string IdNumber)
    {
        List<User> users = _userReadRepository.GetAll().ToList();
        if (users.Any(u => u.IdentityNo == IdNumber))
            throw new BusinessException(UsersBusinessMessages.IdNumberAlreadyExists);
    }

    public async Task PhoneNumberAlreadyExists(string? phoneNumber)
    {
        if (phoneNumber == null)
            return;

        List<User> users = _userReadRepository.GetAll().ToList();
        if (users.Any(u => u.Gsm == phoneNumber))
            throw new BusinessException(UsersBusinessMessages.PhoneNumberAlreadyExists);
    }

    public async Task<bool> UpdatedIdNumberAlreadyExists(string idNumber, string gid, User? user)
    {
        if (user.IdentityNo != idNumber)
        {
            bool idNumberExists = await _userReadRepository.GetAll().AnyAsync(uc => uc.IdentityNo == idNumber && uc.Gid.ToString() != gid);
            if (idNumberExists)
            {
                return false;
            }
        }
        return true;
    }

    public async Task<bool> UpdatedPhoneNumberAlreadyExists(string? phoneNumber, string gid, User? user)
    {
        if (phoneNumber == null)
            return true;

        if (user.Gsm != phoneNumber)
        {
            bool phoneNumberExists = await _userReadRepository.GetAll().AnyAsync(uc => uc.Gsm == phoneNumber && uc.Gid.ToString() != gid);
            if (phoneNumberExists)
            {
                return false;
            }
        }
        return true;
    }


    public async Task FileTypeCheck(string extension, string[] allowedExtensions, IFormFileCollection formFiles)
    {
        if (!await _fileTypeCheckService.CheckFileType(extension, allowedExtensions) || !await _fileTypeCheckService.CheckFileType(extension, formFiles[0]))
            throw new BusinessException(UsersBusinessMessages.IncorrectAvatarImageMessage);
    }
}
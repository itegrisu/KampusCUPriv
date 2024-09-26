using Application.Abstractions.Storage;
using Application.Features.AnnouncementManagementFeatures.Announcements.Constants;
using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Repositories.AnnouncementManagementRepos.AnnouncementRecipientRepo;
using Application.Repositories.AnnouncementManagementRepos.AnnouncementRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.AnnouncementManagements;
using Microsoft.AspNetCore.Http;

namespace Application.Features.AnnouncementManagementFeatures.Announcements.Rules;

public class AnnouncementBusinessRules : BaseBusinessRules
{
    private readonly IAnnouncementReadRepository _announcementReadRepository;
    private readonly IFileTypeCheckService _fileTypeCheckService;

    public AnnouncementBusinessRules(IAnnouncementReadRepository announcementReadRepository, IFileTypeCheckService fileTypeCheckService)
    {
        _announcementReadRepository = announcementReadRepository;
        _fileTypeCheckService = fileTypeCheckService;
    }

    public async Task AnnouncementShouldExistWhenSelected(string gid)
    { 
        
        if (await _announcementReadRepository.GetAsync(predicate:x=>x.Gid.ToString() == gid ) == null)
            throw new BusinessException(AnnouncementsBusinessMessages.ErrorAnnouncementShouldExist);
    }

    public async Task FileTypeCheck(string extension, string[] allowedExtensions, IFormFileCollection formFiles)
    {
        if (!await _fileTypeCheckService.CheckFileType(extension, allowedExtensions) || !await _fileTypeCheckService.CheckFileType(extension, formFiles[0]))
            throw new BusinessException(UsersBusinessMessages.IncorrectAvatarImageMessage);
    }
}
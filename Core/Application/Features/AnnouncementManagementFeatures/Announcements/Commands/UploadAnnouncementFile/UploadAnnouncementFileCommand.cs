using Application.Abstractions.Storage;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Features.GeneralManagementFeatures.Users.Rules;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Domain.Entities.GeneralManagements;
using Application.Repositories.AnnouncementManagementRepos.AnnouncementRepo;
using Application.Features.AnnouncementManagementFeatures.Announcements.Rules;
using Domain.Entities.AnnouncementManagements;
using Application.Features.AnnouncementManagementFeatures.Announcements.Constants;

namespace Application.Features.AnnouncementManagementFeatures.Announcements.Commands.UploadAnnouncementFile
{
    public class UploadAnnouncementFileCommand : IRequest<UploadAnnouncementFileResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }

        public class UploadAvatarUserCommandHandler : IRequestHandler<UploadAnnouncementFileCommand, UploadAnnouncementFileResponse>
        {
            private readonly IMapper _mapper;
            private readonly IAnnouncementWriteRepository _announcementWriteRepository;
            private readonly IAnnouncementReadRepository _announcementReadRepository;
            private readonly AnnouncementBusinessRules _announcementBusinessRules;
            private readonly IFileTypeCheckService _fileTypeCheckService;
            private readonly IStorageService _storageService;
            private readonly IWebHostEnvironment _webHostEnvironment;
            private readonly IConfiguration _configuration;


            public UploadAvatarUserCommandHandler(IMapper mapper, IAnnouncementWriteRepository announcementWriteRepository, IAnnouncementReadRepository announcementReadRepository, AnnouncementBusinessRules announcementBusinessRules, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
            {
                _mapper = mapper;
                _announcementWriteRepository = announcementWriteRepository;
                _announcementReadRepository = announcementReadRepository;
                _announcementBusinessRules = announcementBusinessRules;
                _fileTypeCheckService = fileTypeCheckService;
                _storageService = storageService;
                _webHostEnvironment = webHostEnvironment;
                _configuration = configuration;
            }

            public async Task<UploadAnnouncementFileResponse> Handle(UploadAnnouncementFileCommand request, CancellationToken cancellationToken)
            {
                await _announcementBusinessRules.AnnouncementShouldExistWhenSelected(request.Gid.ToString());

                Announcement? announcement = await _announcementReadRepository.GetSingleAsync(u => u.Gid == request.Gid);

                _storageService.FileCopy(request.FileName, "Files/0temp", "Files/announcement-images");

                announcement.Image = "\\Files\\announcement-images\\" + request.FileName;

                _announcementWriteRepository.Update(announcement);
                await _announcementWriteRepository.SaveAsync();

                return new()
                {
                    FullPath = announcement.Image,
                    Title = UsersBusinessMessages.ProcessCompleted,
                    Message = AnnouncementsBusinessMessages.SuccessUploadImage,
                    IsValid = true
                };
            }


        }
    }
}
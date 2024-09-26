using Application.Abstractions.Storage;
using Application.Features.AnnouncementManagementFeatures.Announcements.Rules;
using Application.Features.GeneralManagementFeatures.Users.Commands.UploadAvatarTemp;
using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Features.GeneralManagementFeatures.Users.Rules;
using Application.Repositories.AnnouncementManagementRepos.AnnouncementRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Domain.Entities.AnnouncementManagements;
using Domain.Entities.GeneralManagements;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.AnnouncementManagementFeatures.Announcements.Commands.UploadAnnouncementFileTemp
{
    public class UploadAnnouncementTempCommand : IRequest<UploadAnnouncementTempResponse>
    {
        public string Params { get; set; }
        public IFormFileCollection? FormFiles { get; set; }

        public class UploadAnnouncementFileTempCommandHandler : IRequestHandler<UploadAnnouncementTempCommand, UploadAnnouncementTempResponse>
        {
            private readonly IAnnouncementReadRepository _announcementReadRepository;
            private readonly AnnouncementBusinessRules _announcementBusinessRules;
            private readonly IFileTypeCheckService _fileTypeCheckService;
            private readonly IStorageService _storageService;

            public UploadAnnouncementFileTempCommandHandler(AnnouncementBusinessRules announcementBusinessRules, IStorageService storageService, IFileTypeCheckService fileTypeCheckService, IAnnouncementReadRepository announcementReadRepository)
            {
                _announcementBusinessRules = announcementBusinessRules;
                _storageService = storageService;
                _fileTypeCheckService = fileTypeCheckService;
                _announcementReadRepository = announcementReadRepository;
            }

            public async Task<UploadAnnouncementTempResponse> Handle(UploadAnnouncementTempCommand request, CancellationToken cancellationToken)
            {
                await _announcementBusinessRules.AnnouncementShouldExistWhenSelected(request.Params);
                Announcement? announcement = await _announcementReadRepository.GetSingleAsync(u => u.Gid.ToString() == request.Params);
                string extension = Path.GetExtension(request.FormFiles[0].FileName);



                string[] allowedExtensions = new string[] { ".png", ".jpg", ".jpeg", ".pdf" };

                await _announcementBusinessRules.FileTypeCheck(extension, allowedExtensions, request.FormFiles);

                List<(string fileName, string pathOrContainer)> datas = await _storageService.UploadAsync("Files\\0temp", request.FormFiles); //tempteki dosya


                return new()
                {
                    Title = UsersBusinessMessages.ProcessCompleted,
                    Message = UsersBusinessMessages.FileReadyForUpload,
                    IsValid = true,
                    FileName = datas[0].fileName,

                };
            }
        }
    }
}

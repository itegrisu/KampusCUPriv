using Application.Abstractions.Storage;
using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Features.GeneralManagementFeatures.Users.Rules;
using Application.Features.SupportManagementFeatures.SupportMessages.Constants;
using Application.Features.SupportManagementFeatures.SupportMessages.Rules;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.SupportManagementRepos.SupportMessageRepo;
using Domain.Entities.GeneralManagements;
using Domain.Entities.SupportManagements;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.SupportManagementFeatures.SupportMessages.Commands.UploadTempFile
{
    public class UploadSupportFileTempCommand : IRequest<UploadSupportFileTempResponse>
    {
        public string Params { get; set; }
        public IFormFileCollection? FormFiles { get; set; }

        public class UploadSupportFileTempCommandHandler : IRequestHandler<UploadSupportFileTempCommand, UploadSupportFileTempResponse>
        {
            private readonly ISupportMessageReadRepository _supportMessageReadRepository;
            private readonly SupportMessageBusinessRules _supportMessageBusinessRules;
            private readonly IFileTypeCheckService _fileTypeCheckService;
            private readonly IStorageService _storageService;

            public UploadSupportFileTempCommandHandler(IStorageService storageService, IFileTypeCheckService fileTypeCheckService, ISupportMessageReadRepository supportMessageReadRepository, SupportMessageBusinessRules supportMessageBusinessRules)
            {
                _storageService = storageService;
                _fileTypeCheckService = fileTypeCheckService;
                _supportMessageReadRepository = supportMessageReadRepository;
                _supportMessageBusinessRules = supportMessageBusinessRules;
            }

            public async Task<UploadSupportFileTempResponse> Handle(UploadSupportFileTempCommand request, CancellationToken cancellationToken)
            {
                //SupportMessage supportMessage = await _supportMessageReadRepository.GetSingleAsync(s => s.Gid.ToString() == request.Params);
                //await _supportMessageBusinessRules.SupportMessageShouldExistWhenSelected(supportMessage);
                string extension = Path.GetExtension(request.FormFiles[0].FileName);

                string[] allowedExtensions = new string[] { ".png", ".jpg", ".jpeg", ".pdf" };

                await _supportMessageBusinessRules.FileTypeCheck(extension, allowedExtensions, request.FormFiles);

                List<(string fileName, string pathOrContainer)> datas = await _storageService.UploadAsync("Files\\0temp", request.FormFiles); //tempteki dosya


                return new()
                {
                    Title = SupportMessagesBusinessMessages.ProcessCompleted,
                    Message = SupportMessagesBusinessMessages.FileReadyForUpload,
                    IsValid = true,
                    FileName = datas[0].fileName,

                };
            }
        }
    }
}

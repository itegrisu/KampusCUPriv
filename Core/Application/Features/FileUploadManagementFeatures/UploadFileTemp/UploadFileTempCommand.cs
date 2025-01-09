using Application.Abstractions.Storage;
using Application.Features.GeneralFeatures.Users.Constants;
using Application.Features.GeneralFeatures.Users.Rules;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.FileUploadManagementFeatures.UploadFileTemp
{
    public class UploadFileTempCommand : IRequest<UploadFileTempResponse>
    {
        public IFormFileCollection? FormFiles { get; set; }

        public class UploadFileTempCommandHandler : IRequestHandler<UploadFileTempCommand, UploadFileTempResponse>
        {
            private readonly IFileTypeCheckService _fileTypeCheckService;
            private readonly IStorageService _storageService;
            private readonly UserBusinessRules _userCustomBusinessRules;

            public UploadFileTempCommandHandler(IFileTypeCheckService fileTypeCheckService, IStorageService storageService, UserBusinessRules userCustomBusinessRules)
            {
                _fileTypeCheckService = fileTypeCheckService;
                _storageService = storageService;
                _userCustomBusinessRules = userCustomBusinessRules;
            }

            public async Task<UploadFileTempResponse> Handle(UploadFileTempCommand request, CancellationToken cancellationToken)
            {
                string extension = Path.GetExtension(request.FormFiles[0].FileName);

                string[] allowedExtensions = new string[] { ".png", ".jpg", ".jpeg", ".pdf" };

                await _userCustomBusinessRules.FileTypeCheck(extension, allowedExtensions, request.FormFiles);

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

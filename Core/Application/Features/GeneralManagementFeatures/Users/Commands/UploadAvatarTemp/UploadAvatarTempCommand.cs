using Application.Abstractions.Storage;
using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Features.GeneralManagementFeatures.Users.Rules;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Domain.Entities.GeneralManagements;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.UploadAvatarTemp
{
    public class UploadAvatarTempCommand : IRequest<UploadAvatarTempResponse>
    {
        public string Params { get; set; }
        public IFormFileCollection? FormFiles { get; set; }

        public class UploadAvatarTempCommandHandler : IRequestHandler<UploadAvatarTempCommand, UploadAvatarTempResponse>
        {
            private readonly IUserReadRepository _userReadRepository;
            private readonly UserBusinessRules _userCustomBusinessRules;
            private readonly IFileTypeCheckService _fileTypeCheckService;
            private readonly IStorageService _storageService;

            public UploadAvatarTempCommandHandler(IUserReadRepository userReadRepository, UserBusinessRules userCustomBusinessRules, IStorageService storageService, IFileTypeCheckService fileTypeCheckService)
            {
                _userReadRepository = userReadRepository;
                _userCustomBusinessRules = userCustomBusinessRules;
                _storageService = storageService;
                _fileTypeCheckService = fileTypeCheckService;
            }

            public async Task<UploadAvatarTempResponse> Handle(UploadAvatarTempCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userReadRepository.GetSingleAsync(u => u.Gid.ToString() == request.Params);
                string extension = Path.GetExtension(request.FormFiles[0].FileName);

                await _userCustomBusinessRules.UserCustomIdShouldExistWhenSelected(user.Gid);

                string[] allowedExtensions = new string[] { ".png", ".jpg", ".jpeg"};

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

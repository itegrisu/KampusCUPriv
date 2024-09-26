using Application.Abstractions.Storage;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Features.GeneralManagementFeatures.Users.Rules;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.UploadAvatar
{
    public class UploadAvatarUserCommand : IRequest<UploadAvatarUserResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }

        public class UploadAvatarUserCommandHandler : IRequestHandler<UploadAvatarUserCommand, UploadAvatarUserResponse>
        {
            private readonly IMapper _mapper;
            private readonly IUserWriteRepository _userWriteRepository;
            private readonly IUserReadRepository _userReadRepository;
            private readonly UserBusinessRules _userCustomBusinessRules;
            private readonly IFileTypeCheckService _fileTypeCheckService;
            private readonly IStorageService _storageService;
            private readonly IWebHostEnvironment _webHostEnvironment;
            private readonly IConfiguration _configuration;


            public UploadAvatarUserCommandHandler(IMapper mapper, IUserWriteRepository userWriteRepository, IUserReadRepository userReadRepository, UserBusinessRules userCustomBusinessRules, IStorageService storageService, IFileTypeCheckService fileTypeCheckService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
            {
                _mapper = mapper;
                _userWriteRepository = userWriteRepository;
                _userReadRepository = userReadRepository;
                _userCustomBusinessRules = userCustomBusinessRules;
                _storageService = storageService;
                _fileTypeCheckService = fileTypeCheckService;
                _webHostEnvironment = webHostEnvironment;
                _configuration = configuration;
            }

            public async Task<UploadAvatarUserResponse> Handle(UploadAvatarUserCommand request, CancellationToken cancellationToken)
            {
                await _userCustomBusinessRules.UserCustomIdShouldExistWhenSelected(request.Gid);

                User? user = await _userReadRepository.GetSingleAsync(u => u.Gid == request.Gid);

                _storageService.FileCopy(request.FileName, "Files/0temp", "Files/user-avatars");

                user.Avatar = "\\Files\\user-avatars\\" + request.FileName;

                _userWriteRepository.Update(user);
                await _userWriteRepository.SaveAsync();

                return new()
                {
                    FullPath = user.Avatar,
                    Title = UsersBusinessMessages.ProcessCompleted,
                    Message = UsersBusinessMessages.SucessUploadAvatarImagesMessage,
                    IsValid = true
                };
            }


        }
    }
}
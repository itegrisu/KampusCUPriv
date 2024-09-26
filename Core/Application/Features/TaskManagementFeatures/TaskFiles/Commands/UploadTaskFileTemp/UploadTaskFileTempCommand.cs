using Application.Abstractions.Storage;
using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Features.TaskManagementFeatures.TaskFiles.Constants;
using Application.Features.TaskManagementFeatures.TaskFiles.Rules;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.TaskManagementRepos.TaskFileRepo;
using Domain.Entities.GeneralManagements;
using Domain.Entities.TaskManagements;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TaskManagementFeatures.TaskFiles.Commands.UploadTaskFileTemp
{
    public class UploadTaskFileTempCommand : IRequest<UploadTaskFileTempResponse>
    {
        public Guid Params { get; set; }
        public IFormFileCollection? FormFiles { get; set; }
        public class UploadTaskFileTempCommandHandler : IRequestHandler<UploadTaskFileTempCommand, UploadTaskFileTempResponse>
        {
            private readonly ITaskFileReadRepository _taskFileReadRepository;
            private readonly TaskFileBusinessRules _taskFileBusinessRules;
            private readonly IStorageService _storageService;

            public UploadTaskFileTempCommandHandler(ITaskFileReadRepository taskFileReadRepository, TaskFileBusinessRules taskFileBusinessRules, IStorageService storageService)
            {
                _taskFileReadRepository = taskFileReadRepository;
                _taskFileBusinessRules = taskFileBusinessRules;
                _storageService = storageService;
            }

            public async Task<UploadTaskFileTempResponse> Handle(UploadTaskFileTempCommand request, CancellationToken cancellationToken)
            {
                await _taskFileBusinessRules.TaskFileShouldExistWhenSelected(request.Params);

                TaskFile? taskFile = await _taskFileReadRepository.GetSingleAsync(u => u.Gid == request.Params);

                string extension = Path.GetExtension(request.FormFiles[0].FileName);


                string[] allowedExtensions = new string[] { ".png", ".jpg", ".jpeg", ".pdf" };

                await _taskFileBusinessRules.FileTypeCheck(extension, allowedExtensions, request.FormFiles);

                List<(string fileName, string pathOrContainer)> datas = await _storageService.UploadAsync("Files\\0temp", request.FormFiles); //tempteki dosya


                return new()
                {
                    Title = TaskFilesBusinessMessages.ProcessCompleted,
                    Message = TaskFilesBusinessMessages.FileReadyForUpload,
                    IsValid = true,
                    FileName = datas[0].fileName,
                };
            }
        }
    }
}

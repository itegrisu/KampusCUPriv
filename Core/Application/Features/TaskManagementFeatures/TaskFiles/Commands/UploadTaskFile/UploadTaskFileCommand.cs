using Application.Abstractions.Storage;
using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Features.TaskManagementFeatures.TaskFiles.Constants;
using Application.Features.TaskManagementFeatures.TaskFiles.Rules;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.TaskManagementRepos.TaskFileRepo;
using Domain.Entities.GeneralManagements;
using Domain.Entities.TaskManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TaskManagementFeatures.TaskFiles.Commands.UploadTaskFile
{
    public class UploadTaskFileCommand : IRequest<UploadTaskFileResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
        public class UploadTaskFileCommandHandler : IRequestHandler<UploadTaskFileCommand, UploadTaskFileResponse>
        {
            private readonly TaskFileBusinessRules _taskFileBusinessRules;
            private readonly ITaskFileReadRepository _taskFileReadRepository;
            private readonly ITaskFileWriteRepository _taskFileWriteRepository;
            private readonly IFileTypeCheckService _fileTypeCheckService;
            private readonly IStorageService _storageService;
            private readonly IWebHostEnvironment _webHostEnvironment;
            private readonly IConfiguration _configuration;

            public UploadTaskFileCommandHandler(TaskFileBusinessRules taskFileBusinessRules, ITaskFileReadRepository taskFileReadRepository, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, ITaskFileWriteRepository taskFileWriteRepository)
            {
                _taskFileBusinessRules = taskFileBusinessRules;
                _taskFileReadRepository = taskFileReadRepository;
                _fileTypeCheckService = fileTypeCheckService;
                _storageService = storageService;
                _webHostEnvironment = webHostEnvironment;
                _configuration = configuration;
                _taskFileWriteRepository = taskFileWriteRepository;
            }

            public async Task<UploadTaskFileResponse> Handle(UploadTaskFileCommand request, CancellationToken cancellationToken)
            {
                await _taskFileBusinessRules.TaskFileShouldExistWhenSelected(request.Gid);

                TaskFile taskFile = await _taskFileReadRepository.GetSingleAsync(u => u.Gid == request.Gid);

                _storageService.FileCopy(request.FileName, "Files/0temp", "Files/task-files");

                taskFile.UploadedFile = "\\Files\\task-files\\" + request.FileName;

                _taskFileWriteRepository.Update(taskFile);
                await _taskFileWriteRepository.SaveAsync();

                return new()
                {
                    FullPath = taskFile.UploadedFile,
                    Title = TaskFilesBusinessMessages.ProcessCompleted,
                    Message = TaskFilesBusinessMessages.SuccessUploadedTaskFileMessage,
                    IsValid = true
                };
            }
        }
    }
}

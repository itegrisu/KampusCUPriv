using Application.Abstractions.Storage;
using Application.Features.Base;
using Application.Features.GeneralManagementFeatures.UserReminders.Constants;
using Application.Features.GeneralManagementFeatures.UserReminders.Rules;
using Application.Repositories.GeneralManagementRepos.UserReminderRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.GeneralManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Application.Features.GeneralManagementFeatures.UserReminders.Commands.UploadFile
{
    public class UploadUserReminderResponse : BaseResponse, IResponse
    {
        public Guid Gid { get; set; }
        public string FullPath { get; set; }
    }

    public class UploadUserReminderCommandHandler : IRequestHandler<UploadUserReminderCommand, UploadUserReminderResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserReminderReadRepository _userReminderReadRepository;
        private readonly IUserReminderWriteRepository _userReminderWriteRepository;
        private readonly UserReminderBusinessRules _userReminderBusinessRules;
        private readonly IFileTypeCheckService _fileTypeCheckService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;


        public UploadUserReminderCommandHandler(IMapper mapper, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration,IUserReminderReadRepository userReminderReadRepository, IUserReminderWriteRepository userReminderWriteRepository, UserReminderBusinessRules userReminderBusinessRules)
        {
            _mapper = mapper;
            _fileTypeCheckService = fileTypeCheckService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _userReminderReadRepository = userReminderReadRepository;
            _userReminderWriteRepository = userReminderWriteRepository;
            _userReminderBusinessRules = userReminderBusinessRules;
        }

        public async Task<UploadUserReminderResponse> Handle(UploadUserReminderCommand request, CancellationToken cancellationToken)
        {
            UserReminder? userReminder = await _userReminderReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
            await _userReminderBusinessRules.UserReminderShouldExistWhenSelected(userReminder);

            _storageService.FileCopy(request.FileName, "Files/0temp", "Files/user-reminder-files");

            userReminder.Document = "\\Files\\user-reminder-files\\" + request.FileName;

            _userReminderWriteRepository.Update(userReminder);
            await _userReminderWriteRepository.SaveAsync();

            return new()
            {
                FullPath = userReminder.Document,
                Title = UserRemindersBusinessMessages.ProcessCompleted,
                Message = UserRemindersBusinessMessages.SucessUploadFile,
                IsValid = true
            };
        }
    }
}

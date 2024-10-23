using Application.Abstractions.Storage;
using Application.Features.Base;
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelPassportInfoRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.PersonnelManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Commands.UploadFile
{
    public class UploadPassportFileResponse : BaseResponse, IResponse
    {
        public Guid Gid { get; set; }
        public string FullPath { get; set; }
    }

    public class UploadPassportFileCommandHandler : IRequestHandler<UploadPassportFileCommand, UploadPassportFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelPassportInfoReadRepository _personnelPassportInfoReadRepository;
        private readonly IPersonnelPassportInfoWriteRepository _personnelPassportInfoWriteRepository;
        private readonly PersonnelPassportInfoBusinessRules _personnelPassportInfoBusinessRules;
        private readonly IFileTypeCheckService _fileTypeCheckService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public UploadPassportFileCommandHandler(IMapper mapper, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IPersonnelPassportInfoReadRepository personnelPassportInfoReadRepository, IPersonnelPassportInfoWriteRepository personnelPassportInfoWriteRepository, PersonnelPassportInfoBusinessRules personnelPassportInfoBusinessRules)
        {
            _mapper = mapper;
            _fileTypeCheckService = fileTypeCheckService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _personnelPassportInfoReadRepository = personnelPassportInfoReadRepository;
            _personnelPassportInfoWriteRepository = personnelPassportInfoWriteRepository;
            _personnelPassportInfoBusinessRules = personnelPassportInfoBusinessRules;
        }

        public async Task<UploadPassportFileResponse> Handle(UploadPassportFileCommand request, CancellationToken cancellationToken)
        {


            PersonnelPassportInfo? personnelPassportInfo = await _personnelPassportInfoReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
            await _personnelPassportInfoBusinessRules.PersonnelPassportInfoShouldExistWhenSelected(personnelPassportInfo);

            _storageService.FileCopy(request.FileName, "Files/0temp", "Files/passport-files");

            personnelPassportInfo.Document = "\\Files\\passport-files\\" + request.FileName;

            _personnelPassportInfoWriteRepository.Update(personnelPassportInfo);
            await _personnelPassportInfoWriteRepository.SaveAsync();

            return new()
            {
                FullPath = personnelPassportInfo.Document,
                Title = PersonnelPassportInfosBusinessMessages.ProcessCompleted,
                Message = PersonnelPassportInfosBusinessMessages.SucessUploadFile,
                IsValid = true
            };
        }


    }
}

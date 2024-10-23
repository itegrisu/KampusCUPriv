using Application.Abstractions.Storage;
using Application.Features.Base;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelResidenceInfoRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.PersonnelManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Commands.UploadFile
{
    public class UploadResidenceFileResponse : BaseResponse, IResponse
    {
        public Guid Gid { get; set; }
        public string FullPath { get; set; }
    }

    public class UploadResidenceFileCommandHandler : IRequestHandler<UploadResidenceFileCommand, UploadResidenceFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelResidenceInfoReadRepository _personnelResidenceInfoReadRepository;
        private readonly IPersonnelResidenceInfoWriteRepository _personnelResidenceInfoWriteRepository;
        private readonly PersonnelResidenceInfoBusinessRules _personnelResidenceInfoBusinessRules;
        private readonly IFileTypeCheckService _fileTypeCheckService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public UploadResidenceFileCommandHandler(IMapper mapper, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IPersonnelResidenceInfoReadRepository personnelResidenceInfoReadRepository, IPersonnelResidenceInfoWriteRepository personnelResidenceInfoWriteRepository, PersonnelResidenceInfoBusinessRules personnelResidenceInfoBusinessRules)
        {
            _mapper = mapper;
            _fileTypeCheckService = fileTypeCheckService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _personnelResidenceInfoReadRepository = personnelResidenceInfoReadRepository;
            _personnelResidenceInfoWriteRepository = personnelResidenceInfoWriteRepository;
            _personnelResidenceInfoBusinessRules = personnelResidenceInfoBusinessRules;
        }

        public async Task<UploadResidenceFileResponse> Handle(UploadResidenceFileCommand request, CancellationToken cancellationToken)
        {


            PersonnelResidenceInfo? personnelResidenceInfo = await _personnelResidenceInfoReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
            await _personnelResidenceInfoBusinessRules.PersonnelResidenceInfoShouldExistWhenSelected(personnelResidenceInfo);

            _storageService.FileCopy(request.FileName, "Files/0temp", "Files/residence-files");

            personnelResidenceInfo.Document = "\\Files\\residence-files\\" + request.FileName;

            _personnelResidenceInfoWriteRepository.Update(personnelResidenceInfo);
            await _personnelResidenceInfoWriteRepository.SaveAsync();

            return new()
            {
                FullPath = personnelResidenceInfo.Document,
                Title = PersonnelResidenceInfosBusinessMessages.ProcessCompleted,
                Message = PersonnelResidenceInfosBusinessMessages.SucessUploadFile,
                IsValid = true
            };
        }


    }
}

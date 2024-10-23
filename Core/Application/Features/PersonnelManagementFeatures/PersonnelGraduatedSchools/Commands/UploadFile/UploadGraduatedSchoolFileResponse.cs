using Application.Abstractions.Storage;
using Application.Features.Base;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelGraduatedSchoolRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.PersonnelManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Commands.UploadFile
{
    public class UploadGraduatedSchoolFileResponse : BaseResponse, IResponse
    {
        public Guid Gid { get; set; }
        public string FullPath { get; set; }
    }

    public class UploadGraduatedSchoolFileCommandHandler : IRequestHandler<UploadGraduatedSchoolFileCommand, UploadGraduatedSchoolFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelGraduatedSchoolReadRepository _personnelGraduatedSchoolReadRepository;
        private readonly IPersonnelGraduatedSchoolWriteRepository _personnelGraduatedSchoolWriteRepository;
        private readonly PersonnelGraduatedSchoolBusinessRules _personnelGraduatedSchoolBusinessRules;
        private readonly IFileTypeCheckService _fileTypeCheckService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public UploadGraduatedSchoolFileCommandHandler(IMapper mapper, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IPersonnelGraduatedSchoolReadRepository personnelGraduatedSchoolReadRepository, IPersonnelGraduatedSchoolWriteRepository personnelGraduatedSchoolWriteRepository, PersonnelGraduatedSchoolBusinessRules personnelGraduatedSchoolBusinessRules)
        {
            _mapper = mapper;
            _fileTypeCheckService = fileTypeCheckService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _personnelGraduatedSchoolReadRepository = personnelGraduatedSchoolReadRepository;
            _personnelGraduatedSchoolWriteRepository = personnelGraduatedSchoolWriteRepository;
            _personnelGraduatedSchoolBusinessRules = personnelGraduatedSchoolBusinessRules;
        }

        public async Task<UploadGraduatedSchoolFileResponse> Handle(UploadGraduatedSchoolFileCommand request, CancellationToken cancellationToken)
        {
            PersonnelGraduatedSchool? personnelGraduatedSchool = await _personnelGraduatedSchoolReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
            await _personnelGraduatedSchoolBusinessRules.PersonnelGraduatedSchoolShouldExistWhenSelected(personnelGraduatedSchool);

            _storageService.FileCopy(request.FileName, "Files/0temp", "Files/graduated-school-files");

            personnelGraduatedSchool.Document = "\\Files\\graduated-school-files\\" + request.FileName;

            _personnelGraduatedSchoolWriteRepository.Update(personnelGraduatedSchool);
            await _personnelGraduatedSchoolWriteRepository.SaveAsync();

            return new()
            {
                FullPath = personnelGraduatedSchool.Document,
                Title = PersonnelGraduatedSchoolsBusinessMessages.ProcessCompleted,
                Message = PersonnelGraduatedSchoolsBusinessMessages.SucessUploadFile,
                IsValid = true
            };
        }
    }
}

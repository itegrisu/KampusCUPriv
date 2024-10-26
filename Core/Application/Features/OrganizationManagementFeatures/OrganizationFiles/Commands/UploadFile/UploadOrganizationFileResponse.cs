using Application.Abstractions.Storage;
using Application.Features.Base;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Constants;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationFileRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.OrganizationManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.UploadFile
{
    public class UploadOrganizationFileResponse : BaseResponse, IResponse
    {
        public Guid Gid { get; set; }
        public string FullPath { get; set; }
    }

    public class UploadOrganizationFileCommandHandler : IRequestHandler<UploadOrganizationFileCommand, UploadOrganizationFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationFileReadRepository _organizationFileReadRepository;
        private readonly IOrganizationFileWriteRepository _organizationFileWriteRepository;
        private readonly OrganizationFileBusinessRules _organizationFileBusinessRules;
        private readonly IFileTypeCheckService _fileTypeCheckService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;


        public UploadOrganizationFileCommandHandler(IMapper mapper, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IOrganizationFileReadRepository organizationFileReadRepository, IOrganizationFileWriteRepository organizationFileWriteRepository, OrganizationFileBusinessRules organizationFileBusinessRules)
        {
            _mapper = mapper;
            _fileTypeCheckService = fileTypeCheckService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _organizationFileReadRepository = organizationFileReadRepository;
            _organizationFileWriteRepository = organizationFileWriteRepository;
            _organizationFileBusinessRules = organizationFileBusinessRules;
        }

        public async Task<UploadOrganizationFileResponse> Handle(UploadOrganizationFileCommand request, CancellationToken cancellationToken)
        {
            OrganizationFile? organizationFile = await _organizationFileReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
            await _organizationFileBusinessRules.OrganizationFileShouldExistWhenSelected(organizationFile);

            _storageService.FileCopy(request.FileName, "Files/0temp", "Files/organization-files");

            organizationFile.Document = "\\Files\\organization-files\\" + request.FileName;

            _organizationFileWriteRepository.Update(organizationFile);
            await _organizationFileWriteRepository.SaveAsync();

            return new()
            {
                FullPath = organizationFile.Document,
                Title = OrganizationFilesBusinessMessages.ProcessCompleted,
                Message = OrganizationFilesBusinessMessages.SucessUploadFile,
                IsValid = true
            };
        }


    }
}

using Application.Abstractions.Storage;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Constants;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationItemFileRepo;
using AutoMapper;
using Domain.Entities.OrganizationManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.UploadDocument
{
    public class UploadDocumentOrganizationItemFileCommand : IRequest<UploadDocumentOrganizationItemFileResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }

        public class UploadDocumentOrganizationItemFileCommandHandler : IRequestHandler<UploadDocumentOrganizationItemFileCommand, UploadDocumentOrganizationItemFileResponse>
        {
            private readonly IMapper _mapper;
            private readonly IOrganizationItemFileReadRepository _organizationItemFileReadRepository;
            private readonly IOrganizationItemFileWriteRepository _organizationItemFileWriteRepository;
            private readonly OrganizationItemFileBusinessRules _organizationItemFileBusinessRules;
            private readonly IFileTypeCheckService _fileTypeCheckService;
            private readonly IStorageService _storageService;
            private readonly IWebHostEnvironment _webHostEnvironment;
            private readonly IConfiguration _configuration;

            public UploadDocumentOrganizationItemFileCommandHandler(IMapper mapper, IOrganizationItemFileReadRepository organizationItemFileReadRepository, IOrganizationItemFileWriteRepository organizationItemFileWriteRepository, OrganizationItemFileBusinessRules organizationItemFileBusinessRules, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
            {
                _mapper = mapper;
                _organizationItemFileReadRepository = organizationItemFileReadRepository;
                _organizationItemFileWriteRepository = organizationItemFileWriteRepository;
                _organizationItemFileBusinessRules = organizationItemFileBusinessRules;
                _fileTypeCheckService = fileTypeCheckService;
                _storageService = storageService;
                _webHostEnvironment = webHostEnvironment;
                _configuration = configuration;
            }

            public async Task<UploadDocumentOrganizationItemFileResponse> Handle(UploadDocumentOrganizationItemFileCommand request, CancellationToken cancellationToken)
            {
                OrganizationItemFile? organizationItemFile = await _organizationItemFileReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
                await _organizationItemFileBusinessRules.OrganizationItemFileShouldExistWhenSelected(organizationItemFile);

                _storageService.FileCopy(request.FileName, "Files/0temp", "Files/organization-item-files");

                organizationItemFile.Document = "\\Files\\organization-item-files\\" + request.FileName;

                _organizationItemFileWriteRepository.Update(organizationItemFile);
                await _organizationItemFileWriteRepository.SaveAsync();

                return new()
                {
                    FullPath = organizationItemFile.Document,
                    Title = OrganizationItemFilesBusinessMessages.ProcessCompleted,
                    Message = OrganizationItemFilesBusinessMessages.SucessUploadFile,
                    IsValid = true
                };
            }
        }
    }
}

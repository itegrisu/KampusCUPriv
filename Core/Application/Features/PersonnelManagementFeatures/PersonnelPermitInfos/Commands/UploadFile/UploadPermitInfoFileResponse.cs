using Application.Abstractions.Storage;
using Application.Features.Base;
using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelPermitInfoRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.PersonnelManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Commands.UploadFile
{
    public class UploadPermitInfoFileResponse : BaseResponse, IResponse
    {
        public Guid Gid { get; set; }
        public string FullPath { get; set; }
    }

    public class UploadPermitInfoFileCommandHandler : IRequestHandler<UploadPermitInfoFileCommand, UploadPermitInfoFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelPermitInfoReadRepository _personnelPermitInfoReadRepository;
        private readonly IPersonnelPermitInfoWriteRepository _personnelPermitInfoWriteRepository;
        private readonly PersonnelPermitInfoBusinessRules _personnelPermitInfoBusinessRules;
        private readonly IFileTypeCheckService _fileTypeCheckService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public UploadPermitInfoFileCommandHandler(IMapper mapper, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IPersonnelPermitInfoReadRepository personnelPermitInfoReadRepository, IPersonnelPermitInfoWriteRepository personnelPermitInfoWriteRepository, PersonnelPermitInfoBusinessRules personnelPermitInfoBusinessRules)
        {
            _mapper = mapper;
            _fileTypeCheckService = fileTypeCheckService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _personnelPermitInfoReadRepository = personnelPermitInfoReadRepository;
            _personnelPermitInfoWriteRepository = personnelPermitInfoWriteRepository;
            _personnelPermitInfoBusinessRules = personnelPermitInfoBusinessRules;
        }

        public async Task<UploadPermitInfoFileResponse> Handle(UploadPermitInfoFileCommand request, CancellationToken cancellationToken)
        {


            PersonnelPermitInfo? personnelPermitInfo = await _personnelPermitInfoReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
            await _personnelPermitInfoBusinessRules.PersonnelPermitInfoShouldExistWhenSelected(personnelPermitInfo);

            _storageService.FileCopy(request.FileName, "Files/0temp", "Files/permit-files");

            personnelPermitInfo.Document = "\\Files\\permit-files\\" + request.FileName;

            _personnelPermitInfoWriteRepository.Update(personnelPermitInfo);
            await _personnelPermitInfoWriteRepository.SaveAsync();

            return new()
            {
                FullPath = personnelPermitInfo.Document,
                Title = PersonnelPermitInfosBusinessMessages.ProcessCompleted,
                Message = PersonnelPermitInfosBusinessMessages.SucessUploadFile,
                IsValid = true
            };
        }


    }
}

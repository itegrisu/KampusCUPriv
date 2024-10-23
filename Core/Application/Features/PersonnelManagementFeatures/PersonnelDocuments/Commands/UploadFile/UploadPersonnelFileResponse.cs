using Application.Abstractions.Storage;
using Application.Features.Base;
using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelDocumentRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.OfferManagements;
using Domain.Entities.PersonnelManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Commands.UploadFile
{
    public class UploadPersonnelFileResponse : BaseResponse, IResponse
    {
        public Guid Gid { get; set; }
        public string FullPath { get; set; }
    }

    public class UploadPersonneltFileCommandHandler : IRequestHandler<UploadPersonnelFileCommand, UploadPersonnelFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelDocumentReadRepository _personnelDocumentReadRepository;
        private readonly IPersonnelDocumentWriteRepository _personnelDocumentWriteRepository;
        private readonly PersonnelDocumentBusinessRules _personnelDocumentBusinessRules;       
        private readonly IFileTypeCheckService _fileTypeCheckService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public UploadPersonneltFileCommandHandler(IMapper mapper, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IPersonnelDocumentReadRepository personnelDocumentReadRepository, IPersonnelDocumentWriteRepository personnelDocumentWriteRepository, PersonnelDocumentBusinessRules personnelDocumentBusinessRules)
        {
            _mapper = mapper;
            _fileTypeCheckService = fileTypeCheckService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _personnelDocumentReadRepository = personnelDocumentReadRepository;
            _personnelDocumentWriteRepository = personnelDocumentWriteRepository;
            _personnelDocumentBusinessRules = personnelDocumentBusinessRules;
        }

        public async Task<UploadPersonnelFileResponse> Handle(UploadPersonnelFileCommand request, CancellationToken cancellationToken)
        {


            PersonnelDocument? personnelDocument = await _personnelDocumentReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
            await _personnelDocumentBusinessRules.PersonnelDocumentShouldExistWhenSelected(personnelDocument);

            _storageService.FileCopy(request.FileName, "Files/0temp", "Files/personnel-document-files");

            personnelDocument.Document = "\\Files\\personnel-document-files\\" + request.FileName;

            _personnelDocumentWriteRepository.Update(personnelDocument);
            await _personnelDocumentWriteRepository.SaveAsync();

            return new()
            {
                FullPath = personnelDocument.Document,
                Title = PersonnelDocumentsBusinessMessages.ProcessCompleted,
                Message = PersonnelDocumentsBusinessMessages.SucessUploadFile,
                IsValid = true
            };
        }


    }
}

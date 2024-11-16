using Application.Abstractions.Storage;
using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleDocuments.Constants;
using Application.Features.VehicleManagementFeatures.VehicleDocuments.Rules;
using Application.Features.VehicleManagementsFeatures.VehicleDocuments.Commands.UploadDocumentFile;
using Application.Repositories.VehicleManagementsRepos.VehicleDocumentRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.VehicleManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Application.Features.VehicleManagementsFeatures.VehicleDocuments.Commands.UploadVehicleDocumentFile
{
    public class UploadVehicleDocumentFileResponse : BaseResponse, IResponse
    {
        public Guid Gid { get; set; }
        public string FullPath { get; set; }
    }

    public class UploadVehicleDocumentFileCommandHandler : IRequestHandler<UploadVehicleDocumentFileCommand, UploadVehicleDocumentFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleDocumentReadRepository _vehicleDocumentReadRepository;
        private readonly IVehicleDocumentWriteRepository _vehicleDocumentWriteRepository;
        private readonly VehicleDocumentBusinessRules _vehicleDocumentBusinessRules;
        private readonly IFileTypeCheckService _fileTypeCheckService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;


        public UploadVehicleDocumentFileCommandHandler(IMapper mapper, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IVehicleDocumentReadRepository vehicleDocumentReadRepository, IVehicleDocumentWriteRepository vehicleDocumentWriteRepository, VehicleDocumentBusinessRules vehicleDocumentBusinessRules)
        {
            _mapper = mapper;
            _fileTypeCheckService = fileTypeCheckService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _vehicleDocumentReadRepository = vehicleDocumentReadRepository;
            _vehicleDocumentWriteRepository = vehicleDocumentWriteRepository;
            _vehicleDocumentBusinessRules = vehicleDocumentBusinessRules;
        }

        public async Task<UploadVehicleDocumentFileResponse> Handle(UploadVehicleDocumentFileCommand request, CancellationToken cancellationToken)
        {
            VehicleDocument? vehicleDocument = await _vehicleDocumentReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
            await _vehicleDocumentBusinessRules.VehicleDocumentShouldExistWhenSelected(vehicleDocument);

            _storageService.FileCopy(request.FileName, "Files/0temp", "Files/document-files");

            vehicleDocument.DocumentFile = "\\Files\\document-files\\" + request.FileName;

            _vehicleDocumentWriteRepository.Update(vehicleDocument);
            await _vehicleDocumentWriteRepository.SaveAsync();

            return new()
            {
                FullPath = vehicleDocument.DocumentFile,
                Title = VehicleDocumentsBusinessMessages.ProcessCompleted,
                Message = VehicleDocumentsBusinessMessages.FileUploaded,
                IsValid = true
            };
        }
    }
}

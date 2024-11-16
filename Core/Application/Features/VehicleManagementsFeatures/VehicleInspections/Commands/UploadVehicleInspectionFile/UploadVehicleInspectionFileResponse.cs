using Application.Abstractions.Storage;
using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleInspections.Constants;
using Application.Features.VehicleManagementFeatures.VehicleInspections.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleInspectionRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.VehicleManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Application.Features.VehicleManagementsFeatures.VehicleInspections.Commands.UploadVehicleInspectionFile
{
    public class UploadVehicleInspectionFileResponse : BaseResponse, IResponse
    {
        public Guid Gid { get; set; }
        public string FullPath { get; set; }
    }

    public class UploadVehicleInspectionFileCommandHandler : IRequestHandler<UploadVehicleInspectionFileCommand, UploadVehicleInspectionFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleInspectionReadRepository _vehicleInspectionReadRepository;
        private readonly IVehicleInspectionWriteRepository _vehicleInspectionWriteRepository;
        private readonly VehicleInspectionBusinessRules _vehicleInspectionBusinessRules;
        private readonly IFileTypeCheckService _fileTypeCheckService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;


        public UploadVehicleInspectionFileCommandHandler(IMapper mapper, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IVehicleInspectionReadRepository vehicleInspectionReadRepository, IVehicleInspectionWriteRepository vehicleInspectionWriteRepository, VehicleInspectionBusinessRules vehicleInspectionBusinessRules)
        {
            _mapper = mapper;
            _fileTypeCheckService = fileTypeCheckService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _vehicleInspectionReadRepository = vehicleInspectionReadRepository;
            _vehicleInspectionWriteRepository = vehicleInspectionWriteRepository;
            _vehicleInspectionBusinessRules = vehicleInspectionBusinessRules;
        }

        public async Task<UploadVehicleInspectionFileResponse> Handle(UploadVehicleInspectionFileCommand request, CancellationToken cancellationToken)
        {
            VehicleInspection? vehicleInspection = await _vehicleInspectionReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
            await _vehicleInspectionBusinessRules.VehicleInspectionShouldExistWhenSelected(vehicleInspection);

            _storageService.FileCopy(request.FileName, "Files/0temp", "Files/Inspection-files");

            vehicleInspection.DocumentFile = "\\Files\\Inspection-files\\" + request.FileName;

            _vehicleInspectionWriteRepository.Update(vehicleInspection);
            await _vehicleInspectionWriteRepository.SaveAsync();

            return new()
            {
                FullPath = vehicleInspection.DocumentFile,
                Title = VehicleInspectionsBusinessMessages.ProcessCompleted,
                Message = VehicleInspectionsBusinessMessages.FileUploaded,
                IsValid = true
            };
        }
    }
}

using Application.Abstractions.Storage;
using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Constants;
using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleMaintenanceRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.VehicleManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Application.Features.VehicleManagementsFeatures.VehicleMaintenances.Commands.UploadVehicleMaintenanceFile
{
    public class UploadVehicleMaintenanceFileResponse : BaseResponse, IResponse
    {
        public Guid Gid { get; set; }
        public string FullPath { get; set; }
    }

    public class UploadVehicleMaintenanceFileCommandHandler : IRequestHandler<UploadVehicleMaintenanceFileCommand, UploadVehicleMaintenanceFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleMaintenanceReadRepository _vehicleMaintenanceReadRepository;
        private readonly IVehicleMaintenanceWriteRepository _vehicleMaintenanceWriteRepository;
        private readonly VehicleMaintenanceBusinessRules _vehicleMaintenanceBusinessRules;
        private readonly IFileTypeCheckService _fileTypeCheckService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;


        public UploadVehicleMaintenanceFileCommandHandler(IMapper mapper, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IVehicleMaintenanceReadRepository vehicleMaintenanceReadRepository, IVehicleMaintenanceWriteRepository vehicleMaintenanceWriteRepository, VehicleMaintenanceBusinessRules vehicleMaintenanceBusinessRules)
        {
            _mapper = mapper;
            _fileTypeCheckService = fileTypeCheckService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _vehicleMaintenanceReadRepository = vehicleMaintenanceReadRepository;
            _vehicleMaintenanceWriteRepository = vehicleMaintenanceWriteRepository;
            _vehicleMaintenanceBusinessRules = vehicleMaintenanceBusinessRules;
        }

        public async Task<UploadVehicleMaintenanceFileResponse> Handle(UploadVehicleMaintenanceFileCommand request, CancellationToken cancellationToken)
        {
            VehicleMaintenance? vehicleMaintenance = await _vehicleMaintenanceReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
            await _vehicleMaintenanceBusinessRules.VehicleMaintenanceShouldExistWhenSelected(vehicleMaintenance);

            _storageService.FileCopy(request.FileName, "Files/0temp", "Files/maintenance-files");

            vehicleMaintenance.DocumentFile = "\\Files\\maintenance-files\\" + request.FileName;

            _vehicleMaintenanceWriteRepository.Update(vehicleMaintenance);
            await _vehicleMaintenanceWriteRepository.SaveAsync();

            return new()
            {
                FullPath = vehicleMaintenance.DocumentFile,
                Title = VehicleMaintenancesBusinessMessages.ProcessCompleted,
                Message = VehicleMaintenancesBusinessMessages.FileUploaded,
                IsValid = true
            };
        }
    }
}

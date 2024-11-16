using Application.Abstractions.Storage;
using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleEquipments.Constants;
using Application.Features.VehicleManagementFeatures.VehicleEquipments.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleEquipmentRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.VehicleManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Application.Features.VehicleManagementsFeatures.VehicleEquipments.Commands.UploadVehicleEquipmentFile
{
    public class UploadVehicleEquipmentFileResponse : BaseResponse, IResponse
    {
        public Guid Gid { get; set; }
        public string FullPath { get; set; }
    }

    public class UploadVehicleEquipmentFileCommandHandler : IRequestHandler<UploadVehicleEquipmentFileCommand, UploadVehicleEquipmentFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleEquipmentReadRepository _vehicleEquipmentReadRepository;
        private readonly IVehicleEquipmentWriteRepository _vehicleEquipmentWriteRepository;
        private readonly VehicleEquipmentBusinessRules _vehicleEquipmentBusinessRules;
        private readonly IFileTypeCheckService _fileTypeCheckService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;


        public UploadVehicleEquipmentFileCommandHandler(IMapper mapper, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IVehicleEquipmentReadRepository vehicleEquipmentReadRepository, IVehicleEquipmentWriteRepository vehicleEquipmentWriteRepository, VehicleEquipmentBusinessRules vehicleEquipmentBusinessRules)
        {
            _mapper = mapper;
            _fileTypeCheckService = fileTypeCheckService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _vehicleEquipmentReadRepository = vehicleEquipmentReadRepository;
            _vehicleEquipmentWriteRepository = vehicleEquipmentWriteRepository;
            _vehicleEquipmentBusinessRules = vehicleEquipmentBusinessRules;
        }

        public async Task<UploadVehicleEquipmentFileResponse> Handle(UploadVehicleEquipmentFileCommand request, CancellationToken cancellationToken)
        {
            VehicleEquipment? vehicleEquipment = await _vehicleEquipmentReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
            await _vehicleEquipmentBusinessRules.VehicleEquipmentShouldExistWhenSelected(vehicleEquipment);

            _storageService.FileCopy(request.FileName, "Files/0temp", "Files/Equipment-files");

            vehicleEquipment.DocumentFile = "\\Files\\Equipment-files\\" + request.FileName;

            _vehicleEquipmentWriteRepository.Update(vehicleEquipment);
            await _vehicleEquipmentWriteRepository.SaveAsync();

            return new()
            {
                FullPath = vehicleEquipment.DocumentFile,
                Title = VehicleEquipmentsBusinessMessages.ProcessCompleted,
                Message = VehicleEquipmentsBusinessMessages.FileUploaded,
                IsValid = true
            };
        }
    }
}

using Application.Abstractions.Storage;
using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleInsurances.Constants;
using Application.Features.VehicleManagementFeatures.VehicleInsurances.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleInsuranceRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.VehicleManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Application.Features.VehicleManagementsFeatures.VehicleInsurances.Commands.UploadVehicleInsuranceFile
{
    public class UploadVehicleInsuranceFileResponse : BaseResponse, IResponse
    {
        public Guid Gid { get; set; }
        public string FullPath { get; set; }
    }

    public class UploadVehicleInsuranceFileCommandHandler : IRequestHandler<UploadVehicleInsuranceFileCommand, UploadVehicleInsuranceFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleInsuranceReadRepository _vehicleInsuranceReadRepository;
        private readonly IVehicleInsuranceWriteRepository _vehicleInsuranceWriteRepository;
        private readonly VehicleInsuranceBusinessRules _vehicleInsuranceBusinessRules;
        private readonly IFileTypeCheckService _fileTypeCheckService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;


        public UploadVehicleInsuranceFileCommandHandler(IMapper mapper, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IVehicleInsuranceReadRepository vehicleInsuranceReadRepository, IVehicleInsuranceWriteRepository vehicleInsuranceWriteRepository, VehicleInsuranceBusinessRules vehicleInsuranceBusinessRules)
        {
            _mapper = mapper;
            _fileTypeCheckService = fileTypeCheckService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _vehicleInsuranceReadRepository = vehicleInsuranceReadRepository;
            _vehicleInsuranceWriteRepository = vehicleInsuranceWriteRepository;
            _vehicleInsuranceBusinessRules = vehicleInsuranceBusinessRules;
        }

        public async Task<UploadVehicleInsuranceFileResponse> Handle(UploadVehicleInsuranceFileCommand request, CancellationToken cancellationToken)
        {
            VehicleInsurance? vehicleInsurance = await _vehicleInsuranceReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
            await _vehicleInsuranceBusinessRules.VehicleInsuranceShouldExistWhenSelected(vehicleInsurance);

            _storageService.FileCopy(request.FileName, "Files/0temp", "Files/insurance-files");

            vehicleInsurance.DocumentFile = "\\Files\\insurance-files\\" + request.FileName;

            _vehicleInsuranceWriteRepository.Update(vehicleInsurance);
            await _vehicleInsuranceWriteRepository.SaveAsync();

            return new()
            {
                FullPath = vehicleInsurance.DocumentFile,
                Title = VehicleInsurancesBusinessMessages.ProcessCompleted,
                Message = VehicleInsurancesBusinessMessages.FileUploaded,
                IsValid = true
            };
        }
    }
}

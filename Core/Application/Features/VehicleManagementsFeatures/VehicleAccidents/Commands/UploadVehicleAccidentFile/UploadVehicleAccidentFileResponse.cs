using Application.Abstractions.Storage;
using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleAccidents.Rules;
using Application.Features.VehicleManagementFeatures.VehicleAccidents.Constants;
using Application.Repositories.VehicleManagementsRepos.VehicleAccidentRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.VehicleManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleAccidents.Commands.UploadVehicleAccidentFile
{
    public class UploadVehicleAccidentFileResponse : BaseResponse, IResponse
    {
        public Guid Gid { get; set; }
        public string FullPath { get; set; }
    }

    public class UploadVehicleAccidentFileCommandHandler : IRequestHandler<UploadVehicleAccidentFileCommand, UploadVehicleAccidentFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleAccidentReadRepository _vehicleAccidentReadRepository;
        private readonly IVehicleAccidentWriteRepository _vehicleAccidentWriteRepository;
        private readonly VehicleAccidentBusinessRules _vehicleAccidentBusinessRules;
        private readonly IFileTypeCheckService _fileTypeCheckService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        public UploadVehicleAccidentFileCommandHandler(IMapper mapper, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IVehicleAccidentReadRepository vehicleAccidentReadRepository, IVehicleAccidentWriteRepository vehicleAccidentWriteRepository, VehicleAccidentBusinessRules vehicleAccidentBusinessRules)
        {
            _mapper = mapper;
            _fileTypeCheckService = fileTypeCheckService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _vehicleAccidentReadRepository = vehicleAccidentReadRepository;
            _vehicleAccidentWriteRepository = vehicleAccidentWriteRepository;
            _vehicleAccidentBusinessRules = vehicleAccidentBusinessRules;
        }

        public async Task<UploadVehicleAccidentFileResponse> Handle(UploadVehicleAccidentFileCommand request, CancellationToken cancellationToken)
        {
            VehicleAccident? vehicleAccident = await _vehicleAccidentReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
            await _vehicleAccidentBusinessRules.VehicleAccidentShouldExistWhenSelected(vehicleAccident);

            _storageService.FileCopy(request.FileName, "Files/0temp", "Files/accident-files");

            vehicleAccident.AccidentFile = "\\Files\\accident-files\\" + request.FileName;

            _vehicleAccidentWriteRepository.Update(vehicleAccident);
            await _vehicleAccidentWriteRepository.SaveAsync();

            return new()
            {
                FullPath = vehicleAccident.AccidentFile,
                Title = VehicleAccidentsBusinessMessages.ProcessCompleted,
                Message = VehicleAccidentsBusinessMessages.FileUploaded,
                IsValid = true
            };
        }
    }
}

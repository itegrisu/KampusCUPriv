using Application.Abstractions.Storage;
using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Constants;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Rules;
using Application.Features.VehicleManagementsFeatures.VehicleTransactions.Commands.UploadLicenseFile;
using Application.Repositories.VehicleManagementsRepos.VehicleTransactionRepo;
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

namespace Application.Features.VehicleManagementsFeatures.VehicleTransactions.Commands.UploadLicenseFile
{
    public class UploadVehicleTransactionLicenseFileResponse : BaseResponse, IResponse
    {
        public Guid Gid { get; set; }
        public string FullPath { get; set; }
    }

    public class UploadVehicleTransactionLicenseFileCommandHandler : IRequestHandler<UploadVehicleTransactionLicenseFileCommand, UploadVehicleTransactionLicenseFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleTransactionReadRepository _vehicleTransactionReadRepository;
        private readonly IVehicleTransactionWriteRepository _vehicleTransactionWriteRepository;
        private readonly VehicleTransactionBusinessRules _vehicleTransactionBusinessRules;
        private readonly IFileTypeCheckService _fileTypeCheckService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;


        public UploadVehicleTransactionLicenseFileCommandHandler(IMapper mapper, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IVehicleTransactionReadRepository vehicleTransactionReadRepository, IVehicleTransactionWriteRepository vehicleTransactionWriteRepository, VehicleTransactionBusinessRules vehicleTransactionBusinessRules)
        {
            _mapper = mapper;
            _fileTypeCheckService = fileTypeCheckService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _vehicleTransactionReadRepository = vehicleTransactionReadRepository;
            _vehicleTransactionWriteRepository = vehicleTransactionWriteRepository;
            _vehicleTransactionBusinessRules = vehicleTransactionBusinessRules;
        }

        public async Task<UploadVehicleTransactionLicenseFileResponse> Handle(UploadVehicleTransactionLicenseFileCommand request, CancellationToken cancellationToken)
        {
            VehicleTransaction? vehicleTransaction = await _vehicleTransactionReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
            await _vehicleTransactionBusinessRules.VehicleTransactionShouldExistWhenSelected(vehicleTransaction);

            _storageService.FileCopy(request.FileName, "Files/0temp", "Files/License-files");

            vehicleTransaction.LicenseFile = "\\Files\\License-files\\" + request.FileName;

            _vehicleTransactionWriteRepository.Update(vehicleTransaction);
            await _vehicleTransactionWriteRepository.SaveAsync();

            return new()
            {
                FullPath = vehicleTransaction.LicenseFile,
                Title = VehicleTransactionsBusinessMessages.ProcessCompleted,
                Message = VehicleTransactionsBusinessMessages.SucessUploadFile,
                IsValid = true
            };
        }
    }
}

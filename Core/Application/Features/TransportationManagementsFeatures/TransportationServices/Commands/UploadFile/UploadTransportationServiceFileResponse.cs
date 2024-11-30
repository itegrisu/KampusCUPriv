using Application.Abstractions.Storage;
using Application.Features.Base;
using Application.Features.OfferManagementFeatures.OfferFiles.Commands.UploadFile;
using Application.Features.OfferManagementFeatures.OfferFiles.Constants;
using Application.Features.OfferManagementFeatures.OfferFiles.Rules;
using Application.Features.TransportationManagementFeatures.TransportationServices.Constants;
using Application.Features.TransportationManagementFeatures.TransportationServices.Rules;
using Application.Repositories.OfferManagementRepos.OfferFileRepo;
using Application.Repositories.TransportationRepos.TransportationServiceRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.OfferManagements;
using Domain.Entities.TransportationManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.UploadFile
{
    public class UploadTransportationServiceFileResponse : BaseResponse, IResponse
    {
        public Guid Gid { get; set; }
        public string FullPath { get; set; }
    }

    public class UploadTransportationServiceFilCommandHandler : IRequestHandler<UploadTransportationServiceFileCommand, UploadTransportationServiceFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransportationServiceReadRepository _transportationServiceReadRepository;
        private readonly ITransportationServiceWriteRepository _transportationServiceWriteRepository;
        private readonly TransportationServiceBusinessRules _transportationServiceBusinessRules;
        private readonly IFileTypeCheckService _fileTypeCheckService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        public UploadTransportationServiceFilCommandHandler(IMapper mapper, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, ITransportationServiceReadRepository transportationServiceReadRepository, ITransportationServiceWriteRepository transportationServiceWriteRepository, TransportationServiceBusinessRules transportationServiceBusinessRules)
        {
            _mapper = mapper;
            _fileTypeCheckService = fileTypeCheckService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _transportationServiceReadRepository = transportationServiceReadRepository;
            _transportationServiceWriteRepository = transportationServiceWriteRepository;
            _transportationServiceBusinessRules = transportationServiceBusinessRules;
        }

        public async Task<UploadTransportationServiceFileResponse> Handle(UploadTransportationServiceFileCommand request, CancellationToken cancellationToken)
        {


            TransportationService? transportationService = await _transportationServiceReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
            await _transportationServiceBusinessRules.TransportationServiceShouldExistWhenSelected(transportationService);

            _storageService.FileCopy(request.FileName, "Files/0temp", "Files/transportation-service-files");

            transportationService.TransportationFile = "\\Files\\transportation-service-files\\" + request.FileName;

            _transportationServiceWriteRepository.Update(transportationService);
            await _transportationServiceWriteRepository.SaveAsync();

            return new()
            {
                FullPath = transportationService.TransportationFile,
                Title = TransportationServicesBusinessMessages.ProcessCompleted,
                Message = TransportationServicesBusinessMessages.SucessUploadFile,
                IsValid = true
            };
        }
    }
}

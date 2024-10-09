using Application.Abstractions.Storage;
using Application.Features.Base;
using Application.Features.OfferManagementFeatures.OfferFiles.Constants;
using Application.Features.OfferManagementFeatures.OfferFiles.Rules;
using Application.Repositories.OfferManagementRepos.OfferFileRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.OfferManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Application.Features.OfferManagementFeatures.OfferFiles.Commands.UploadFile
{
    public class UploadOfferFileResponse : BaseResponse, IResponse
    {
        public Guid Gid { get; set; }
        public string FullPath { get; set; }
    }

    public class UploadTrainingRequestFileCommandHandler : IRequestHandler<UploadOfferFileCommand, UploadOfferFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOfferFileReadRepository _offerFileReadRepository;
        private readonly IOfferFileWriteRepository _offerFileWriteRepository;
        private readonly OfferFileBusinessRules _offerFileBusinessRules;
        private readonly IFileTypeCheckService _fileTypeCheckService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;


        public UploadTrainingRequestFileCommandHandler(IMapper mapper, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, OfferFileBusinessRules offerFileBusinessRules, IOfferFileReadRepository offerFileReadRepository, IOfferFileWriteRepository offerFileWriteRepository)
        {
            _mapper = mapper;
            _fileTypeCheckService = fileTypeCheckService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _offerFileBusinessRules = offerFileBusinessRules;
            _offerFileReadRepository = offerFileReadRepository;
            _offerFileWriteRepository = offerFileWriteRepository;
        }

        public async Task<UploadOfferFileResponse> Handle(UploadOfferFileCommand request, CancellationToken cancellationToken)
        {


            OfferFile? offerFile = await _offerFileReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
            await _offerFileBusinessRules.OfferFileShouldExistWhenSelected(offerFile);

            _storageService.FileCopy(request.FileName, "Files/0temp", "Files/offer-files");

            offerFile.Document = "\\Files\\offer-files\\" + request.FileName;

            _offerFileWriteRepository.Update(offerFile);
            await _offerFileWriteRepository.SaveAsync();

            return new()
            {
                FullPath = offerFile.Document,
                Title = OfferFilesBusinessMessages.ProcessCompleted,
                Message = OfferFilesBusinessMessages.SucessUploadFile,
                IsValid = true
            };
        }


    }
}

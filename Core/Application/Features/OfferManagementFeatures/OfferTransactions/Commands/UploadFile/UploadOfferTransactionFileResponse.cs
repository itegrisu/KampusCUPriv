using Application.Abstractions.Storage;
using Application.Features.Base;
using Application.Features.OfferManagementFeatures.OfferTransactions.Constants;
using Application.Features.OfferManagementFeatures.OfferTransactions.Rules;
using Application.Repositories.OfferManagementRepos.OfferTransactionRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.OfferManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Application.Features.OfferManagementFeatures.OfferTransactions.Commands.UploadFile
{
    public class UploadOfferTransactionFileResponse : BaseResponse, IResponse
    {
        public Guid Gid { get; set; }
        public string FullPath { get; set; }
    }

    public class UploadOfferTransactionFileCommandHandler : IRequestHandler<UploadOfferFileTransactionCommand, UploadOfferTransactionFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOfferTransactionReadRepository _offerTransactionReadRepository;
        private readonly IOfferTransactionWriteRepository _offerTransactionWriteRepository;
        private readonly OfferTransactionBusinessRules _offerTransactionBusinessRules;
        private readonly IFileTypeCheckService _fileTypeCheckService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public UploadOfferTransactionFileCommandHandler(IMapper mapper, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IOfferTransactionReadRepository offerTransactionReadRepository, IOfferTransactionWriteRepository offerTransactionWriteRepository, OfferTransactionBusinessRules offerTransactionBusinessRules)
        {
            _mapper = mapper;
            _fileTypeCheckService = fileTypeCheckService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _offerTransactionReadRepository = offerTransactionReadRepository;
            _offerTransactionWriteRepository = offerTransactionWriteRepository;
            _offerTransactionBusinessRules = offerTransactionBusinessRules;
        }

        public async Task<UploadOfferTransactionFileResponse> Handle(UploadOfferFileTransactionCommand request, CancellationToken cancellationToken)
        {


            OfferTransaction? offerTransaction = await _offerTransactionReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
            await _offerTransactionBusinessRules.OfferTransactionShouldExistWhenSelected(offerTransaction);

            _storageService.FileCopy(request.FileName, "Files/0temp", "Files/offer-transactions");

            offerTransaction.Document = "\\Files\\offer-transactions\\" + request.FileName;

            _offerTransactionWriteRepository.Update(offerTransaction);
            await _offerTransactionWriteRepository.SaveAsync();

            return new()
            {
                FullPath = offerTransaction.Document,
                Title = OfferTransactionsBusinessMessages.ProcessCompleted,
                Message = OfferTransactionsBusinessMessages.SucessUploadFile,
                IsValid = true
            };
        }


    }

}

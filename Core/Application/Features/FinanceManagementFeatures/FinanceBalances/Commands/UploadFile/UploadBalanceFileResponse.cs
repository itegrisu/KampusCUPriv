using Application.Abstractions.Storage;
using Application.Features.Base;
using Application.Features.FinanceManagementFeatures.FinanceBalances.Constants;
using Application.Features.FinanceManagementFeatures.FinanceBalances.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceBalanceRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.FinanceManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Application.Features.FinanceManagementFeatures.FinanceBalances.Commands.UploadFile
{
    public class UploadBalanceFileResponse : BaseResponse, IResponse
    {
        public Guid Gid { get; set; }
        public string FullPath { get; set; }
    }

    public class UploadBalanceFileCommandHandler : IRequestHandler<UploadBalanceFileCommand, UploadBalanceFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFinanceBalanceReadRepository _financeBalanceReadRepository;
        private readonly IFinanceBalanceWriteRepository _financeBalanceWriteRepository;
        private readonly FinanceBalanceBusinessRules _financeBalanceBusinessRules;
        private readonly IFileTypeCheckService _fileTypeCheckService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;


        public UploadBalanceFileCommandHandler(IMapper mapper, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IFinanceBalanceReadRepository financeBalanceReadRepository, IFinanceBalanceWriteRepository financeBalanceWriteRepository, FinanceBalanceBusinessRules financeBalanceBusinessRules)
        {
            _mapper = mapper;
            _fileTypeCheckService = fileTypeCheckService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _financeBalanceReadRepository = financeBalanceReadRepository;
            _financeBalanceWriteRepository = financeBalanceWriteRepository;
            _financeBalanceBusinessRules = financeBalanceBusinessRules;
        }

        public async Task<UploadBalanceFileResponse> Handle(UploadBalanceFileCommand request, CancellationToken cancellationToken)
        {

            FinanceBalance? balanceFile = await _financeBalanceReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
            await _financeBalanceBusinessRules.FinanceBalanceShouldExistWhenSelected(balanceFile);

            _storageService.FileCopy(request.FileName, "Files/0temp", "Files/balance-files");

            balanceFile.PaymentFile = "\\Files\\balance-files\\" + request.FileName;

            _financeBalanceWriteRepository.Update(balanceFile);
            await _financeBalanceWriteRepository.SaveAsync();

            return new()
            {
                FullPath = balanceFile.PaymentFile,
                Title = FinanceBalancesBusinessMessages.ProcessCompleted,
                Message = FinanceBalancesBusinessMessages.SucessUploadFile,
                IsValid = true
            };
        }


    }
}

using Application.Abstractions.Storage;
using Application.Features.Base;
using Application.Features.FinanceManagementFeatures.FinanceIncomes.Constants;
using Application.Features.FinanceManagementFeatures.FinanceIncomes.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceIncomeRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.FinanceManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomes.Commands.UploadFile
{
    public class UploadFinanceIncomeResponse : BaseResponse, IResponse
    {
        public Guid Gid { get; set; }
        public string FullPath { get; set; }
    }

    public class UploadFinanceIncomeCommandHandler : IRequestHandler<UploadFinanceIncomeCommand, UploadFinanceIncomeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFinanceIncomeReadRepository _financeIncomeReadRepository;
        private readonly IFinanceIncomeWriteRepository _financeIncomeWriteRepository;
        private readonly FinanceIncomeBusinessRules _financeIncomeBusinessRules;
        private readonly IFileTypeCheckService _fileTypeCheckService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;


        public UploadFinanceIncomeCommandHandler(IMapper mapper, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IFinanceIncomeReadRepository financeIncomeReadRepository, IFinanceIncomeWriteRepository financeIncomeWriteRepository, FinanceIncomeBusinessRules financeIncomeBusinessRules)
        {
            _mapper = mapper;
            _fileTypeCheckService = fileTypeCheckService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _financeIncomeReadRepository = financeIncomeReadRepository;
            _financeIncomeWriteRepository = financeIncomeWriteRepository;
            _financeIncomeBusinessRules = financeIncomeBusinessRules;
        }

        public async Task<UploadFinanceIncomeResponse> Handle(UploadFinanceIncomeCommand request, CancellationToken cancellationToken)
        {


            FinanceIncome? financeIncome = await _financeIncomeReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
            await _financeIncomeBusinessRules.FinanceIncomeShouldExistWhenSelected(financeIncome);

            _storageService.FileCopy(request.FileName, "Files/0temp", "Files/finance-income-files");

            financeIncome.Document = "\\Files\\finance-income-files\\" + request.FileName;

            _financeIncomeWriteRepository.Update(financeIncome);
            await _financeIncomeWriteRepository.SaveAsync();

            return new()
            {
                FullPath = financeIncome.Document,
                Title = FinanceIncomesBusinessMessages.ProcessCompleted,
                Message = FinanceIncomesBusinessMessages.SucessUploadFile,
                IsValid = true
            };
        }


    }
}

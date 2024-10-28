using Application.Abstractions.Storage;
using Application.Features.Base;
using Application.Features.FinanceManagementFeatures.FinanceExpenses.Constants;
using Application.Features.FinanceManagementFeatures.FinanceExpenses.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.FinanceManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenses.Commands.FileUpload
{
    public class UploadFinanceExpenseResponse : BaseResponse, IResponse
    {
        public Guid Gid { get; set; }
        public string FullPath { get; set; }
    }

    public class UploadFinanceExpenseCommandHandler : IRequestHandler<UploadFinanceExpenseCommand, UploadFinanceExpenseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFinanceExpenseReadRepository _financeExpenseReadRepository;
        private readonly IFinanceExpenseWriteRepository _financeExpenseWriteRepository;
        private readonly FinanceExpenseBusinessRules _financeExpenseBusinessRules;
        private readonly IFileTypeCheckService _fileTypeCheckService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;


        public UploadFinanceExpenseCommandHandler(IMapper mapper, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IFinanceExpenseReadRepository financeExpenseReadRepository, IFinanceExpenseWriteRepository financeExpenseWriteRepository, FinanceExpenseBusinessRules financeExpenseBusinessRules)
        {
            _mapper = mapper;
            _fileTypeCheckService = fileTypeCheckService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _financeExpenseReadRepository = financeExpenseReadRepository;
            _financeExpenseWriteRepository = financeExpenseWriteRepository;
            _financeExpenseBusinessRules = financeExpenseBusinessRules;
        }

        public async Task<UploadFinanceExpenseResponse> Handle(UploadFinanceExpenseCommand request, CancellationToken cancellationToken)
        {
            FinanceExpense? financeExpense = await _financeExpenseReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
            await _financeExpenseBusinessRules.FinanceExpenseShouldExistWhenSelected(financeExpense);

            _storageService.FileCopy(request.FileName, "Files/0temp", "Files/finance-expense-files");

            financeExpense.Document = "\\Files\\finance-expense-files\\" + request.FileName;

            _financeExpenseWriteRepository.Update(financeExpense);
            await _financeExpenseWriteRepository.SaveAsync();

            return new()
            {
                FullPath = financeExpense.Document,
                Title = FinanceExpensesBusinessMessages.ProcessCompleted,
                Message = FinanceExpensesBusinessMessages.SucessUploadFile,
                IsValid = true
            };
        }


    }
}

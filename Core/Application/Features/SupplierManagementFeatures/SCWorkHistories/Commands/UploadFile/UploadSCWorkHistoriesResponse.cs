using Application.Abstractions.Storage;
using Application.Features.Base;
using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Constants;
using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Rules;
using Application.Repositories.SupplierManagementRepos.SCWorkHistoryRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities.SupplierCustomerManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Application.Features.SupplierManagementFeatures.SCWorkHistories.Commands.UploadFile
{
    public class UploadSCWorkHistoriesResponse : BaseResponse, IResponse
    {
        public Guid Gid { get; set; }
        public string FullPath { get; set; }
    }

    public class UploadSCWorkHistoriesCommandHandler : IRequestHandler<UploadSCWorkHistoriesCommand, UploadSCWorkHistoriesResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISCWorkHistoryReadRepository _sCWorkHistoryReadRepository;
        private readonly ISCWorkHistoryWriteRepository _sCWorkHistoryWriteRepository;
        private readonly SCWorkHistoryBusinessRules _sCWorkHistoryBusinessRules;
        private readonly IFileTypeCheckService _fileTypeCheckService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public UploadSCWorkHistoriesCommandHandler(IMapper mapper, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, ISCWorkHistoryReadRepository sCWorkHistoryReadRepository, ISCWorkHistoryWriteRepository sCWorkHistoryWriteRepository, SCWorkHistoryBusinessRules sCWorkHistoryBusinessRules)
        {
            _mapper = mapper;
            _fileTypeCheckService = fileTypeCheckService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _sCWorkHistoryReadRepository = sCWorkHistoryReadRepository;
            _sCWorkHistoryWriteRepository = sCWorkHistoryWriteRepository;
            _sCWorkHistoryBusinessRules = sCWorkHistoryBusinessRules;
        }

        public async Task<UploadSCWorkHistoriesResponse> Handle(UploadSCWorkHistoriesCommand request, CancellationToken cancellationToken)
        {


            SCWorkHistory? sCWorkHistory = await _sCWorkHistoryReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
            await _sCWorkHistoryBusinessRules.SCWorkHistoryShouldExistWhenSelected(sCWorkHistory);

            _storageService.FileCopy(request.FileName, "Files/0temp", "Files/sc-work-histories");

            sCWorkHistory.WorkFile = "\\Files\\sc-work-histories\\" + request.FileName;

            _sCWorkHistoryWriteRepository.Update(sCWorkHistory);
            await _sCWorkHistoryWriteRepository.SaveAsync();

            return new()
            {
                FullPath = sCWorkHistory.WorkFile,
                Title = SCWorkHistoriesBusinessMessages.ProcessCompleted,
                Message = SCWorkHistoriesBusinessMessages.SucessUploadFile,
                IsValid = true
            };
        }


    }
}

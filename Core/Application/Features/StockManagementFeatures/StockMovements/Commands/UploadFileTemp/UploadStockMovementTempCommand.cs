using Application.Abstractions.Storage;
using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Features.GeneralManagementFeatures.Users.Rules;
using Application.Features.StockManagementFeatures.StockMovements.Rules;
using Application.Repositories.StockManagementRepos.StockMovementRepo;
using Domain.Entities.StockManagements;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.StockManagementFeatures.StockMovements.Commands.UploadFileTemp
{
    public class UploadStockMovementTempCommand : IRequest<UploadStockMovementTempResponse>
    {
        public string Params { get; set; }
        public IFormFileCollection? FormFiles { get; set; }

        public class UploadStockMovementTempCommandHandler : IRequestHandler<UploadStockMovementTempCommand, UploadStockMovementTempResponse>
        {
            private readonly StockMovementBusinessRules _stockMovementBusinessRules;
            private readonly IStockMovementWriteRepository _stockMovementWriteRepository;
            private readonly IStockMovementReadRepository _stockMovementReadRepository;
            private readonly IFileTypeCheckService _fileTypeCheckService;
            private readonly IStorageService _storageService;
            private readonly UserBusinessRules _userCustomBusinessRules;

            public UploadStockMovementTempCommandHandler(StockMovementBusinessRules stockMovementBusinessRules, IStockMovementWriteRepository stockMovementWriteRepository, IStockMovementReadRepository stockMovementReadRepository, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, UserBusinessRules userCustomBusinessRules)
            {
                _stockMovementBusinessRules = stockMovementBusinessRules;
                _stockMovementWriteRepository = stockMovementWriteRepository;
                _stockMovementReadRepository = stockMovementReadRepository;
                _fileTypeCheckService = fileTypeCheckService;
                _storageService = storageService;
                _userCustomBusinessRules = userCustomBusinessRules;
            }

            public async Task<UploadStockMovementTempResponse> Handle(UploadStockMovementTempCommand request, CancellationToken cancellationToken)
            {
                StockMovement? _stockMovement = await _stockMovementReadRepository.GetSingleAsync(u => u.Gid.ToString() == request.Params);
                await _stockMovementBusinessRules.StockMovementShouldExistWhenSelected(_stockMovement);

                string extension = Path.GetExtension(request.FormFiles[0].FileName);

                string[] allowedExtensions = new string[] { ".png", ".jpg", ".jpeg", ".pdf" };

                await _userCustomBusinessRules.FileTypeCheck(extension, allowedExtensions, request.FormFiles);

                List<(string fileName, string pathOrContainer)> datas = await _storageService.UploadAsync("Files\\0temp", request.FormFiles); //tempteki dosya

                return new()
                {
                    Title = UsersBusinessMessages.ProcessCompleted,
                    Message = UsersBusinessMessages.FileReadyForUpload,
                    IsValid = true,
                    FileName = datas[0].fileName,
                };
            }
        }
    }
}

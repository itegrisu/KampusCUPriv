using Application.Abstractions.Storage;
using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Features.GeneralManagementFeatures.Users.Rules;
using Application.Features.StockManagementFeatures.StockCardImages.Rules;
using Application.Repositories.StockManagementRepos.StockCardImageRepo;
using Domain.Entities.StockManagements;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.StockManagementFeatures.StockCardImages.Commands.UploadFileTemp
{
    public class UploadStockCardImageTempCommand : IRequest<UploadStockCardImageTempResponse>
    {
        public string Params { get; set; }
        public IFormFileCollection? FormFiles { get; set; }

        public class UploadStockCardImageTempCommandHandler : IRequestHandler<UploadStockCardImageTempCommand, UploadStockCardImageTempResponse>
        {
            private readonly StockCardImageBusinessRules _stockCardImageBusinessRules;
            private readonly IStockCardImageReadRepository _stockCardImageReadRepository;
            private readonly IFileTypeCheckService _fileTypeCheckService;
            private readonly IStorageService _storageService;
            private readonly UserBusinessRules _userCustomBusinessRules;

            public UploadStockCardImageTempCommandHandler(StockCardImageBusinessRules stockCardImageBusinessRules, IStockCardImageReadRepository stockCardImageReadRepository, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, UserBusinessRules userCustomBusinessRules)
            {
                _stockCardImageBusinessRules = stockCardImageBusinessRules;
                _stockCardImageReadRepository = stockCardImageReadRepository;
                _fileTypeCheckService = fileTypeCheckService;
                _storageService = storageService;
                _userCustomBusinessRules = userCustomBusinessRules;
            }

            public async Task<UploadStockCardImageTempResponse> Handle(UploadStockCardImageTempCommand request, CancellationToken cancellationToken)
            {
                StockCardImage? image = await _stockCardImageReadRepository.GetSingleAsync(u => u.Gid.ToString() == request.Params);
                await _stockCardImageBusinessRules.StockCardImageShouldExistWhenSelected(image);

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

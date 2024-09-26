using Application.Abstractions.Storage;
using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Features.StockManagementFeatures.StockCardImages.Constants;
using Application.Features.StockManagementFeatures.StockCardImages.Rules;
using Application.Repositories.StockManagementRepos.StockCardImageRepo;
using AutoMapper;
using Domain.Entities.StockManagements;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Application.Features.StockManagementFeatures.StockCardImages.Commands.UploadFile
{
    public class UploadStockCardImageCommand : IRequest<UploadStockCardImageResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }

        public class UploadStockCardImageCommandHandler : IRequestHandler<UploadStockCardImageCommand, UploadStockCardImageResponse>
        {
            private readonly IMapper _mapper;
            private readonly StockCardImageBusinessRules _stockCardImageBusinessRules;
            private readonly IStockCardImageReadRepository _stockCardImageReadRepository;
            private readonly IStockCardImageWriteRepository _stockCardImageWriteRepository;
            private readonly IFileTypeCheckService _fileTypeCheckService;
            private readonly IStorageService _storageService;
            private readonly IWebHostEnvironment _webHostEnvironment;
            private readonly IConfiguration _configuration;


            public UploadStockCardImageCommandHandler(IMapper mapper, StockCardImageBusinessRules stockCardImageBusinessRules, IStockCardImageReadRepository stockCardImageReadRepository, IStockCardImageWriteRepository stockCardImageWriteRepository, IFileTypeCheckService fileTypeCheckService, IStorageService storageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
            {
                _mapper = mapper;
                _stockCardImageBusinessRules = stockCardImageBusinessRules;
                _stockCardImageReadRepository = stockCardImageReadRepository;
                _stockCardImageWriteRepository = stockCardImageWriteRepository;
                _fileTypeCheckService = fileTypeCheckService;
                _storageService = storageService;
                _webHostEnvironment = webHostEnvironment;
                _configuration = configuration;
            }

            public async Task<UploadStockCardImageResponse> Handle(UploadStockCardImageCommand request, CancellationToken cancellationToken)
            {


                StockCardImage? image = await _stockCardImageReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
                await _stockCardImageBusinessRules.StockCardImageShouldExistWhenSelected(image);

                _storageService.FileCopy(request.FileName, "Files/0temp", "Files/stock-card-image");

                image.Image = "\\Files\\stock-card-image\\" + request.FileName;

                _stockCardImageWriteRepository.Update(image);
                await _stockCardImageWriteRepository.SaveAsync();

                return new()
                {
                    FullPath = image.Image,
                    Title = UsersBusinessMessages.ProcessCompleted,
                    Message = StockCardImagesBusinessMessages.SuccessUploadedStockCardImageMessage,
                    IsValid = true
                };
            }


        }
    }
}
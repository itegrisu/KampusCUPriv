using Application.Abstractions.Storage;
using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Features.ProjectManagementFeatures.ProjectFiles.Commands;
using Application.Features.StockManagementFeatures.StockMovements.Rules;
using Application.Repositories.StockManagementRepos.StockMovementRepo;
using AutoMapper;
using Domain.Entities.StockManagements;
using MediatR;

namespace Application.Features.StockManagementFeatures.StockMovements.Commands.UploadFile
{
    public class UploadStockMovementCommand : IRequest<UploadStockMovementResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
        public class UploadStockMovementCommandHandler : IRequestHandler<UploadStockMovementCommand, UploadStockMovementResponse>
        {
            private readonly IMapper _mapper;
            private readonly StockMovementBusinessRules _stockMovementBusinessRules;
            private readonly IStockMovementWriteRepository _stockMovementWriteRepository;
            private readonly IStockMovementReadRepository _stockMovementReadRepository;
            private readonly IStorageService _storageService;

            public UploadStockMovementCommandHandler(IMapper mapper, StockMovementBusinessRules stockMovementBusinessRules, IStockMovementWriteRepository stockMovementWriteRepository, IStockMovementReadRepository stockMovementReadRepository, IStorageService storageService)
            {
                _mapper = mapper;
                _stockMovementBusinessRules = stockMovementBusinessRules;
                _stockMovementWriteRepository = stockMovementWriteRepository;
                _stockMovementReadRepository = stockMovementReadRepository;
                _storageService = storageService;
            }

            public async Task<UploadStockMovementResponse> Handle(UploadStockMovementCommand request, CancellationToken cancellationToken)
            {


                StockMovement? stockMovement = await _stockMovementReadRepository.GetSingleAsync(u => u.Gid == request.Gid);
                await _stockMovementBusinessRules.StockMovementShouldExistWhenSelected(stockMovement);

                _storageService.FileCopy(request.FileName, "Files/0temp", "Files/stock-movement-document");

                stockMovement.Document = "\\Files\\stock-movement-document\\" + request.FileName;

                _stockMovementWriteRepository.Update(stockMovement);
                await _stockMovementWriteRepository.SaveAsync();

                return new()
                {
                    FullPath = stockMovement.Document,
                    Title = UsersBusinessMessages.ProcessCompleted,
                    Message = UsersBusinessMessages.SucessUploadAvatarImagesMessage,
                    IsValid = true
                };
            }


        }
    }
}
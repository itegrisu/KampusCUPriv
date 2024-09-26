using Application.Features.StockManagementFeatures.StockCardImages.Constants;
using Application.Features.StockManagementFeatures.StockCardImages.Rules;
using Application.Repositories.StockManagementRepos.StockCardImageRepo;
using AutoMapper;
using X = Domain.Entities.StockManagements;
using MediatR;

namespace Application.Features.StockManagementFeatures.StockCardImages.Commands.Delete;

public class DeleteStockCardImageCommand : IRequest<DeletedStockCardImageResponse>
{
	public Guid Gid { get; set; }

    public class DeleteStockCardImageCommandHandler : IRequestHandler<DeleteStockCardImageCommand, DeletedStockCardImageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStockCardImageReadRepository _stockCardImageReadRepository;
        private readonly IStockCardImageWriteRepository _stockCardImageWriteRepository;
        private readonly StockCardImageBusinessRules _stockCardImageBusinessRules;

        public DeleteStockCardImageCommandHandler(IMapper mapper, IStockCardImageReadRepository stockCardImageReadRepository,
                                         StockCardImageBusinessRules stockCardImageBusinessRules, IStockCardImageWriteRepository stockCardImageWriteRepository)
        {
            _mapper = mapper;
            _stockCardImageReadRepository = stockCardImageReadRepository;
            _stockCardImageBusinessRules = stockCardImageBusinessRules;
            _stockCardImageWriteRepository = stockCardImageWriteRepository;
        }

        public async Task<DeletedStockCardImageResponse> Handle(DeleteStockCardImageCommand request, CancellationToken cancellationToken)
        {
            X.StockCardImage? stockCardImage = await _stockCardImageReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _stockCardImageBusinessRules.StockCardImageShouldExistWhenSelected(stockCardImage);
            stockCardImage.DataState = Core.Enum.DataState.Deleted;

            _stockCardImageWriteRepository.Update(stockCardImage);
            await _stockCardImageWriteRepository.SaveAsync();

            return new()
            {
                Title = StockCardImagesBusinessMessages.ProcessCompleted,
                Message = StockCardImagesBusinessMessages.SuccessDeletedStockCardImageMessage,
                IsValid = true
            };
        }
    }
}
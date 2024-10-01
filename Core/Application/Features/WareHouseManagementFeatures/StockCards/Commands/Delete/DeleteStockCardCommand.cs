using Application.Features.WarehouseManagementFeatures.StockCards.Constants;
using Application.Features.WarehouseManagementFeatures.StockCards.Rules;
using Application.Repositories.WarehouseManagementRepos.StockCardRepo;
using AutoMapper;
using X = Domain.Entities.WarehouseManagements;
using MediatR;

namespace Application.Features.WarehouseManagementFeatures.StockCards.Commands.Delete;

public class DeleteStockCardCommand : IRequest<DeletedStockCardResponse>
{
	public Guid Gid { get; set; }

    public class DeleteStockCardCommandHandler : IRequestHandler<DeleteStockCardCommand, DeletedStockCardResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStockCardReadRepository _stockCardReadRepository;
        private readonly IStockCardWriteRepository _stockCardWriteRepository;
        private readonly StockCardBusinessRules _stockCardBusinessRules;

        public DeleteStockCardCommandHandler(IMapper mapper, IStockCardReadRepository stockCardReadRepository,
                                         StockCardBusinessRules stockCardBusinessRules, IStockCardWriteRepository stockCardWriteRepository)
        {
            _mapper = mapper;
            _stockCardReadRepository = stockCardReadRepository;
            _stockCardBusinessRules = stockCardBusinessRules;
            _stockCardWriteRepository = stockCardWriteRepository;
        }

        public async Task<DeletedStockCardResponse> Handle(DeleteStockCardCommand request, CancellationToken cancellationToken)
        {
            X.StockCard? stockCard = await _stockCardReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _stockCardBusinessRules.StockCardShouldExistWhenSelected(stockCard);
            stockCard.DataState = Core.Enum.DataState.Deleted;

            _stockCardWriteRepository.Update(stockCard);
            await _stockCardWriteRepository.SaveAsync();

            return new()
            {
                Title = StockCardsBusinessMessages.ProcessCompleted,
                Message = StockCardsBusinessMessages.SuccessDeletedStockCardMessage,
                IsValid = true
            };
        }
    }
}
using Application.Features.StockManagementFeatures.StockMovements.Constants;
using Application.Features.StockManagementFeatures.StockMovements.Rules;
using Application.Repositories.StockManagementRepos.StockMovementRepo;
using AutoMapper;
using X = Domain.Entities.StockManagements;
using MediatR;

namespace Application.Features.StockManagementFeatures.StockMovements.Commands.Delete;

public class DeleteStockMovementCommand : IRequest<DeletedStockMovementResponse>
{
	public Guid Gid { get; set; }

    public class DeleteStockMovementCommandHandler : IRequestHandler<DeleteStockMovementCommand, DeletedStockMovementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStockMovementReadRepository _stockMovementReadRepository;
        private readonly IStockMovementWriteRepository _stockMovementWriteRepository;
        private readonly StockMovementBusinessRules _stockMovementBusinessRules;

        public DeleteStockMovementCommandHandler(IMapper mapper, IStockMovementReadRepository stockMovementReadRepository,
                                         StockMovementBusinessRules stockMovementBusinessRules, IStockMovementWriteRepository stockMovementWriteRepository)
        {
            _mapper = mapper;
            _stockMovementReadRepository = stockMovementReadRepository;
            _stockMovementBusinessRules = stockMovementBusinessRules;
            _stockMovementWriteRepository = stockMovementWriteRepository;
        }

        public async Task<DeletedStockMovementResponse> Handle(DeleteStockMovementCommand request, CancellationToken cancellationToken)
        {
            X.StockMovement? stockMovement = await _stockMovementReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _stockMovementBusinessRules.StockMovementShouldExistWhenSelected(stockMovement);
            stockMovement.DataState = Core.Enum.DataState.Deleted;

            _stockMovementWriteRepository.Update(stockMovement);
            await _stockMovementWriteRepository.SaveAsync();

            return new()
            {
                Title = StockMovementsBusinessMessages.ProcessCompleted,
                Message = StockMovementsBusinessMessages.SuccessDeletedStockMovementMessage,
                IsValid = true
            };
        }
    }
}
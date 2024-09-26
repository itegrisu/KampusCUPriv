using Application.Features.StockManagementFeatures.StockMovements.Constants;
using Application.Features.StockManagementFeatures.StockMovements.Queries.GetByGid;
using Application.Features.StockManagementFeatures.StockMovements.Rules;
using Application.Repositories.StockManagementRepos.StockMovementRepo;
using AutoMapper;
using X = Domain.Entities.StockManagements;
using MediatR;
using Domain.Enums;

namespace Application.Features.StockManagementFeatures.StockMovements.Commands.Update;

public class UpdateStockMovementCommand : IRequest<UpdatedStockMovementResponse>
{
    public Guid Gid { get; set; }

    public Guid GidStockCardFK { get; set; }
    public string GidPreviousWarehouseFK { get; set; }
    public string GidNextWarehouseFK { get; set; }
    public EnumOperationType OperationType { get; set; }
    public EnumMovementType MovementType { get; set; }
    public DateTime TransactionDate { get; set; }
    public int Amount { get; set; }
    public string? Document { get; set; }
    public string? Description { get; set; }



    public class UpdateStockMovementCommandHandler : IRequestHandler<UpdateStockMovementCommand, UpdatedStockMovementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStockMovementWriteRepository _stockMovementWriteRepository;
        private readonly IStockMovementReadRepository _stockMovementReadRepository;
        private readonly StockMovementBusinessRules _stockMovementBusinessRules;

        public UpdateStockMovementCommandHandler(IMapper mapper, IStockMovementWriteRepository stockMovementWriteRepository,
                                         StockMovementBusinessRules stockMovementBusinessRules, IStockMovementReadRepository stockMovementReadRepository)
        {
            _mapper = mapper;
            _stockMovementWriteRepository = stockMovementWriteRepository;
            _stockMovementBusinessRules = stockMovementBusinessRules;
            _stockMovementReadRepository = stockMovementReadRepository;
        }

        public async Task<UpdatedStockMovementResponse> Handle(UpdateStockMovementCommand request, CancellationToken cancellationToken)
        {
            X.StockMovement? stockMovement = await _stockMovementReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _stockMovementBusinessRules.StockMovementShouldExistWhenSelected(stockMovement);
            stockMovement = _mapper.Map(request, stockMovement);

            _stockMovementWriteRepository.Update(stockMovement!);
            await _stockMovementWriteRepository.SaveAsync();
            GetByGidStockMovementResponse obj = _mapper.Map<GetByGidStockMovementResponse>(stockMovement);

            return new()
            {
                Title = StockMovementsBusinessMessages.ProcessCompleted,
                Message = StockMovementsBusinessMessages.SuccessCreatedStockMovementMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
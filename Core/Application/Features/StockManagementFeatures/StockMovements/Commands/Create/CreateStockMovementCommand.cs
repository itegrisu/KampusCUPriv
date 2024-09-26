using Application.Features.StockManagementFeatures.StockMovements.Constants;
using Application.Features.StockManagementFeatures.StockMovements.Queries.GetByGid;
using Application.Features.StockManagementFeatures.StockMovements.Rules;
using Application.Repositories.StockManagementRepos.StockMovementRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.StockManagements;

namespace Application.Features.StockManagementFeatures.StockMovements.Commands.Create;

public class CreateStockMovementCommand : IRequest<CreatedStockMovementResponse>
{
    public Guid GidStockCardFK { get; set; }
    public Guid? GidPreviousWarehouseFK { get; set; }
    public Guid? GidNextWarehouseFK { get; set; }
    public EnumOperationType OperationType { get; set; }
    public EnumMovementType MovementType { get; set; }
    public DateTime TransactionDate { get; set; }
    public int Amount { get; set; }
    public string? Description { get; set; }



    public class CreateStockMovementCommandHandler : IRequestHandler<CreateStockMovementCommand, CreatedStockMovementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStockMovementWriteRepository _stockMovementWriteRepository;
        private readonly IStockMovementReadRepository _stockMovementReadRepository;
        private readonly StockMovementBusinessRules _stockMovementBusinessRules;

        public CreateStockMovementCommandHandler(IMapper mapper, IStockMovementWriteRepository stockMovementWriteRepository,
                                         StockMovementBusinessRules stockMovementBusinessRules, IStockMovementReadRepository stockMovementReadRepository)
        {
            _mapper = mapper;
            _stockMovementWriteRepository = stockMovementWriteRepository;
            _stockMovementBusinessRules = stockMovementBusinessRules;
            _stockMovementReadRepository = stockMovementReadRepository;
        }

        public async Task<CreatedStockMovementResponse> Handle(CreateStockMovementCommand request, CancellationToken cancellationToken)
        {
            X.StockMovement stockMovement = _mapper.Map<X.StockMovement>(request);

            await _stockMovementWriteRepository.AddAsync(stockMovement);
            await _stockMovementWriteRepository.SaveAsync();

            X.StockMovement savedStockMovement = await _stockMovementReadRepository.GetAsync(predicate: x => x.Gid == stockMovement.Gid,
            include: x => x.Include(x => x.StockCardFK).Include(x => x.NextWarehouseFK).Include(x => x.PreviousWarehouseFK));

            GetByGidStockMovementResponse obj = _mapper.Map<GetByGidStockMovementResponse>(savedStockMovement);
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
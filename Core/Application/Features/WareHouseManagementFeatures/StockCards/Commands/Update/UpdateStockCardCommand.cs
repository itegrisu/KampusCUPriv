using Application.Features.WarehouseManagementFeatures.StockCards.Constants;
using Application.Features.WarehouseManagementFeatures.StockCards.Queries.GetByGid;
using Application.Features.WarehouseManagementFeatures.StockCards.Rules;
using Application.Repositories.WarehouseManagementRepos.StockCardRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.WarehouseManagements;

namespace Application.Features.WarehouseManagementFeatures.StockCards.Commands.Update;

public class UpdateStockCardCommand : IRequest<UpdatedStockCardResponse>
{
    public Guid Gid { get; set; }

    public Guid GidStockCategoryFK { get; set; }
    public Guid GidMeasureFK { get; set; }

    public EnumStockType StockType { get; set; }
    public string StockName { get; set; }
    public string? StockCode { get; set; }
    public string? Brand { get; set; }
    public string? Description { get; set; }



    public class UpdateStockCardCommandHandler : IRequestHandler<UpdateStockCardCommand, UpdatedStockCardResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStockCardWriteRepository _stockCardWriteRepository;
        private readonly IStockCardReadRepository _stockCardReadRepository;
        private readonly StockCardBusinessRules _stockCardBusinessRules;

        public UpdateStockCardCommandHandler(IMapper mapper, IStockCardWriteRepository stockCardWriteRepository,
                                         StockCardBusinessRules stockCardBusinessRules, IStockCardReadRepository stockCardReadRepository)
        {
            _mapper = mapper;
            _stockCardWriteRepository = stockCardWriteRepository;
            _stockCardBusinessRules = stockCardBusinessRules;
            _stockCardReadRepository = stockCardReadRepository;
        }

        public async Task<UpdatedStockCardResponse> Handle(UpdateStockCardCommand request, CancellationToken cancellationToken)
        {
            X.StockCard? stockCard = await _stockCardReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _stockCardBusinessRules.StockCardShouldExistWhenSelected(stockCard);
            await _stockCardBusinessRules.StockCategorySouldExistWhenSelected(request.GidStockCategoryFK);
            await _stockCardBusinessRules.MeasureShouldExistWhenSelected(request.GidMeasureFK);
            await _stockCardBusinessRules.StockNameShouldUnique(request.StockName, request.Gid);
            await _stockCardBusinessRules.StockCodeShouldUnique(request.StockCode, request.Gid);
            stockCard = _mapper.Map(request, stockCard);

            _stockCardWriteRepository.Update(stockCard!);
            await _stockCardWriteRepository.SaveAsync();
            X.StockCard updatedStockCard = await _stockCardReadRepository.GetAsync(predicate: x => x.Gid == stockCard.Gid, include: x => x.Include(x => x.MeasureTypeFK).Include(x => x.StockCategoryFK));
            GetByGidStockCardResponse obj = _mapper.Map<GetByGidStockCardResponse>(updatedStockCard);

            return new()
            {
                Title = StockCardsBusinessMessages.ProcessCompleted,
                Message = StockCardsBusinessMessages.SuccessCreatedStockCardMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
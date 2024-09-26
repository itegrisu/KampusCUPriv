using Application.Features.StockManagementFeatures.StockCards.Constants;
using Application.Features.StockManagementFeatures.StockCards.Queries.GetByGid;
using Application.Features.StockManagementFeatures.StockCards.Rules;
using Application.Repositories.StockManagementRepos.StockCardRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.StockManagements;

namespace Application.Features.StockManagementFeatures.StockCards.Commands.Update;

public class UpdateStockCardCommand : IRequest<UpdatedStockCardResponse>
{
    public Guid Gid { get; set; }

    //public Guid GidStockCategoryFK { get; set; }
    //public Guid GidBrandFK { get; set; }
    //public Guid GidUnitFK { get; set; }
    //public Guid GidPriceCurrencyFK { get; set; }

    //  public EnumCardType CardType { get; set; }
    public string? StockCode { get; set; }
    public string StockName { get; set; }
    public decimal Price { get; set; }
    public int TaxRate { get; set; }
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

            stockCard = _mapper.Map(request, stockCard);

            _stockCardWriteRepository.Update(stockCard!);
            await _stockCardWriteRepository.SaveAsync();

            X.StockCard updatedStockCard = await _stockCardReadRepository.GetAsync(predicate: x => x.Gid == stockCard.Gid
               );



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
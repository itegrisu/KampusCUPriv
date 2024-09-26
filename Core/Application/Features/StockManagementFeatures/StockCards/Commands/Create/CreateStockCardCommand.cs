using Application.Features.StockManagementFeatures.StockCards.Constants;
using Application.Features.StockManagementFeatures.StockCards.Queries.GetByGid;
using Application.Features.StockManagementFeatures.StockCards.Rules;
using Application.Repositories.StockManagementRepos.StockCardRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.StockManagements;

namespace Application.Features.StockManagementFeatures.StockCards.Commands.Create;

public class CreateStockCardCommand : IRequest<CreatedStockCardResponse>
{
    //public Guid GidStockCategoryFK { get; set; }
    //public Guid GidBrandFK { get; set; }
    //public Guid GidUnitFK { get; set; }
    //public Guid GidPriceCurrencyFK { get; set; }
    //public EnumStockCardType CardType { get; set; }
    public string? StockCode { get; set; }
    public string StockName { get; set; }
    public decimal Price { get; set; }
    public int TaxRate { get; set; }
    public string? Description { get; set; }



    public class CreateStockCardCommandHandler : IRequestHandler<CreateStockCardCommand, CreatedStockCardResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStockCardWriteRepository _stockCardWriteRepository;
        private readonly IStockCardReadRepository _stockCardReadRepository;
        private readonly StockCardBusinessRules _stockCardBusinessRules;

        public CreateStockCardCommandHandler(IMapper mapper, IStockCardWriteRepository stockCardWriteRepository,
                                         StockCardBusinessRules stockCardBusinessRules, IStockCardReadRepository stockCardReadRepository)
        {
            _mapper = mapper;
            _stockCardWriteRepository = stockCardWriteRepository;
            _stockCardBusinessRules = stockCardBusinessRules;
            _stockCardReadRepository = stockCardReadRepository;
        }

        public async Task<CreatedStockCardResponse> Handle(CreateStockCardCommand request, CancellationToken cancellationToken)
        {

            //await _stockCardBusinessRules.CurrencyShouldExistWhenSelected(request.GidPriceCurrencyFK);
            //await _stockCardBusinessRules.UnitShouldExistWhenSelected(request.GidUnitFK);
            //await _stockCardBusinessRules.BrandShouldExistWhenSelected(request.GidBrandFK);
            //await _stockCardBusinessRules.CategoryShouldExistWhenSelected(request.GidStockCategoryFK);

            X.StockCard stockCard = _mapper.Map<X.StockCard>(request);

            await _stockCardWriteRepository.AddAsync(stockCard);
            await _stockCardWriteRepository.SaveAsync();

            X.StockCard savedStockCard = await _stockCardReadRepository.GetAsync(predicate: x => x.Gid == stockCard.Gid
                );


            GetByGidStockCardResponse obj = _mapper.Map<GetByGidStockCardResponse>(savedStockCard);
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
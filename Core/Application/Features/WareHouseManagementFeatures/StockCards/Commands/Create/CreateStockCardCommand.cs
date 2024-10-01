using Application.Features.WarehouseManagementFeatures.StockCards.Constants;
using Application.Features.WarehouseManagementFeatures.StockCards.Queries.GetByGid;
using Application.Features.WarehouseManagementFeatures.StockCards.Rules;
using Application.Repositories.WarehouseManagementRepos.StockCardRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.WarehouseManagements;

namespace Application.Features.WarehouseManagementFeatures.StockCards.Commands.Create;

public class CreateStockCardCommand : IRequest<CreatedStockCardResponse>
{
    public Guid GidStockCategoryFK { get; set; }
    public Guid GidMeasureFK { get; set; }
    public EnumStockType StockType { get; set; }
    public string StockName { get; set; }
    public string? StockCode { get; set; }
    public string? Brand { get; set; }
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

            await _stockCardBusinessRules.StockCategorySouldExistWhenSelected(request.GidStockCategoryFK);
            await _stockCardBusinessRules.MeasureShouldExistWhenSelected(request.GidMeasureFK);
            await _stockCardBusinessRules.StockNameShouldUnique(request.StockName);
            await _stockCardBusinessRules.StockCodeShouldUnique(request.StockCode);


            X.StockCard stockCard = _mapper.Map<X.StockCard>(request);
            //stockCard.RowNo = maxRowNo + 1;

            await _stockCardWriteRepository.AddAsync(stockCard);
            await _stockCardWriteRepository.SaveAsync();

            X.StockCard savedStockCard = await _stockCardReadRepository.GetAsync(predicate: x => x.Gid == stockCard.Gid, include: x => x.Include(x => x.MeasureTypeFK).Include(x => x.StockCategoryFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

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
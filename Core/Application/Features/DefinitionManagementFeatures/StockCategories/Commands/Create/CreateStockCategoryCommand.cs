using Application.Features.DefinitionManagementFeatures.StockCategories.Constants;
using Application.Features.DefinitionManagementFeatures.StockCategories.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.StockCategories.Rules;
using Application.Repositories.DefinitionManagementRepos.StockCategoryRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.StockCategories.Commands.Create;

public class CreateStockCategoryCommand : IRequest<CreatedStockCategoryResponse>
{

    public string Name { get; set; }
    public string? Code { get; set; }



    public class CreateStockCategoryCommandHandler : IRequestHandler<CreateStockCategoryCommand, CreatedStockCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStockCategoryWriteRepository _stockCategoryWriteRepository;
        private readonly IStockCategoryReadRepository _stockCategoryReadRepository;
        private readonly StockCategoryBusinessRules _stockCategoryBusinessRules;

        public CreateStockCategoryCommandHandler(IMapper mapper, IStockCategoryWriteRepository stockCategoryWriteRepository,
                                         StockCategoryBusinessRules stockCategoryBusinessRules, IStockCategoryReadRepository stockCategoryReadRepository)
        {
            _mapper = mapper;
            _stockCategoryWriteRepository = stockCategoryWriteRepository;
            _stockCategoryBusinessRules = stockCategoryBusinessRules;
            _stockCategoryReadRepository = stockCategoryReadRepository;
        }

        public async Task<CreatedStockCategoryResponse> Handle(CreateStockCategoryCommand request, CancellationToken cancellationToken)
        {
            await _stockCategoryBusinessRules.StockNameShouldBeUnique(request.Name);
            
            if(request.Code != "")
            {
                await _stockCategoryBusinessRules.StockCodeShouldBeUniqe(request.Code);
            }

            X.StockCategory stockCategory = _mapper.Map<X.StockCategory>(request);


            await _stockCategoryWriteRepository.AddAsync(stockCategory);
            await _stockCategoryWriteRepository.SaveAsync();

            X.StockCategory savedStockCategory = await _stockCategoryReadRepository.GetAsync(predicate: x => x.Gid == stockCategory.Gid);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidStockCategoryResponse obj = _mapper.Map<GetByGidStockCategoryResponse>(savedStockCategory);
            return new()
            {
                Title = StockCategoriesBusinessMessages.ProcessCompleted,
                Message = StockCategoriesBusinessMessages.SuccessCreatedStockCategoryMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
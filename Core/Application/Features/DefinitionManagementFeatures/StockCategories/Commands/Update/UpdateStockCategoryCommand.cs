using Application.Features.DefinitionManagementFeatures.StockCategories.Constants;
using Application.Features.DefinitionManagementFeatures.StockCategories.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.StockCategories.Rules;
using Application.Repositories.DefinitionManagementRepos.StockCategoryRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.StockCategories.Commands.Update;

public class UpdateStockCategoryCommand : IRequest<UpdatedStockCategoryResponse>
{
    public Guid Gid { get; set; }


    public string Name { get; set; }
    public string? Code { get; set; }



    public class UpdateStockCategoryCommandHandler : IRequestHandler<UpdateStockCategoryCommand, UpdatedStockCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStockCategoryWriteRepository _stockCategoryWriteRepository;
        private readonly IStockCategoryReadRepository _stockCategoryReadRepository;
        private readonly StockCategoryBusinessRules _stockCategoryBusinessRules;

        public UpdateStockCategoryCommandHandler(IMapper mapper, IStockCategoryWriteRepository stockCategoryWriteRepository,
                                         StockCategoryBusinessRules stockCategoryBusinessRules, IStockCategoryReadRepository stockCategoryReadRepository)
        {
            _mapper = mapper;
            _stockCategoryWriteRepository = stockCategoryWriteRepository;
            _stockCategoryBusinessRules = stockCategoryBusinessRules;
            _stockCategoryReadRepository = stockCategoryReadRepository;
        }

        public async Task<UpdatedStockCategoryResponse> Handle(UpdateStockCategoryCommand request, CancellationToken cancellationToken)
        {
            X.StockCategory? stockCategory = await _stockCategoryReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _stockCategoryBusinessRules.StockCategoryShouldExistWhenSelected(stockCategory);
            await _stockCategoryBusinessRules.StockNameShouldBeUnique(request.Name, request.Gid);
            await _stockCategoryBusinessRules.StockCodeShouldBeUniqe(request.Code, request.Gid);
            stockCategory = _mapper.Map(request, stockCategory);

            _stockCategoryWriteRepository.Update(stockCategory!);
            await _stockCategoryWriteRepository.SaveAsync();
            GetByGidStockCategoryResponse obj = _mapper.Map<GetByGidStockCategoryResponse>(stockCategory);

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
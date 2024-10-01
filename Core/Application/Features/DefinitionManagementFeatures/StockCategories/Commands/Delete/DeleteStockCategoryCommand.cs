using Application.Features.DefinitionManagementFeatures.StockCategories.Constants;
using Application.Features.DefinitionManagementFeatures.StockCategories.Rules;
using Application.Repositories.DefinitionManagementRepos.StockCategoryRepo;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;

namespace Application.Features.DefinitionManagementFeatures.StockCategories.Commands.Delete;

public class DeleteStockCategoryCommand : IRequest<DeletedStockCategoryResponse>
{
	public Guid Gid { get; set; }

    public class DeleteStockCategoryCommandHandler : IRequestHandler<DeleteStockCategoryCommand, DeletedStockCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStockCategoryReadRepository _stockCategoryReadRepository;
        private readonly IStockCategoryWriteRepository _stockCategoryWriteRepository;
        private readonly StockCategoryBusinessRules _stockCategoryBusinessRules;

        public DeleteStockCategoryCommandHandler(IMapper mapper, IStockCategoryReadRepository stockCategoryReadRepository,
                                         StockCategoryBusinessRules stockCategoryBusinessRules, IStockCategoryWriteRepository stockCategoryWriteRepository)
        {
            _mapper = mapper;
            _stockCategoryReadRepository = stockCategoryReadRepository;
            _stockCategoryBusinessRules = stockCategoryBusinessRules;
            _stockCategoryWriteRepository = stockCategoryWriteRepository;
        }

        public async Task<DeletedStockCategoryResponse> Handle(DeleteStockCategoryCommand request, CancellationToken cancellationToken)
        {
            X.StockCategory? stockCategory = await _stockCategoryReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _stockCategoryBusinessRules.StockCategoryShouldExistWhenSelected(stockCategory);
            stockCategory.DataState = Core.Enum.DataState.Deleted;

            _stockCategoryWriteRepository.Update(stockCategory);
            await _stockCategoryWriteRepository.SaveAsync();

            return new()
            {
                Title = StockCategoriesBusinessMessages.ProcessCompleted,
                Message = StockCategoriesBusinessMessages.SuccessDeletedStockCategoryMessage,
                IsValid = true
            };
        }
    }
}
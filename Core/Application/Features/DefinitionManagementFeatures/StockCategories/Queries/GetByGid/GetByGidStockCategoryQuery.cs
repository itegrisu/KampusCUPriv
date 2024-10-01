using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.DefinitionManagementRepos.StockCategoryRepo;
using Application.Features.DefinitionManagementFeatures.StockCategories.Rules;

namespace Application.Features.DefinitionManagementFeatures.StockCategories.Queries.GetByGid
{
    public class GetByGidStockCategoryQuery : IRequest<GetByGidStockCategoryResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidStockCategoryQueryHandler : IRequestHandler<GetByGidStockCategoryQuery, GetByGidStockCategoryResponse>
        {
            private readonly IMapper _mapper;
            private readonly IStockCategoryReadRepository _stockCategoryReadRepository;
            private readonly StockCategoryBusinessRules _stockCategoryBusinessRules;

            public GetByGidStockCategoryQueryHandler(IMapper mapper, IStockCategoryReadRepository stockCategoryReadRepository, StockCategoryBusinessRules stockCategoryBusinessRules)
            {
                _mapper = mapper;
                _stockCategoryReadRepository = stockCategoryReadRepository;
                _stockCategoryBusinessRules = stockCategoryBusinessRules;
            }

            public async Task<GetByGidStockCategoryResponse> Handle(GetByGidStockCategoryQuery request, CancellationToken cancellationToken)
            {
                X.StockCategory? stockCategory = await _stockCategoryReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _stockCategoryBusinessRules.StockCategoryShouldExistWhenSelected(stockCategory);

                GetByGidStockCategoryResponse response = _mapper.Map<GetByGidStockCategoryResponse>(stockCategory);
                return response;
            }
        }
    }
}
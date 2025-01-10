using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.DefinitionFeatures.Categories.Rules;
using Application.Repositories.DefinitionManagementRepo.CategoryRepo;

namespace Application.Features.DefinitionFeatures.Categories.Queries.GetByGid
{
    public class GetByGidCategoryQuery : IRequest<GetByGidCategoryResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidCategoryQueryHandler : IRequestHandler<GetByGidCategoryQuery, GetByGidCategoryResponse>
        {
            private readonly IMapper _mapper;
            private readonly ICategoryReadRepository _categoryReadRepository;
            private readonly CategoryBusinessRules _categoryBusinessRules;

            public GetByGidCategoryQueryHandler(IMapper mapper, ICategoryReadRepository categoryReadRepository, CategoryBusinessRules categoryBusinessRules)
            {
                _mapper = mapper;
                _categoryReadRepository = categoryReadRepository;
                _categoryBusinessRules = categoryBusinessRules;
            }

            public async Task<GetByGidCategoryResponse> Handle(GetByGidCategoryQuery request, CancellationToken cancellationToken)
            {
                X.Category? category = await _categoryReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _categoryBusinessRules.CategoryShouldExistWhenSelected(category);

                GetByGidCategoryResponse response = _mapper.Map<GetByGidCategoryResponse>(category);
                return response;
            }
        }
    }
}
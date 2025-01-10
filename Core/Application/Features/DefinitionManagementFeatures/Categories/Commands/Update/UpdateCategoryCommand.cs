using Application.Features.DefinitionFeatures.Categories.Constants;
using Application.Features.DefinitionFeatures.Categories.Queries.GetByGid;
using Application.Features.DefinitionFeatures.Categories.Rules;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using Application.Repositories.DefinitionManagementRepo.CategoryRepo;

namespace Application.Features.DefinitionFeatures.Categories.Commands.Update;

public class UpdateCategoryCommand : IRequest<UpdatedCategoryResponse>
{
    public Guid Gid { get; set; }

	
public string Name { get; set; }



    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdatedCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryWriteRepository _categoryWriteRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly CategoryBusinessRules _categoryBusinessRules;

        public UpdateCategoryCommandHandler(IMapper mapper, ICategoryWriteRepository categoryWriteRepository,
                                         CategoryBusinessRules categoryBusinessRules, ICategoryReadRepository categoryReadRepository)
        {
            _mapper = mapper;
            _categoryWriteRepository = categoryWriteRepository;
            _categoryBusinessRules = categoryBusinessRules;
            _categoryReadRepository = categoryReadRepository;
        }

        public async Task<UpdatedCategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            X.Category? category = await _categoryReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _categoryBusinessRules.CategoryShouldExistWhenSelected(category);
            category = _mapper.Map(request, category);

            _categoryWriteRepository.Update(category!);
            await _categoryWriteRepository.SaveAsync();
            GetByGidCategoryResponse obj = _mapper.Map<GetByGidCategoryResponse>(category);

            return new()
            {
                Title = CategoriesBusinessMessages.ProcessCompleted,
                Message = CategoriesBusinessMessages.SuccessCreatedCategoryMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
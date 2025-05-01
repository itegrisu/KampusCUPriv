using Application.Features.DefinitionFeatures.Categories.Constants;
using Application.Features.DefinitionFeatures.Categories.Rules;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using Application.Repositories.DefinitionManagementRepo.CategoryRepo;

namespace Application.Features.DefinitionFeatures.Categories.Commands.Delete;

public class DeleteCategoryCommand : IRequest<DeletedCategoryResponse>
{
	public Guid Gid { get; set; }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeletedCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly ICategoryWriteRepository _categoryWriteRepository;
        private readonly CategoryBusinessRules _categoryBusinessRules;

        public DeleteCategoryCommandHandler(IMapper mapper, ICategoryReadRepository categoryReadRepository,
                                         CategoryBusinessRules categoryBusinessRules, ICategoryWriteRepository categoryWriteRepository)
        {
            _mapper = mapper;
            _categoryReadRepository = categoryReadRepository;
            _categoryBusinessRules = categoryBusinessRules;
            _categoryWriteRepository = categoryWriteRepository;
        }

        public async Task<DeletedCategoryResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            X.Category? category = await _categoryReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _categoryBusinessRules.CategoryShouldExistWhenSelected(category);
            category.DataState = Core.Enum.DataState.Deleted;

            _categoryWriteRepository.Update(category);
            await _categoryWriteRepository.SaveAsync();

            return new()
            {
                Title = CategoriesBusinessMessages.ProcessCompleted,
                Message = CategoriesBusinessMessages.SuccessDeletedCategoryMessage,
                IsValid = true
            };
        }
    }
}
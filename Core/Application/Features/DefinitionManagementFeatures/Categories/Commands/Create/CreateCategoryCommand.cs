using Application.Features.DefinitionFeatures.Categories.Constants;
using Application.Features.DefinitionFeatures.Categories.Queries.GetByGid;
using Application.Features.DefinitionFeatures.Categories.Rules;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.DefinitionManagementRepo.CategoryRepo;

namespace Application.Features.DefinitionFeatures.Categories.Commands.Create;

public class CreateCategoryCommand : IRequest<CreatedCategoryResponse>
{
    
public string Name { get; set; }



    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreatedCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryWriteRepository _categoryWriteRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly CategoryBusinessRules _categoryBusinessRules;

        public CreateCategoryCommandHandler(IMapper mapper, ICategoryWriteRepository categoryWriteRepository,
                                         CategoryBusinessRules categoryBusinessRules, ICategoryReadRepository categoryReadRepository)
        {
            _mapper = mapper;
            _categoryWriteRepository = categoryWriteRepository;
            _categoryBusinessRules = categoryBusinessRules;
            _categoryReadRepository = categoryReadRepository;
        }

        public async Task<CreatedCategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _categoryReadRepository.GetAll().MaxAsync(r => r.RowNo);
			X.Category category = _mapper.Map<X.Category>(request);
            //category.RowNo = maxRowNo + 1;

            await _categoryWriteRepository.AddAsync(category);
            await _categoryWriteRepository.SaveAsync();

			X.Category savedCategory = await _categoryReadRepository.GetAsync(predicate: x => x.Gid == category.Gid);
			//INCLUDES Buraya Gelecek include varsa eklenecek
			//include: x => x.Include(x => x.UserFK));

            GetByGidCategoryResponse obj = _mapper.Map<GetByGidCategoryResponse>(savedCategory);
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
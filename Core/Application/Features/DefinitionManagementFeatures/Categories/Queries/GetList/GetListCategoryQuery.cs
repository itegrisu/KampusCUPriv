using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.DefinitionManagementRepo.CategoryRepo;

namespace Application.Features.DefinitionFeatures.Categories.Queries.GetList;

public class GetListCategoryQuery : IRequest<GetListResponse<GetListCategoryListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListCategoryQueryHandler : IRequestHandler<GetListCategoryQuery, GetListResponse<GetListCategoryListItemDto>>
    {
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.Category, GetListCategoryListItemDto> _noPagination;

        public GetListCategoryQueryHandler(ICategoryReadRepository categoryReadRepository, IMapper mapper, NoPagination<X.Category, GetListCategoryListItemDto> noPagination)
        {
            _categoryReadRepository = categoryReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListCategoryListItemDto>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
				//includes varsa eklenecek - Orn: Altta
				//return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<Category, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.CategoryMembers
                //    });
				return await _noPagination.NoPaginationData(cancellationToken, orderBy: x => x.Name);
            IPaginate<X.Category> categorys = await _categoryReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                orderBy: x => x.OrderBy(o => o.Name)
            );

            GetListResponse<GetListCategoryListItemDto> response = _mapper.Map<GetListResponse<GetListCategoryListItemDto>>(categorys);
            return response;
        }
    }
}
using Application.Helpers.PaginationHelpers;
using Application.Repositories.DefinitionManagementRepos.TyreTypeRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.DefinitionManagementFeatures.TyreTypes.Queries.GetList;

public class GetListTyreTypeQuery : IRequest<GetListResponse<GetListTyreTypeListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListTyreTypeQueryHandler : IRequestHandler<GetListTyreTypeQuery, GetListResponse<GetListTyreTypeListItemDto>>
    {
        private readonly ITyreTypeReadRepository _tyreTypeReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.TyreType, GetListTyreTypeListItemDto> _noPagination;

        public GetListTyreTypeQueryHandler(ITyreTypeReadRepository tyreTypeReadRepository, IMapper mapper, NoPagination<X.TyreType, GetListTyreTypeListItemDto> noPagination)
        {
            _tyreTypeReadRepository = tyreTypeReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListTyreTypeListItemDto>> Handle(GetListTyreTypeQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
				//includes varsa eklenecek - Orn: Altta
				//return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<TyreType, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.TyreTypeMembers
                //    });
				return await _noPagination.NoPaginationData(cancellationToken, orderBy: x => x.TyreTypeName);
            IPaginate<X.TyreType> tyreTypes = await _tyreTypeReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                orderBy: x => x.OrderBy(x => x.TyreTypeName)
            );

            GetListResponse<GetListTyreTypeListItemDto> response = _mapper.Map<GetListResponse<GetListTyreTypeListItemDto>>(tyreTypes);
            return response;
        }
    }
}
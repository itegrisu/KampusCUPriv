using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.DefinitionManagementRepo.ClassRepo;

namespace Application.Features.DefinitionFeatures.Classes.Queries.GetList;

public class GetListClassQuery : IRequest<GetListResponse<GetListClassListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListClassQueryHandler : IRequestHandler<GetListClassQuery, GetListResponse<GetListClassListItemDto>>
    {
        private readonly IClassReadRepository _classReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.Class, GetListClassListItemDto> _noPagination;

        public GetListClassQueryHandler(IClassReadRepository classReadRepository, IMapper mapper, NoPagination<X.Class, GetListClassListItemDto> noPagination)
        {
            _classReadRepository = classReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListClassListItemDto>> Handle(GetListClassQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
				//includes varsa eklenecek - Orn: Altta
				//return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<Class, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.ClassMembers
                //    });
				return await _noPagination.NoPaginationData(cancellationToken, orderBy: x => x.Name);
            IPaginate<X.Class> classs = await _classReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                orderBy: x => x.OrderBy(o => o.Name)
            );

            GetListResponse<GetListClassListItemDto> response = _mapper.Map<GetListResponse<GetListClassListItemDto>>(classs);
            return response;
        }
    }
}
using Application.Helpers.PaginationHelpers;
using Application.Repositories.DefinitionManagementRepos.PermitTypeRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.PermitTypes.Queries.GetList;

public class GetListPermitTypeQuery : IRequest<GetListResponse<GetListPermitTypeListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListPermitTypeQueryHandler : IRequestHandler<GetListPermitTypeQuery, GetListResponse<GetListPermitTypeListItemDto>>
    {
        private readonly IPermitTypeReadRepository _permitTypeReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.PermitType, GetListPermitTypeListItemDto> _noPagination;

        public GetListPermitTypeQueryHandler(IPermitTypeReadRepository permitTypeReadRepository, IMapper mapper, NoPagination<X.PermitType, GetListPermitTypeListItemDto> noPagination)
        {
            _permitTypeReadRepository = permitTypeReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListPermitTypeListItemDto>> Handle(GetListPermitTypeQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken);

            IPaginate<X.PermitType> permitTypes = await _permitTypeReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPermitTypeListItemDto> response = _mapper.Map<GetListResponse<GetListPermitTypeListItemDto>>(permitTypes);
            return response;
        }
    }
}
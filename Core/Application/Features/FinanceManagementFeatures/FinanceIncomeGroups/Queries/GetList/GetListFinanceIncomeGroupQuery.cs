using Application.Helpers.PaginationHelpers;
using Application.Repositories.FinanceManagementRepos.FinanceIncomeGroupRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Queries.GetList;

public class GetListFinanceIncomeGroupQuery : IRequest<GetListResponse<GetListFinanceIncomeGroupListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListFinanceIncomeGroupQueryHandler : IRequestHandler<GetListFinanceIncomeGroupQuery, GetListResponse<GetListFinanceIncomeGroupListItemDto>>
    {
        private readonly IFinanceIncomeGroupReadRepository _financeIncomeGroupReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.FinanceIncomeGroup, GetListFinanceIncomeGroupListItemDto> _noPagination;

        public GetListFinanceIncomeGroupQueryHandler(IFinanceIncomeGroupReadRepository financeIncomeGroupReadRepository, IMapper mapper, NoPagination<X.FinanceIncomeGroup, GetListFinanceIncomeGroupListItemDto> noPagination)
        {
            _financeIncomeGroupReadRepository = financeIncomeGroupReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListFinanceIncomeGroupListItemDto>> Handle(GetListFinanceIncomeGroupQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken);


            IPaginate<X.FinanceIncomeGroup> financeIncomeGroups = await _financeIncomeGroupReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListFinanceIncomeGroupListItemDto> response = _mapper.Map<GetListResponse<GetListFinanceIncomeGroupListItemDto>>(financeIncomeGroups);
            return response;
        }
    }
}
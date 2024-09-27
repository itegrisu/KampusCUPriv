using Application.Helpers.PaginationHelpers;
using Application.Repositories.SupplierManagementRepos.SCWorkHistoryRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.SupplierCustomerManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Queries.GetList;

public class GetListSCWorkHistoryQuery : IRequest<GetListResponse<GetListSCWorkHistoryListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListSCWorkHistoryQueryHandler : IRequestHandler<GetListSCWorkHistoryQuery, GetListResponse<GetListSCWorkHistoryListItemDto>>
    {
        private readonly ISCWorkHistoryReadRepository _sCWorkHistoryReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.SCWorkHistory, GetListSCWorkHistoryListItemDto> _noPagination;

        public GetListSCWorkHistoryQueryHandler(ISCWorkHistoryReadRepository sCWorkHistoryReadRepository, IMapper mapper, NoPagination<X.SCWorkHistory, GetListSCWorkHistoryListItemDto> noPagination)
        {
            _sCWorkHistoryReadRepository = sCWorkHistoryReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListSCWorkHistoryListItemDto>> Handle(GetListSCWorkHistoryQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<SCWorkHistory, object>>[]
                    {
                       x=>x.SCCompanyFK
                    });

            }

            IPaginate<X.SCWorkHistory> sCWorkHistorys = await _sCWorkHistoryReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.SCCompanyFK)
            );

            GetListResponse<GetListSCWorkHistoryListItemDto> response = _mapper.Map<GetListResponse<GetListSCWorkHistoryListItemDto>>(sCWorkHistorys);
            return response;
        }
    }
}
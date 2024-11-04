using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.SupplierManagementRepos.SCWorkHistoryRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.SupplierCustomerManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierManagementFeatures.SCWorkHistories.Queries.GetByCompanyGid
{
    public class GetByCompanyGidListSCWorkHistoryQuery : IRequest<GetListResponse<GetByCompanyGidListSCWorkHistoryListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid Gid { get; set; }
        public class GetByCompanyGidListSCWorkHistoryQueryHandler : IRequestHandler<GetByCompanyGidListSCWorkHistoryQuery, GetListResponse<GetByCompanyGidListSCWorkHistoryListItemDto>>
        {
            private readonly ISCWorkHistoryReadRepository _sCWorkHistoryReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.SCWorkHistory, GetByCompanyGidListSCWorkHistoryListItemDto> _noPagination;

            public GetByCompanyGidListSCWorkHistoryQueryHandler(ISCWorkHistoryReadRepository sCWorkHistoryReadRepository, IMapper mapper, NoPagination<X.SCWorkHistory, GetByCompanyGidListSCWorkHistoryListItemDto> noPagination)
            {
                _sCWorkHistoryReadRepository = sCWorkHistoryReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByCompanyGidListSCWorkHistoryListItemDto>> Handle(GetByCompanyGidListSCWorkHistoryQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidSCCompanyFK == request.Gid,
                        includes: new Expression<Func<SCWorkHistory, object>>[]
                        {
                            x=>x.SCCompanyFK
                        });

                }

                IPaginate<X.SCWorkHistory> sCWorkHistorys = await _sCWorkHistoryReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidSCCompanyFK == request.Gid,
                    include: x => x.Include(x => x.SCCompanyFK)
                );

                GetListResponse<GetByCompanyGidListSCWorkHistoryListItemDto> response = _mapper.Map<GetListResponse<GetByCompanyGidListSCWorkHistoryListItemDto>>(sCWorkHistorys);
                return response;
            }
        }
    }
}

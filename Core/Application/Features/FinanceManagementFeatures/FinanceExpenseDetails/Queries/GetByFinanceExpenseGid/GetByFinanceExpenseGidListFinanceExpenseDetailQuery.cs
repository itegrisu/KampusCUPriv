using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseDetailRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.FinanceManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Queries.GetByFinanceExpenseGid
{
    public class GetByFinanceExpenseGidListFinanceExpenseDetailQuery : IRequest<GetListResponse<GetByFinanceExpenseGidListFinanceExpenseDetailListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid Gid { get; set; }
        public class GetByFinanceExpenseGidListFinanceExpenseDetailQueryHandler : IRequestHandler<GetByFinanceExpenseGidListFinanceExpenseDetailQuery, GetListResponse<GetByFinanceExpenseGidListFinanceExpenseDetailListItemDto>>
        {
            private readonly IFinanceExpenseDetailReadRepository _financeExpenseDetailReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.FinanceExpenseDetail, GetByFinanceExpenseGidListFinanceExpenseDetailListItemDto> _noPagination;

            public GetByFinanceExpenseGidListFinanceExpenseDetailQueryHandler(IFinanceExpenseDetailReadRepository financeExpenseDetailReadRepository, IMapper mapper, NoPagination<X.FinanceExpenseDetail, GetByFinanceExpenseGidListFinanceExpenseDetailListItemDto> noPagination)
            {
                _financeExpenseDetailReadRepository = financeExpenseDetailReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByFinanceExpenseGidListFinanceExpenseDetailListItemDto>> Handle(GetByFinanceExpenseGidListFinanceExpenseDetailQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidExpenseFK == request.Gid,
                        includes: new Expression<Func<FinanceExpenseDetail, object>>[]
                        {
                           X => X.ControlPersonnelFK,
                           X => X.CurrencyFK,
                           X => X.FinanceExpenseFK,
                           X => X.SpendPersonnelFK
                        });
                }
                IPaginate<X.FinanceExpenseDetail> financeExpenseDetails = await _financeExpenseDetailReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidExpenseFK == request.Gid,
                    include: x => x.Include(x => x.ControlPersonnelFK).Include(x => x.CurrencyFK).Include(x => x.FinanceExpenseFK).Include(x => x.SpendPersonnelFK)
                );

                GetListResponse<GetByFinanceExpenseGidListFinanceExpenseDetailListItemDto> response = _mapper.Map<GetListResponse<GetByFinanceExpenseGidListFinanceExpenseDetailListItemDto>>(financeExpenseDetails);
                return response;
            }
        }
    }
}

using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.SupplierManagementRepos.SCBankRepo;
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


namespace Application.Features.SupplierManagementFeatures.SCBanks.Queries.GetByCompanyGid
{
    public class GetByCompanyGidListSCBankQuery : IRequest<GetListResponse<GetByCompanyGidListSCBankListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid Gid { get; set; }
        public class GetByCompanyGidListSCBankQueryHandler : IRequestHandler<GetByCompanyGidListSCBankQuery, GetListResponse<GetByCompanyGidListSCBankListItemDto>>
        {
            private readonly ISCBankReadRepository _sCBankReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.SCBank, GetByCompanyGidListSCBankListItemDto> _noPagination;

            public GetByCompanyGidListSCBankQueryHandler(ISCBankReadRepository sCBankReadRepository, IMapper mapper, NoPagination<X.SCBank, GetByCompanyGidListSCBankListItemDto> noPagination)
            {
                _sCBankReadRepository = sCBankReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByCompanyGidListSCBankListItemDto>> Handle(GetByCompanyGidListSCBankQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidSCCompanyFK == request.Gid,
                        includes: new Expression<Func<SCBank, object>>[]
                        {
                           x=>x.SCCompanyFK,
                           x=>x.CurrencyFK
                        });
                }

                IPaginate<X.SCBank> sCBanks = await _sCBankReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidSCCompanyFK == request.Gid,
                    include: x => x.Include(x => x.SCCompanyFK).Include(x => x.CurrencyFK)
                );

                GetListResponse<GetByCompanyGidListSCBankListItemDto> response = _mapper.Map<GetListResponse<GetByCompanyGidListSCBankListItemDto>>(sCBanks);
                return response;
            }
        }
    }
}

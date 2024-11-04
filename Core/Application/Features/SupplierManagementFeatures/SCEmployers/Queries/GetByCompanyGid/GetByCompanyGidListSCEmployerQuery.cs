using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.SupplierManagementRepos.SCEmployerRepo;
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

namespace Application.Features.SupplierManagementFeatures.SCEmployers.Queries.GetByCompanyGid
{
    public class GetByCompanyGidListSCEmployerQuery : IRequest<GetListResponse<GetByCompanyGidListSCEmployerListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid Gid { get; set; }
        public class GetByCompanyGidListSCEmployerQueryHandler : IRequestHandler<GetByCompanyGidListSCEmployerQuery, GetListResponse<GetByCompanyGidListSCEmployerListItemDto>>
        {
            private readonly ISCEmployerReadRepository _sCEmployerReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.SCEmployer, GetByCompanyGidListSCEmployerListItemDto> _noPagination;

            public GetByCompanyGidListSCEmployerQueryHandler(ISCEmployerReadRepository sCEmployerReadRepository, IMapper mapper, NoPagination<X.SCEmployer, GetByCompanyGidListSCEmployerListItemDto> noPagination)
            {
                _sCEmployerReadRepository = sCEmployerReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByCompanyGidListSCEmployerListItemDto>> Handle(GetByCompanyGidListSCEmployerQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidSCCompanyFK == request.Gid,
                        includes: new Expression<Func<SCEmployer, object>>[]
                        {
                           x=> x.SCCompanyFK
                        });
                }
                IPaginate<X.SCEmployer> sCEmployers = await _sCEmployerReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidSCCompanyFK == request.Gid,
                    include: x => x.Include(x => x.SCCompanyFK)
                );

                GetListResponse<GetByCompanyGidListSCEmployerListItemDto> response = _mapper.Map<GetListResponse<GetByCompanyGidListSCEmployerListItemDto>>(sCEmployers);
                return response;
            }
        }
    }
}

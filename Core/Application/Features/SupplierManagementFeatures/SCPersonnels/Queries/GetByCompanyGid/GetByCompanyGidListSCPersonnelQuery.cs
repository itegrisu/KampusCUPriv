using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.SupplierManagementRepos.SCPersonnelRepo;
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

namespace Application.Features.SupplierManagementFeatures.SCPersonnels.Queries.GetByCompanyGid
{
    public class GetByCompanyGidListSCPersonnelQuery : IRequest<GetListResponse<GetByCompanyGidListSCPersonnelListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid Gid { get; set; }
        public class GetByCompanyGidListSCPersonnelQueryHandler : IRequestHandler<GetByCompanyGidListSCPersonnelQuery, GetListResponse<GetByCompanyGidListSCPersonnelListItemDto>>
        {
            private readonly ISCPersonnelReadRepository _sCPersonnelReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.SCPersonnel, GetByCompanyGidListSCPersonnelListItemDto> _noPagination;

            public GetByCompanyGidListSCPersonnelQueryHandler(ISCPersonnelReadRepository sCPersonnelReadRepository, IMapper mapper, NoPagination<X.SCPersonnel, GetByCompanyGidListSCPersonnelListItemDto> noPagination)
            {
                _sCPersonnelReadRepository = sCPersonnelReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByCompanyGidListSCPersonnelListItemDto>> Handle(GetByCompanyGidListSCPersonnelQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidSCCompanyFK == request.Gid,
                        includes: new Expression<Func<SCPersonnel, object>>[]
                        {
                       x => x.UserFK,
                       x => x.SCCompanyFK
                        });
                // return await _noPagination.NoPaginationData(cancellationToken);
                IPaginate<X.SCPersonnel> sCPersonnels = await _sCPersonnelReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    include: x => x.Include(x => x.UserFK).Include(x => x.SCCompanyFK),
                    predicate: x => x.GidSCCompanyFK == request.Gid,
                    cancellationToken: cancellationToken
                );

                GetListResponse<GetByCompanyGidListSCPersonnelListItemDto> response = _mapper.Map<GetListResponse<GetByCompanyGidListSCPersonnelListItemDto>>(sCPersonnels);
                return response;
            }
        }
    }
}

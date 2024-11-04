using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.SupplierManagementRepos.SCAddressRepo;
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

namespace Application.Features.SupplierManagementFeatures.SCAddresses.Queries.GetByCompanyGid
{
    public class GetByCompanyGidListSCAddressQuery : IRequest<GetListResponse<GetByCompanyGidListSCAddressListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid Gid { get; set; }
        public class GetByCompanyGidListSCAddressQueryHandler : IRequestHandler<GetByCompanyGidListSCAddressQuery, GetListResponse<GetByCompanyGidListSCAddressListItemDto>>
        {
            private readonly ISCAddressReadRepository _sCAddressReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.SCAddress, GetByCompanyGidListSCAddressListItemDto> _noPagination;

            public GetByCompanyGidListSCAddressQueryHandler(ISCAddressReadRepository sCAddressReadRepository, IMapper mapper, NoPagination<X.SCAddress, GetByCompanyGidListSCAddressListItemDto> noPagination)
            {
                _sCAddressReadRepository = sCAddressReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByCompanyGidListSCAddressListItemDto>> Handle(GetByCompanyGidListSCAddressQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                       predicate: x => x.GidSCCompanyFK == request.Gid,
                       includes: new Expression<Func<SCAddress, object>>[]
                       {
                          x=> x.SCCompanyFK,
                          x=> x.CityFK
                       });
                }

                IPaginate<X.SCAddress> sCAddresss = await _sCAddressReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidSCCompanyFK == request.Gid,
                    include: x => x.Include(x => x.SCCompanyFK).Include(x => x.CityFK)

                );

                GetListResponse<GetByCompanyGidListSCAddressListItemDto> response = _mapper.Map<GetListResponse<GetByCompanyGidListSCAddressListItemDto>>(sCAddresss);
                return response;
            }
        }
    }
}

using Application.Helpers.PaginationHelpers;
using Application.Repositories.SupplierManagementRepos.SCAddressRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.SupplierCustomerManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Queries.GetList;

public class GetListSCAddressQuery : IRequest<GetListResponse<GetListSCAddressListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListSCAddressQueryHandler : IRequestHandler<GetListSCAddressQuery, GetListResponse<GetListSCAddressListItemDto>>
    {
        private readonly ISCAddressReadRepository _sCAddressReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.SCAddress, GetListSCAddressListItemDto> _noPagination;

        public GetListSCAddressQueryHandler(ISCAddressReadRepository sCAddressReadRepository, IMapper mapper, NoPagination<X.SCAddress, GetListSCAddressListItemDto> noPagination)
        {
            _sCAddressReadRepository = sCAddressReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListSCAddressListItemDto>> Handle(GetListSCAddressQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                   includes: new Expression<Func<SCAddress, object>>[]
                   {
                      x=> x.SCCompanyFK,
                      x=> x.CityFK
                   });
            }

            IPaginate<X.SCAddress> sCAddresss = await _sCAddressReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.SCCompanyFK).Include(x => x.CityFK)

            );

            GetListResponse<GetListSCAddressListItemDto> response = _mapper.Map<GetListResponse<GetListSCAddressListItemDto>>(sCAddresss);
            return response;
        }
    }
}
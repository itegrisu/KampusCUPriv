using Application.Helpers.PaginationHelpers;
using Application.Repositories.SupplierManagementRepos.SCPersonnelRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.SupplierCustomerManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Queries.GetList;

public class GetListSCPersonnelQuery : IRequest<GetListResponse<GetListSCPersonnelListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListSCPersonnelQueryHandler : IRequestHandler<GetListSCPersonnelQuery, GetListResponse<GetListSCPersonnelListItemDto>>
    {
        private readonly ISCPersonnelReadRepository _sCPersonnelReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.SCPersonnel, GetListSCPersonnelListItemDto> _noPagination;

        public GetListSCPersonnelQueryHandler(ISCPersonnelReadRepository sCPersonnelReadRepository, IMapper mapper, NoPagination<X.SCPersonnel, GetListSCPersonnelListItemDto> noPagination)
        {
            _sCPersonnelReadRepository = sCPersonnelReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListSCPersonnelListItemDto>> Handle(GetListSCPersonnelQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)               
                return await _noPagination.NoPaginationData(cancellationToken,                   
                    includes: new Expression<Func<SCPersonnel, object>>[]
                    {
                       x => x.UserFK,
                       x => x.SCCompanyFK
                    });
            // return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.SCPersonnel> sCPersonnels = await _sCPersonnelReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                include: x => x.Include(x => x.UserFK).Include(x => x.SCCompanyFK),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListSCPersonnelListItemDto> response = _mapper.Map<GetListResponse<GetListSCPersonnelListItemDto>>(sCPersonnels);
            return response;
        }
    }
}
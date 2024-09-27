using Application.Helpers.PaginationHelpers;
using Application.Repositories.SupplierManagementRepos.SCEmployerRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.SupplierCustomerManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Queries.GetList;

public class GetListSCEmployerQuery : IRequest<GetListResponse<GetListSCEmployerListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListSCEmployerQueryHandler : IRequestHandler<GetListSCEmployerQuery, GetListResponse<GetListSCEmployerListItemDto>>
    {
        private readonly ISCEmployerReadRepository _sCEmployerReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.SCEmployer, GetListSCEmployerListItemDto> _noPagination;

        public GetListSCEmployerQueryHandler(ISCEmployerReadRepository sCEmployerReadRepository, IMapper mapper, NoPagination<X.SCEmployer, GetListSCEmployerListItemDto> noPagination)
        {
            _sCEmployerReadRepository = sCEmployerReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListSCEmployerListItemDto>> Handle(GetListSCEmployerQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<SCEmployer, object>>[]
                    {
                       x=> x.SCCompanyFK
                    });
            }
            IPaginate<X.SCEmployer> sCEmployers = await _sCEmployerReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.SCCompanyFK)
            );

            GetListResponse<GetListSCEmployerListItemDto> response = _mapper.Map<GetListResponse<GetListSCEmployerListItemDto>>(sCEmployers);
            return response;
        }
    }
}
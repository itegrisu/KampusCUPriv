using Application.Helpers.PaginationHelpers;
using Application.Repositories.SupplierManagementRepos.SCBankRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.SupplierCustomerManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCBanks.Queries.GetList;

public class GetListSCBankQuery : IRequest<GetListResponse<GetListSCBankListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListSCBankQueryHandler : IRequestHandler<GetListSCBankQuery, GetListResponse<GetListSCBankListItemDto>>
    {
        private readonly ISCBankReadRepository _sCBankReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.SCBank, GetListSCBankListItemDto> _noPagination;

        public GetListSCBankQueryHandler(ISCBankReadRepository sCBankReadRepository, IMapper mapper, NoPagination<X.SCBank, GetListSCBankListItemDto> noPagination)
        {
            _sCBankReadRepository = sCBankReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListSCBankListItemDto>> Handle(GetListSCBankQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<SCBank, object>>[]
                    {
                       x=>x.SCCompanyFK,
                       x=>x.CurrencyFK
                    });
            }

            IPaginate<X.SCBank> sCBanks = await _sCBankReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.SCCompanyFK).Include(x => x.CurrencyFK)
            );

            GetListResponse<GetListSCBankListItemDto> response = _mapper.Map<GetListResponse<GetListSCBankListItemDto>>(sCBanks);
            return response;
        }
    }
}
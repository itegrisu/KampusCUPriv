using Application.Helpers.PaginationHelpers;
using Application.Repositories.TransportationRepos.TransportationRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.TransportationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.Transportations.Queries.GetList;

public class GetListTransportationQuery : IRequest<GetListResponse<GetListTransportationListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListTransportationQueryHandler : IRequestHandler<GetListTransportationQuery, GetListResponse<GetListTransportationListItemDto>>
    {
        private readonly ITransportationReadRepository _transportationReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.Transportation, GetListTransportationListItemDto> _noPagination;

        public GetListTransportationQueryHandler(ITransportationReadRepository transportationReadRepository, IMapper mapper, NoPagination<X.Transportation, GetListTransportationListItemDto> noPagination)
        {
            _transportationReadRepository = transportationReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListTransportationListItemDto>> Handle(GetListTransportationQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<Transportation, object>>[]
                    {
                       x => x.OrganizationFK,
                       x => x.FeeCurrencyFK,
                       x => x.SCCompanyFK
                    });
            IPaginate<X.Transportation> transportations = await _transportationReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.OrganizationFK).Include(x => x.FeeCurrencyFK).Include(x => x.SCCompanyFK)
            );

            GetListResponse<GetListTransportationListItemDto> response = _mapper.Map<GetListResponse<GetListTransportationListItemDto>>(transportations);
            return response;
        }
    }
}
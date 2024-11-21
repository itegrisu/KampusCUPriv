using Application.Helpers.PaginationHelpers;
using Application.Repositories.TransportationRepos.TransportationPersonnelRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.TransportationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationPersonnels.Queries.GetList;

public class GetListTransportationPersonnelQuery : IRequest<GetListResponse<GetListTransportationPersonnelListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListTransportationPersonnelQueryHandler : IRequestHandler<GetListTransportationPersonnelQuery, GetListResponse<GetListTransportationPersonnelListItemDto>>
    {
        private readonly ITransportationPersonnelReadRepository _transportationPersonnelReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.TransportationPersonnel, GetListTransportationPersonnelListItemDto> _noPagination;

        public GetListTransportationPersonnelQueryHandler(ITransportationPersonnelReadRepository transportationPersonnelReadRepository, IMapper mapper, NoPagination<X.TransportationPersonnel, GetListTransportationPersonnelListItemDto> noPagination)
        {
            _transportationPersonnelReadRepository = transportationPersonnelReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListTransportationPersonnelListItemDto>> Handle(GetListTransportationPersonnelQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<TransportationPersonnel, object>>[]
                    {
                       x => x.TransportationServiceFK,
                       x=> x.TransportationServiceFK
                    });
            IPaginate<X.TransportationPersonnel> transportationPersonnels = await _transportationPersonnelReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.TransportationServiceFK).Include(x => x.UserFK)
            );

            GetListResponse<GetListTransportationPersonnelListItemDto> response = _mapper.Map<GetListResponse<GetListTransportationPersonnelListItemDto>>(transportationPersonnels);
            return response;
        }
    }
}
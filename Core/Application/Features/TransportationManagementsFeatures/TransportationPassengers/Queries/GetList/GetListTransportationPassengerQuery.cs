using Application.Helpers.PaginationHelpers;
using Application.Repositories.TransportationRepos.TransportationPassengerRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationPassengers.Queries.GetList;

public class GetListTransportationPassengerQuery : IRequest<GetListResponse<GetListTransportationPassengerListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListTransportationPassengerQueryHandler : IRequestHandler<GetListTransportationPassengerQuery, GetListResponse<GetListTransportationPassengerListItemDto>>
    {
        private readonly ITransportationPassengerReadRepository _transportationPassengerReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.TransportationPassenger, GetListTransportationPassengerListItemDto> _noPagination;

        public GetListTransportationPassengerQueryHandler(ITransportationPassengerReadRepository transportationPassengerReadRepository, IMapper mapper, NoPagination<X.TransportationPassenger, GetListTransportationPassengerListItemDto> noPagination)
        {
            _transportationPassengerReadRepository = transportationPassengerReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListTransportationPassengerListItemDto>> Handle(GetListTransportationPassengerQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
                //includes varsa eklenecek - Orn: Altta
                //return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<TransportationPassenger, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.TransportationPassengerMembers
                //    });
                return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.TransportationPassenger> transportationPassengers = await _transportationPassengerReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListTransportationPassengerListItemDto> response = _mapper.Map<GetListResponse<GetListTransportationPassengerListItemDto>>(transportationPassengers);
            return response;
        }
    }
}
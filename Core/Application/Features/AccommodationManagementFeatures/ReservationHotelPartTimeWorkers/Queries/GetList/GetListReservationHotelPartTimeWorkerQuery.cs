using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.ReservationHotelPartTimeWorkerRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Queries.GetList;

public class GetListReservationHotelPartTimeWorkerQuery : IRequest<GetListResponse<GetListReservationHotelPartTimeWorkerListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListReservationHotelPartTimeWorkerQueryHandler : IRequestHandler<GetListReservationHotelPartTimeWorkerQuery, GetListResponse<GetListReservationHotelPartTimeWorkerListItemDto>>
    {
        private readonly IReservationHotelPartTimeWorkerReadRepository _reservationHotelPartTimeWorkerReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.ReservationHotelPartTimeWorker, GetListReservationHotelPartTimeWorkerListItemDto> _noPagination;

        public GetListReservationHotelPartTimeWorkerQueryHandler(IReservationHotelPartTimeWorkerReadRepository reservationHotelPartTimeWorkerReadRepository, IMapper mapper, NoPagination<X.ReservationHotelPartTimeWorker, GetListReservationHotelPartTimeWorkerListItemDto> noPagination)
        {
            _reservationHotelPartTimeWorkerReadRepository = reservationHotelPartTimeWorkerReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListReservationHotelPartTimeWorkerListItemDto>> Handle(GetListReservationHotelPartTimeWorkerQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
                //includes varsa eklenecek - Orn: Altta
                //return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<ReservationHotelPartTimeWorker, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.ReservationHotelPartTimeWorkerMembers
                //    });
                return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.ReservationHotelPartTimeWorker> reservationHotelPartTimeWorkers = await _reservationHotelPartTimeWorkerReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListReservationHotelPartTimeWorkerListItemDto> response = _mapper.Map<GetListResponse<GetListReservationHotelPartTimeWorkerListItemDto>>(reservationHotelPartTimeWorkers);
            return response;
        }
    }
}
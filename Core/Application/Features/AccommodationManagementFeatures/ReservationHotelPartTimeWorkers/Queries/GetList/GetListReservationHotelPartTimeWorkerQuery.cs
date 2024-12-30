using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.ReservationHotelPartTimeWorkerRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Queries.GetList;

public class GetListReservationHotelPartTimeWorkerQuery : IRequest<GetListResponse<GetListReservationHotelPartTimeWorkerListItemDto>>
{
    public string ReservationHotelGid { get; set; }
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;

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
            Expression<Func<X.ReservationHotelPartTimeWorker, bool>> predicate = null;

            if (request.ReservationHotelGid != null)
                predicate = x => x.GidHotelFK.ToString() == request.ReservationHotelGid;

            if (request.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    predicate: predicate,
                    includes: new Expression<Func<X.ReservationHotelPartTimeWorker, object>>[]
                    {
                       x => x.PartTimeWorkerFK
                    });

            IPaginate<X.ReservationHotelPartTimeWorker> reservationHotelPartTimeWorkers = await _reservationHotelPartTimeWorkerReadRepository.GetListAsync(
                index: request.PageIndex,
                size: request.PageSize,
                predicate: predicate,
                include: x => x.Include(x => x.PartTimeWorkerFK),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListReservationHotelPartTimeWorkerListItemDto> response = _mapper.Map<GetListResponse<GetListReservationHotelPartTimeWorkerListItemDto>>(reservationHotelPartTimeWorkers);
            return response;
        }
    }
}
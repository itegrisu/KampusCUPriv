using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.AccommodationManagements.ReservationRoomRepo;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationRooms.Queries.GetList;

public class GetListReservationRoomQuery : IRequest<GetListResponse<GetListReservationRoomListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListReservationRoomQueryHandler : IRequestHandler<GetListReservationRoomQuery, GetListResponse<GetListReservationRoomListItemDto>>
    {
        private readonly IReservationRoomReadRepository _reservationRoomReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.ReservationRoom, GetListReservationRoomListItemDto> _noPagination;

        public GetListReservationRoomQueryHandler(IReservationRoomReadRepository reservationRoomReadRepository, IMapper mapper, NoPagination<X.ReservationRoom, GetListReservationRoomListItemDto> noPagination)
        {
            _reservationRoomReadRepository = reservationRoomReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListReservationRoomListItemDto>> Handle(GetListReservationRoomQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<ReservationRoom, object>>[]
                    {
                       x => x.ReservationDetailFK,
                    });
            IPaginate<X.ReservationRoom> reservationRooms = await _reservationRoomReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.ReservationDetailFK)
            );

            GetListResponse<GetListReservationRoomListItemDto> response = _mapper.Map<GetListResponse<GetListReservationRoomListItemDto>>(reservationRooms);
            return response;
        }
    }
}
using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.AccommodationDateRepo;
using Application.Repositories.AccommodationManagements.ReservationRoomRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Enum;
using Core.Persistence.Paging;
using Domain.Entities.AccommodationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationRooms.Queries.GetList;

public class GetListReservationRoomQuery : IRequest<GetListResponse<GetListReservationRoomListItemDto>>
{
    public string ReservationDetailGid { get; set; }
    public DateTime ReservationDate { get; set; }
    public string RoomTypeGid { get; set; }
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;

    public class GetListReservationRoomQueryHandler : IRequestHandler<GetListReservationRoomQuery, GetListResponse<GetListReservationRoomListItemDto>>
    {
        private readonly IReservationRoomReadRepository _reservationRoomReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.ReservationRoom, GetListReservationRoomListItemDto> _noPagination;
        private readonly IAccommodationDateReadRepository _accommodationDateReadRepository;

        public GetListReservationRoomQueryHandler(IReservationRoomReadRepository reservationRoomReadRepository, IMapper mapper, NoPagination<X.ReservationRoom, GetListReservationRoomListItemDto> noPagination, IAccommodationDateReadRepository accommodationDateReadRepository)
        {
            _reservationRoomReadRepository = reservationRoomReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
            _accommodationDateReadRepository = accommodationDateReadRepository;
        }

        public async Task<GetListResponse<GetListReservationRoomListItemDto>> Handle(GetListReservationRoomQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<ReservationRoom, bool>> predicate = null;
            if (request.ReservationDetailGid != null)
            {
                predicate = x => x.GidReservationDetailFK.ToString() == request.ReservationDetailGid
                && x.ReservationDetailFK.GidRoomTypeFK.ToString() == request.RoomTypeGid && x.ReservationDetailFK.ReservationDate == request.ReservationDate;
            }


            if (request.PageIndex == -1)
            {
                var roomList = await _noPagination.NoPaginationData(cancellationToken,
                    predicate: predicate,
                    orderBy: x => x.RoomNo,
                    includes: new Expression<Func<ReservationRoom, object>>[]
                    {
                       x => x.ReservationDetailFK,
                       x => x.ReservationDetailFK.RoomTypeFK
                    });

                foreach (var room in roomList.Items)
                {
                    var accommodationDate = await _accommodationDateReadRepository.GetWhere(x => x.DataState == DataState.Active && x.GidReservationDetailFK.ToString() == request.ReservationDetailGid
                    && x.GidRoomNoFK == room.Gid && x.Date == request.ReservationDate).Include(x => x.GuestFK).Include(x => x.GuestFK).ThenInclude(x => x.CountryFK).ToListAsync();
                    room.Guests = accommodationDate.Select(x => new RoomGuests
                    {
                        Fullname = x.GuestFK.Name + " " + x.GuestFK.Surename,
                        Gender = x.GuestFK.Gender,
                        Country = x.GuestFK.CountryFK.Name
                    }).ToList();
                }

                return roomList;
            }



            IPaginate<X.ReservationRoom> reservationRooms = await _reservationRoomReadRepository.GetListAsync(
                index: request.PageIndex,
                size: request.PageSize,
                predicate: predicate,
                orderBy: x => x.OrderBy(x => x.RoomNo),
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.ReservationDetailFK).Include(x => x.ReservationDetailFK).ThenInclude(x => x.RoomTypeFK)
            );

            GetListResponse<GetListReservationRoomListItemDto> response = _mapper.Map<GetListResponse<GetListReservationRoomListItemDto>>(reservationRooms);
            return response;
        }
    }
}
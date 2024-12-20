using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.AccommodationManagements.ReservationDetailRepo;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationDetails.Queries.GetList;

public class GetListReservationDetailQuery : IRequest<GetListResponse<GetListReservationDetailListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListReservationDetailQueryHandler : IRequestHandler<GetListReservationDetailQuery, GetListResponse<GetListReservationDetailListItemDto>>
    {
        private readonly IReservationDetailReadRepository _reservationDetailReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.ReservationDetail, GetListReservationDetailListItemDto> _noPagination;

        public GetListReservationDetailQueryHandler(IReservationDetailReadRepository reservationDetailReadRepository, IMapper mapper, NoPagination<X.ReservationDetail, GetListReservationDetailListItemDto> noPagination)
        {
            _reservationDetailReadRepository = reservationDetailReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListReservationDetailListItemDto>> Handle(GetListReservationDetailQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<ReservationDetail, object>>[]
                    {
                       x => x.ReservationHotelFK,
                       x => x.ReservationHotelFK.ReservationFK,
                       x => x.RoomTypeFK,
                    });
            IPaginate<X.ReservationDetail> reservationDetails = await _reservationDetailReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.ReservationHotelFK).Include(X => X.RoomTypeFK).Include(x => x.ReservationHotelFK).ThenInclude(x => x.ReservationFK)
            );

            GetListResponse<GetListReservationDetailListItemDto> response = _mapper.Map<GetListResponse<GetListReservationDetailListItemDto>>(reservationDetails);
            return response;
        }
    }
}
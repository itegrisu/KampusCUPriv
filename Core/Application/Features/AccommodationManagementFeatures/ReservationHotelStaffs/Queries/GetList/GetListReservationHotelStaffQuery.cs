using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.ReservationHotelStaffRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.AccommodationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Queries.GetList;

public class GetListReservationHotelStaffQuery : IRequest<GetListResponse<GetListReservationHotelStaffListItemDto>>
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public string HotelGid { get; set; }

    public class GetListReservationHotelStaffQueryHandler : IRequestHandler<GetListReservationHotelStaffQuery, GetListResponse<GetListReservationHotelStaffListItemDto>>
    {
        private readonly IReservationHotelStaffReadRepository _reservationHotelStaffReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.ReservationHotelStaff, GetListReservationHotelStaffListItemDto> _noPagination;

        public GetListReservationHotelStaffQueryHandler(IReservationHotelStaffReadRepository reservationHotelStaffReadRepository, IMapper mapper, NoPagination<X.ReservationHotelStaff, GetListReservationHotelStaffListItemDto> noPagination)
        {
            _reservationHotelStaffReadRepository = reservationHotelStaffReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListReservationHotelStaffListItemDto>> Handle(GetListReservationHotelStaffQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<ReservationHotelStaff, bool>> predicate = null;
            if (request.HotelGid != null)
                predicate = x => x.GidHotelFK.ToString() == request.HotelGid;

            if (request.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken, predicate: predicate,
                    includes: new Expression<Func<ReservationHotelStaff, object>>[]
                    {
                       x => x.SCCompanyFK,
                    });
            IPaginate<X.ReservationHotelStaff> reservationHotelStaffs = await _reservationHotelStaffReadRepository.GetListAsync(
                index: request.PageIndex,
                size: request.PageSize,
                predicate: predicate,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.SCCompanyFK)
            );

            GetListResponse<GetListReservationHotelStaffListItemDto> response = _mapper.Map<GetListResponse<GetListReservationHotelStaffListItemDto>>(reservationHotelStaffs);
            return response;
        }
    }
}
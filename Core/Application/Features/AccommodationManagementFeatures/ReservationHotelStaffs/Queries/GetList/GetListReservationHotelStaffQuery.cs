using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.AccommodationManagements.ReservationHotelStaffRepo;
using Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Queries.GetList;

public class GetListReservationHotelStaffQuery : IRequest<GetListResponse<GetListReservationHotelStaffListItemDto>>
{
    public PageRequest PageRequest { get; set; }

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
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<ReservationHotelStaff, object>>[]
                    {
                       x => x.SCCompanyFK,
                    });
            IPaginate<X.ReservationHotelStaff> reservationHotelStaffs = await _reservationHotelStaffReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.SCCompanyFK)
            );

            GetListResponse<GetListReservationHotelStaffListItemDto> response = _mapper.Map<GetListResponse<GetListReservationHotelStaffListItemDto>>(reservationHotelStaffs);
            return response;
        }
    }
}
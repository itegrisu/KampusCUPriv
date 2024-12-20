using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.AccommodationManagements.ReservationRepo;
using Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AccommodationManagementFeatures.Reservations.Queries.GetList;

public class GetListReservationQuery : IRequest<GetListResponse<GetListReservationListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListReservationQueryHandler : IRequestHandler<GetListReservationQuery, GetListResponse<GetListReservationListItemDto>>
    {
        private readonly IReservationReadRepository _reservationReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.Reservation, GetListReservationListItemDto> _noPagination;

        public GetListReservationQueryHandler(IReservationReadRepository reservationReadRepository, IMapper mapper, NoPagination<X.Reservation, GetListReservationListItemDto> noPagination)
        {
            _reservationReadRepository = reservationReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListReservationListItemDto>> Handle(GetListReservationQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<Reservation, object>>[]
                    {
                       x => x.OrganizationFK,
                    });
            IPaginate<X.Reservation> reservations = await _reservationReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.OrganizationFK)
            );

            GetListResponse<GetListReservationListItemDto> response = _mapper.Map<GetListResponse<GetListReservationListItemDto>>(reservations);
            return response;
        }
    }
}
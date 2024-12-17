using Application.Features.VehicleManagementsFeatures.VehicleTransactions.Queries.GetListWithDateRange;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.TransportationRepos.TransportationServiceRepo;
using Application.Repositories.VehicleManagementsRepos.VehicleRequestRepo;
using Application.Repositories.VehicleManagementsRepos.VehicleTransactionRepo;
using AutoMapper;
using Core.Application.Responses;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementsFeatures.VehicleTransactions.Queries.GetForCalendar
{

    public class GetForCalendarQuery : IRequest<GetListResponse<GetForCalendarListItemDto>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public class GetForCalendarQueryHandler : IRequestHandler<GetForCalendarQuery, GetListResponse<GetForCalendarListItemDto>>
        {
            private readonly IVehicleTransactionReadRepository _vehicleTransactionReadRepository;
            private readonly ITransportationServiceReadRepository _transportationServiceReadRepository;
            private readonly IVehicleRequestReadRepository _vehicleRequestReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.VehicleTransaction, GetForCalendarListItemDto> _noPagination;

            public GetForCalendarQueryHandler(IVehicleTransactionReadRepository vehicleTransactionReadRepository, IMapper mapper, NoPagination<X.VehicleTransaction, GetForCalendarListItemDto> noPagination, ITransportationServiceReadRepository transportationServiceReadRepository, IVehicleRequestReadRepository vehicleRequestReadRepository)
            {
                _vehicleTransactionReadRepository = vehicleTransactionReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
                _transportationServiceReadRepository = transportationServiceReadRepository;
                _vehicleRequestReadRepository = vehicleRequestReadRepository;
            }
            public async Task<GetListResponse<GetForCalendarListItemDto>> Handle(GetForCalendarQuery request, CancellationToken cancellationToken)
            {
                // FirmaAraci ve KiralikAlinanArac olan tüm araçları alıyoruz
                var allVehicles = (await _vehicleTransactionReadRepository.GetListAsync(
                    predicate: x => (x.VehicleStatus == EnumVehicleStatus.FirmaAraci || x.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac)
                                    && x.GidVehicleFK != null,
                    include: x => x.Include(v => v.VehicleAllFK)))
                    .Items.Select(x => new { x.GidVehicleFK, PlateNumber = x.VehicleAllFK.PlateNumber })
                    .Distinct()
                    .ToList();

                // Tarih aralığını gün gün parçalıyoruz
                var result = new List<GetForCalendarListItemDto>();

                for (var date = request.StartDate.Date; date <= request.EndDate.Date; date = date.AddDays(1))
                {
                    // O günün başlangıcı ve bitişini ayarlıyoruz
                    var dayStart = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                    var dayEnd = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

                    // TransportationService: Belirtilen tarihte dolu olan araçlar
                    var busyInTransport = (await _transportationServiceReadRepository.GetListAsync(
                        predicate: x => x.StartDate <= dayEnd && x.EndDate >= dayStart)) // Günü kapsayan seferler
                        .Items.Select(x => x.GidVehicleFK)
                        .Distinct()
                        .ToList();

                    // VehicleRequest: Onaylanmış taleplerdeki dolu araçlar
                    var busyInRequests = (await _vehicleRequestReadRepository.GetListAsync(
                        predicate: x => x.VehicleApprovedStatus == EnumVehicleApprovedStatus.Onaylandi &&
                                        x.StartDate <= dayEnd && x.EndDate >= dayStart)) // Günü kapsayan talepler
                        .Items.Select(x => x.GidVehicleFK)
                        .Distinct()
                        .ToList();

                    // Dolu araçları birleştir
                    var busyVehicles = busyInTransport.Union(busyInRequests).Distinct().ToList();

                    // Boş araçları hesapla
                    var emptyVehicles = allVehicles.Where(v => !busyVehicles.Contains(v.GidVehicleFK))
                                                   .Select(v => v.PlateNumber)
                                                   .ToList();

                    // Dolu araç plakalarını al
                    var busyVehiclePlates = allVehicles.Where(v => busyVehicles.Contains(v.GidVehicleFK))
                                                       .Select(v => v.PlateNumber)
                                                       .ToList();

                    // Günü ve araç durumlarını ekliyoruz
                    result.Add(new GetForCalendarListItemDto
                    {
                        Date = dayStart, // O günün tarihi
                        BusyVehicles = busyVehiclePlates,
                        EmptyVehicles = emptyVehicles
                    });
                }

                return new GetListResponse<GetForCalendarListItemDto>
                {
                    Items = result,
                    Count= result.Count
                };
            }
        }
    }
}

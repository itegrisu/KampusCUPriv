using Application.Features.VehicleManagementsFeatures.VehicleTransactions.Queries.GetHistoriesByVehicleGid;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.TransportationRepos.TransportationServiceRepo;
using Application.Repositories.VehicleManagementsRepos.VehicleRequestRepo;
using Application.Repositories.VehicleManagementsRepos.VehicleTransactionRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Enum;
using Core.Persistence.Paging;
using Domain.Entities.VehicleManagements;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementsFeatures.VehicleTransactions.Queries.GetListWithDateRange
{
    public class GetListWithDateRangeVehicleTransactionQuery : IRequest<GetListResponse<GetListWithDateRangeVehicleTransactionListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public class GetListWithDateRangeVehicleTransactionQueryHandler : IRequestHandler<GetListWithDateRangeVehicleTransactionQuery, GetListResponse<GetListWithDateRangeVehicleTransactionListItemDto>>
        {
            private readonly IVehicleTransactionReadRepository _vehicleTransactionReadRepository;
            private readonly ITransportationServiceReadRepository _transportationServiceReadRepository;
            private readonly IVehicleRequestReadRepository _vehicleRequestReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.VehicleTransaction, GetListWithDateRangeVehicleTransactionListItemDto> _noPagination;

            public GetListWithDateRangeVehicleTransactionQueryHandler(IVehicleTransactionReadRepository vehicleTransactionReadRepository, IMapper mapper, NoPagination<X.VehicleTransaction, GetListWithDateRangeVehicleTransactionListItemDto> noPagination, ITransportationServiceReadRepository transportationServiceReadRepository, IVehicleRequestReadRepository vehicleRequestReadRepository)
            {
                _vehicleTransactionReadRepository = vehicleTransactionReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
                _transportationServiceReadRepository = transportationServiceReadRepository;
                _vehicleRequestReadRepository = vehicleRequestReadRepository;
            }
            public async Task<GetListResponse<GetListWithDateRangeVehicleTransactionListItemDto>> Handle(GetListWithDateRangeVehicleTransactionQuery request, CancellationToken cancellationToken)
            {
                // FirmaAraci ve KiralikAlinanArac olan araçları getiriyoruz
                var allVehicles = (await _vehicleTransactionReadRepository.GetListAsync(
                    predicate: x => (x.VehicleStatus == EnumVehicleStatus.FirmaAraci || x.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac)
                                    && x.GidVehicleFK != null))
                    .Items.Select(x => x.GidVehicleFK).Distinct().ToList();

                // TransportationService'de belirtilen tarih aralığında kullanılan araçları buluyoruz
                var inTransportationServiceVehicles = (await _transportationServiceReadRepository.GetListAsync(
                    predicate: x => x.StartDate < request.EndDate && x.EndDate > request.StartDate // Çakışan sefer kontrolü
                ))
                .Items.Select(x => x.GidVehicleFK).Distinct().ToList();

                // Onaylanmış araç taleplerini kontrol ediyoruz
                var inApprovedVehicleRequests = (await _vehicleRequestReadRepository.GetListAsync(
                    predicate: x => x.VehicleApprovedStatus == EnumVehicleApprovedStatus.Onaylandi // Sadece onaylananlar
                                    && x.StartDate < request.EndDate
                                    && x.EndDate > request.StartDate // Tarihler çakışıyor mu kontrolü
                ))
                .Items.Select(x => x.GidVehicleFK).Distinct().ToList();

                // Seferde veya onaylı talepte olmayan araçları filtreliyoruz
                var unavailableVehicles = inTransportationServiceVehicles.Union(inApprovedVehicleRequests).ToList();
                var availableVehicles = allVehicles.Except(unavailableVehicles).ToList();

                // Sonuçları dönüyoruz
                var vehicleTransactions = await _vehicleTransactionReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => availableVehicles.Contains(x.GidVehicleFK), // Seferde olmayan araçları 
                    include: x => x.Include(x => x.UserFK)
                                   .Include(x => x.SCCompanyFK)
                                   .Include(x => x.VehicleAllFK)
                                   .Include(x => x.CurrencyFK)
                );

                return _mapper.Map<GetListResponse<GetListWithDateRangeVehicleTransactionListItemDto>>(vehicleTransactions);

            }

        }
    }

}

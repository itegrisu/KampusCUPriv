using Application.Abstractions;
using Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Update;
using Application.Features.TransportationManagementFeatures.TransportationServices.Constants;
using Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationServices.Rules;
using Application.Repositories.TransportationRepos.TransportationServiceRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.ReportTransportationService
{
    public class ReportTransportationServiceCommand : IRequest<ReportedTransportationServiceResponse>
    {
        public Guid Gid { get; set; }
        public Guid GidTransportationFK { get; set; }
        public Guid GidVehicleFK { get; set; }
        public string ServiceNo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? StartKM { get; set; }
        public int? EndKM { get; set; }
        public string? VehiclePhone { get; set; }
        public EnumTransportationServiceStatus TransportationServiceStatus { get; set; }
        public string? TransportationFile { get; set; }
        public string? Description { get; set; }
        public string? RefNoTransportation { get; set; }

        public class ReportTransportationServiceCommandHandler : IRequestHandler<ReportTransportationServiceCommand, ReportedTransportationServiceResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITransportationServiceWriteRepository _transportationServiceWriteRepository;
            private readonly ITransportationServiceReadRepository _transportationServiceReadRepository;
            private readonly TransportationServiceBusinessRules _transportationServiceBusinessRules;
            private readonly IUlasımService _ulasımService;
            public ReportTransportationServiceCommandHandler(IMapper mapper, ITransportationServiceWriteRepository transportationServiceWriteRepository,
                                             TransportationServiceBusinessRules transportationServiceBusinessRules, ITransportationServiceReadRepository transportationServiceReadRepository, IUlasımService ulasımService)
            {
                _mapper = mapper;
                _transportationServiceWriteRepository = transportationServiceWriteRepository;
                _transportationServiceBusinessRules = transportationServiceBusinessRules;
                _transportationServiceReadRepository = transportationServiceReadRepository;
                _ulasımService = ulasımService;
            }

            public async Task<ReportedTransportationServiceResponse> Handle(ReportTransportationServiceCommand request, CancellationToken cancellationToken)
            {
                X.TransportationService? transportationService = await _transportationServiceReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.TransportationFK).Include(x => x.VehicleAllFK));
                string sonuc = "";
                //INCLUDES Buraya Gelecek include varsa eklenecek
                await _transportationServiceBusinessRules.TransportationServiceShouldExistWhenSelected(transportationService);
                transportationService = _mapper.Map(request, transportationService);

                if (request.TransportationServiceStatus == EnumTransportationServiceStatus.Taslak && String.IsNullOrEmpty(request.RefNoTransportation))
                {
                    sonuc = await _ulasımService.SeferEkleAsync(transportationService);
                }
                else if (request.TransportationServiceStatus == EnumTransportationServiceStatus.Taslak && !String.IsNullOrEmpty(request.RefNoTransportation))
                {
                    sonuc = await _ulasımService.SeferGuncelleAsync(transportationService,long.Parse(request.RefNoTransportation));
                }

                if (!sonuc.Contains("HATA"))
                {
                    transportationService.RefNoTransportation = sonuc;
                    transportationService.TransportationServiceStatus = EnumTransportationServiceStatus.Gecerli;

                    _transportationServiceWriteRepository.Update(transportationService!);
                    await _transportationServiceWriteRepository.SaveAsync();
                    GetByGidTransportationServiceResponse obj = _mapper.Map<GetByGidTransportationServiceResponse>(transportationService);

                    return new()
                    {
                        Title = TransportationServicesBusinessMessages.ProcessCompleted,
                        Message = TransportationServicesBusinessMessages.SuccessReportedTransportatinServiceMessage,
                        IsValid = true,
                        Obj = obj
                    };
                }

                return new()
                {
                    Title = TransportationServicesBusinessMessages.TechnicalError,
                    Message = sonuc,
                    IsValid = false
                };
            }
        }
    }
}

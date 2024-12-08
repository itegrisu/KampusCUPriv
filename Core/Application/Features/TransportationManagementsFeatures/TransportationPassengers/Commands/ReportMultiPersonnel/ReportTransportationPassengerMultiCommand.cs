using Application.Features.TransportationManagementFeatures.TransportationPassengers.Commands.Update;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Constants;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Rules;
using Application.Repositories.TransportationRepos.TransportationPassengerRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;
using Application.Abstractions;
using Domain.Entities.TransportationManagements;
namespace Application.Features.TransportationManagementsFeatures.TransportationPassengers.Commands.ReportMultiPersonnel
{
    public class ReportTransportationPassengerMultiCommand : IRequest<ReportedTransportationPassengerMultiResponse>
    {
        public List<TransportationPassenger> PassengerList { get; set; }

        public class ReportTransportationPassengerCommandHandler : IRequestHandler<ReportTransportationPassengerMultiCommand, ReportedTransportationPassengerMultiResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITransportationPassengerWriteRepository _transportationPassengerWriteRepository;
            private readonly ITransportationPassengerReadRepository _transportationPassengerReadRepository;
            private readonly TransportationPassengerBusinessRules _transportationPassengerBusinessRules;
            private readonly IUlasımService _ulasımService;

            public ReportTransportationPassengerCommandHandler(
                IMapper mapper,
                ITransportationPassengerWriteRepository transportationPassengerWriteRepository,
                TransportationPassengerBusinessRules transportationPassengerBusinessRules,
                ITransportationPassengerReadRepository transportationPassengerReadRepository,
                IUlasımService ulasımService)
            {
                _mapper = mapper;
                _transportationPassengerWriteRepository = transportationPassengerWriteRepository;
                _transportationPassengerBusinessRules = transportationPassengerBusinessRules;
                _transportationPassengerReadRepository = transportationPassengerReadRepository;
                _ulasımService = ulasımService;
            }

            public async Task<ReportedTransportationPassengerMultiResponse> Handle(ReportTransportationPassengerMultiCommand request, CancellationToken cancellationToken)
            {
                var passengersToReport = new List<TransportationPassenger>();

                foreach (var passengerDto in request.PassengerList)
                {
                    // Veritabanından yolcu bilgilerini al
                    X.TransportationPassenger? transportationPassenger = await _transportationPassengerReadRepository.GetAsync(
                        predicate: x => x.Gid == passengerDto.Gid,
                        cancellationToken: cancellationToken,
                        include: x => x.Include(x => x.TransportationGroupFK).Include(x => x.TransportationGroupFK).ThenInclude(x => x.TransportationServiceFK)
                    );

                    await _transportationPassengerBusinessRules.TransportationPassengerShouldExistWhenSelected(transportationPassenger);

                    passengersToReport.Add(transportationPassenger);
                }

                // Ulaştırma servisine yolcu listesini gönder
                var seferRefNumber = passengersToReport[0].TransportationGroupFK.TransportationServiceFK.RefNoTransportation; // Sefer Referans Numarası
                var groupRefNumber = passengersToReport[0].TransportationGroupFK.RefNoTransportationGroup; // Grup Referans Numarası
                var sonuc = await _ulasımService.YolcuEkleAsync(passengersToReport,long.Parse(seferRefNumber) ,long.Parse(groupRefNumber));

                if (!sonuc.Contains("HATA"))
                {
                    return new()
                    {
                        Title = TransportationPassengersBusinessMessages.ProcessCompleted,
                        Message = TransportationPassengersBusinessMessages.SuccessReportedTransportationPassengerMultiMessage,
                        IsValid = true,
                    };
                }

                return new()
                {
                    Title = TransportationPassengersBusinessMessages.TechnicalError,
                    Message = sonuc,
                    IsValid = false,
                };
            }
        }
    }
}

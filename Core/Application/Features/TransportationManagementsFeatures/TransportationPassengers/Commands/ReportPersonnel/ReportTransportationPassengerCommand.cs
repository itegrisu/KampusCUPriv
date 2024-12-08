using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Commands.Update;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Constants;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Rules;
using Application.Repositories.TransportationRepos.TransportationPassengerRepo;
using AutoMapper;
using Domain.Entities.TransportationManagements;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;
namespace Application.Features.TransportationManagementsFeatures.TransportationPassengers.Commands.ReportPersonnel
{
    public class ReportTransportationPassengerCommand : IRequest<ReportedTransportationPassengerResponse>
    {
        public Guid Gid { get; set; }
        public Guid GidTransportationGroupFK { get; set; }
        public string Country { get; set; }
        public string IdentityNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EnumGender Gender { get; set; }
        public string? Phone { get; set; }
        public EnumPassengerStatus PassengerStatus { get; set; }
        public string? RefNoTransportationPassenger { get; set; }

        public class ReportTransportationPassengerCommandHandler : IRequestHandler<ReportTransportationPassengerCommand, ReportedTransportationPassengerResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITransportationPassengerWriteRepository _transportationPassengerWriteRepository;
            private readonly ITransportationPassengerReadRepository _transportationPassengerReadRepository;
            private readonly TransportationPassengerBusinessRules _transportationPassengerBusinessRules;
            private readonly IUlasımService _ulasımService;

            public ReportTransportationPassengerCommandHandler(IMapper mapper, ITransportationPassengerWriteRepository transportationPassengerWriteRepository,
                                             TransportationPassengerBusinessRules transportationPassengerBusinessRules, ITransportationPassengerReadRepository transportationPassengerReadRepository, IUlasımService ulasımService)
            {
                _mapper = mapper;
                _transportationPassengerWriteRepository = transportationPassengerWriteRepository;
                _transportationPassengerBusinessRules = transportationPassengerBusinessRules;
                _transportationPassengerReadRepository = transportationPassengerReadRepository;
                _ulasımService = ulasımService;
            }

            public async Task<ReportedTransportationPassengerResponse> Handle(ReportTransportationPassengerCommand request, CancellationToken cancellationToken)
            {
                var passengersToReport = new List<TransportationPassenger>();

                X.TransportationPassenger? transportationPassenger = await _transportationPassengerReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.TransportationGroupFK).Include(x => x.TransportationGroupFK).ThenInclude(x => x.TransportationServiceFK));
                //INCLUDES Buraya Gelecek include varsa eklenecek
                await _transportationPassengerBusinessRules.TransportationPassengerShouldExistWhenSelected(transportationPassenger);
                transportationPassenger = _mapper.Map(request, transportationPassenger);

                // Ulaştırma servisine yolcu listesini gönder
                var seferRefNumber = transportationPassenger.TransportationGroupFK.TransportationServiceFK.RefNoTransportation; // Sefer Referans Numarası
                var groupRefNumber = transportationPassenger.TransportationGroupFK.RefNoTransportationGroup; // Grup Referans Numarası

                passengersToReport.Add(transportationPassenger);

                var sonuc = await _ulasımService.YolcuEkleAsync(passengersToReport, long.Parse(seferRefNumber),long.Parse(groupRefNumber));

                _transportationPassengerWriteRepository.Update(transportationPassenger!);
                await _transportationPassengerWriteRepository.SaveAsync();
                GetByGidTransportationPassengerResponse obj = _mapper.Map<GetByGidTransportationPassengerResponse>(transportationPassenger);

                if (!sonuc.Contains("HATA"))
                {
                    return new()
                    {
                        Title = TransportationPassengersBusinessMessages.ProcessCompleted,
                        Message = TransportationPassengersBusinessMessages.SuccessReportedTransportationPassengerMessage,
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

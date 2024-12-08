using Application.Abstractions;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Commands.Delete;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Constants;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Rules;
using Application.Repositories.TransportationRepos.TransportationPassengerRepo;
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

namespace Application.Features.TransportationManagementsFeatures.TransportationPassengers.Commands.CancelReport
{
    public class CancelTransportationPassengerCommand : IRequest<CanceledTransportationPassengerResponse>
    {
        public Guid Gid { get; set; }

        public class CancelTransportationPassengerCommandHandler : IRequestHandler<CancelTransportationPassengerCommand, CanceledTransportationPassengerResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITransportationPassengerReadRepository _transportationPassengerReadRepository;
            private readonly ITransportationPassengerWriteRepository _transportationPassengerWriteRepository;
            private readonly TransportationPassengerBusinessRules _transportationPassengerBusinessRules;
            private readonly IUlasımService _ulasımService;
            public CancelTransportationPassengerCommandHandler(IMapper mapper, ITransportationPassengerReadRepository transportationPassengerReadRepository,
                                             TransportationPassengerBusinessRules transportationPassengerBusinessRules, ITransportationPassengerWriteRepository transportationPassengerWriteRepository, IUlasımService ulasımService)
            {
                _mapper = mapper;
                _transportationPassengerReadRepository = transportationPassengerReadRepository;
                _transportationPassengerBusinessRules = transportationPassengerBusinessRules;
                _transportationPassengerWriteRepository = transportationPassengerWriteRepository;
                _ulasımService = ulasımService;
            }

            public async Task<CanceledTransportationPassengerResponse> Handle(CancelTransportationPassengerCommand request, CancellationToken cancellationToken)
            {
                X.TransportationPassenger? transportationPassenger = await _transportationPassengerReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.TransportationGroupFK).ThenInclude(x => x.TransportationServiceFK));
                await _transportationPassengerBusinessRules.TransportationPassengerShouldExistWhenSelected(transportationPassenger);

                var response = await _ulasımService.YolcuIptalAsync(long.Parse(transportationPassenger.TransportationGroupFK.TransportationServiceFK.RefNoTransportation), long.Parse(transportationPassenger.RefNoTransportationPassenger));

                if (!response.Contains("HATA"))
                {
                    transportationPassenger.PassengerStatus = EnumPassengerStatus.Iptal;
                    transportationPassenger.RefNoTransportationPassenger = null;
                    _transportationPassengerWriteRepository.Update(transportationPassenger);
                    await _transportationPassengerWriteRepository.SaveAsync();
                    return new()
                    {
                        Title = TransportationPassengersBusinessMessages.ProcessCompleted,
                        Message = TransportationPassengersBusinessMessages.SuccessReportCancelTransportationPassengerMessage,
                        IsValid = true
                    };
                }
                  

                return new()
                {
                    Title = TransportationPassengersBusinessMessages.TechnicalError,
                    Message =  response,
                    IsValid = false
                };
            }
        }
    }
}

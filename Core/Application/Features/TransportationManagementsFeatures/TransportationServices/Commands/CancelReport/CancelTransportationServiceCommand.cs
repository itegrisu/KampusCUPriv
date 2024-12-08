using Application.Abstractions;
using Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Delete;
using Application.Features.TransportationManagementFeatures.TransportationServices.Constants;
using Application.Features.TransportationManagementFeatures.TransportationServices.Rules;
using Application.Repositories.TransportationRepos.TransportationServiceRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.CancelReport
{
    public class CancelTransportationServiceCommand : IRequest<CancaledTransportationServiceResponse>
    {
        public string refNoTransportation { get; set; }

        public class CancelTransportationServiceCommandHandler : IRequestHandler<CancelTransportationServiceCommand, CancaledTransportationServiceResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITransportationServiceReadRepository _transportationServiceReadRepository;
            private readonly ITransportationServiceWriteRepository _transportationServiceWriteRepository;
            private readonly TransportationServiceBusinessRules _transportationServiceBusinessRules;
            private readonly IUlasımService _ulasımService;
            public CancelTransportationServiceCommandHandler(IMapper mapper, ITransportationServiceReadRepository transportationServiceReadRepository,
                                             TransportationServiceBusinessRules transportationServiceBusinessRules, ITransportationServiceWriteRepository transportationServiceWriteRepository, IUlasımService ulasımService)
            {
                _mapper = mapper;
                _transportationServiceReadRepository = transportationServiceReadRepository;
                _transportationServiceBusinessRules = transportationServiceBusinessRules;
                _transportationServiceWriteRepository = transportationServiceWriteRepository;
                _ulasımService = ulasımService;
            }

            public async Task<CancaledTransportationServiceResponse> Handle(CancelTransportationServiceCommand request, CancellationToken cancellationToken)
            {
                X.TransportationService? transportationService = await _transportationServiceReadRepository.GetAsync(predicate: x => x.RefNoTransportation == request.refNoTransportation, cancellationToken: cancellationToken);
                await _transportationServiceBusinessRules.TransportationServiceShouldExistWhenSelected(transportationService);

                var sonuc = await _ulasımService.SeferIptalAsync(long.Parse(transportationService.RefNoTransportation));

                if (!sonuc.Contains("HATA"))
                {
                    transportationService.RefNoTransportation = null;
                    transportationService.TransportationServiceStatus =EnumTransportationServiceStatus.Iptal;

                    _transportationServiceWriteRepository.Update(transportationService);
                    await _transportationServiceWriteRepository.SaveAsync();
                    return new()
                    {
                        Title = TransportationServicesBusinessMessages.ProcessCompleted,
                        Message = TransportationServicesBusinessMessages.SuccessReportCancelledTransportatinServiceMessage,
                        IsValid = true
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

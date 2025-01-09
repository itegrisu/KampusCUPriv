using Application.Abstractions;
using Application.Features.TransportationManagementFeatures.TransportationServices.Constants;
using Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationServices.Rules;
using Application.Repositories.TransportationRepos.TransportationGroupRepo;
using Application.Repositories.TransportationRepos.TransportationServiceRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;
namespace Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.Print
{
    public class PrintTransportationServiceCommand : IRequest<PrintedTransportationServiceResponse>
    {
        public Guid Gid { get; set; }

        public class PrintTransportationServiceCommandHandler : IRequestHandler<PrintTransportationServiceCommand, PrintedTransportationServiceResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITransportationServiceWriteRepository _transportationServiceWriteRepository;
            private readonly ITransportationServiceReadRepository _transportationServiceReadRepository;
            private readonly TransportationServiceBusinessRules _transportationServiceBusinessRules;
            private readonly IUlasımService _ulasımService;
            private readonly ITransportationGroupReadRepository _transportationGroupReadRepository;
            public PrintTransportationServiceCommandHandler(IMapper mapper, ITransportationServiceWriteRepository transportationServiceWriteRepository,
                                             TransportationServiceBusinessRules transportationServiceBusinessRules, ITransportationServiceReadRepository transportationServiceReadRepository, IUlasımService ulasımService, ITransportationGroupReadRepository transportationGroupReadRepository)
            {
                _mapper = mapper;
                _transportationServiceWriteRepository = transportationServiceWriteRepository;
                _transportationServiceBusinessRules = transportationServiceBusinessRules;
                _transportationServiceReadRepository = transportationServiceReadRepository;
                _ulasımService = ulasımService;
                _transportationGroupReadRepository = transportationGroupReadRepository;
            }

            public async Task<PrintedTransportationServiceResponse> Handle(PrintTransportationServiceCommand request, CancellationToken cancellationToken)
            {
                X.TransportationService? transportationService = await _transportationServiceReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.TransportationFK).Include(x => x.VehicleAllFK));

                await _transportationServiceBusinessRules.TransportationServiceShouldExistWhenSelected(transportationService);

                await _transportationServiceBusinessRules.TransportationServiceShouldReport(request.Gid);
                await _transportationServiceBusinessRules.PersonnelShouldReport(transportationService.Gid);
                await _transportationServiceBusinessRules.GroupShouldReport(transportationService.Gid);

                var transportationGroup = await _transportationGroupReadRepository.GetAsync(predicate: x => x.GidTransportationServiceFK == transportationService.Gid);
                await _transportationServiceBusinessRules.PassengerShouldReport(transportationGroup.Gid);

                transportationService = _mapper.Map(request, transportationService);

                var response = await _ulasımService.SeferCiktisiAl(transportationService, "\\Files\\transportation-service-files\\");

                if (!response.Contains("HATA"))
                {
                    X.TransportationService? savedTransportationService = await _transportationServiceReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken,
                        include: x => x.Include(x => x.TransportationFK).Include(x => x.VehicleAllFK));

                    GetByGidTransportationServiceResponse obj = _mapper.Map<GetByGidTransportationServiceResponse>(savedTransportationService);

                    return new()
                    {
                        Title = TransportationServicesBusinessMessages.ProcessCompleted,
                        Message = response,
                        IsValid = true,
                        Obj = obj
                    };
                }

                return new()
                {
                    Title = TransportationServicesBusinessMessages.TechnicalError,
                    Message = response,
                    IsValid = false,
                };
            }
        }
    }
}

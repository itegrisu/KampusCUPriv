using Application.Features.TransportationManagementFeatures.TransportationServices.Constants;
using Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationServices.Rules;
using Application.Repositories.TransportationRepos.TransportationServiceRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Update;

public class UpdateTransportationServiceCommand : IRequest<UpdatedTransportationServiceResponse>
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



    public class UpdateTransportationServiceCommandHandler : IRequestHandler<UpdateTransportationServiceCommand, UpdatedTransportationServiceResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransportationServiceWriteRepository _transportationServiceWriteRepository;
        private readonly ITransportationServiceReadRepository _transportationServiceReadRepository;
        private readonly TransportationServiceBusinessRules _transportationServiceBusinessRules;

        public UpdateTransportationServiceCommandHandler(IMapper mapper, ITransportationServiceWriteRepository transportationServiceWriteRepository,
                                         TransportationServiceBusinessRules transportationServiceBusinessRules, ITransportationServiceReadRepository transportationServiceReadRepository)
        {
            _mapper = mapper;
            _transportationServiceWriteRepository = transportationServiceWriteRepository;
            _transportationServiceBusinessRules = transportationServiceBusinessRules;
            _transportationServiceReadRepository = transportationServiceReadRepository;
        }

        public async Task<UpdatedTransportationServiceResponse> Handle(UpdateTransportationServiceCommand request, CancellationToken cancellationToken)
        {
            X.TransportationService? transportationService = await _transportationServiceReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.TransportationFK).Include(x => x.VehicleAllFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _transportationServiceBusinessRules.TransportationServiceShouldExistWhenSelected(transportationService);
            transportationService = _mapper.Map(request, transportationService);

            _transportationServiceWriteRepository.Update(transportationService!);
            await _transportationServiceWriteRepository.SaveAsync();
            GetByGidTransportationServiceResponse obj = _mapper.Map<GetByGidTransportationServiceResponse>(transportationService);

            return new()
            {
                Title = TransportationServicesBusinessMessages.ProcessCompleted,
                Message = TransportationServicesBusinessMessages.SuccessCreatedTransportationServiceMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
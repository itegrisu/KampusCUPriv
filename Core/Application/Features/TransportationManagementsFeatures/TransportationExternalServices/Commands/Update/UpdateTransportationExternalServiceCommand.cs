using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Constants;
using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Rules;
using Application.Repositories.TransportationRepos.TransportationExternalServiceRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationExternalServices.Commands.Update;

public class UpdateTransportationExternalServiceCommand : IRequest<UpdatedTransportationExternalServiceResponse>
{
    public Guid Gid { get; set; }

    public Guid GidSupplierFK { get; set; }
    public Guid GidOrganizationFK { get; set; }
    public Guid GidFeeCurrencyFK { get; set; }

    public string Title { get; set; }
    public decimal Fee { get; set; }
    public string PlateNo { get; set; }
    public EnumExternalVehicleType ExternalVehicleType { get; set; }
    public int? PassengerCapacity { get; set; }
    public string? VehicleOfficer { get; set; }
    public string? VehiclePhone { get; set; }
    public bool? IsHasFile { get; set; }
    public EnumExternalServiceStatus ExternalServiceStatus { get; set; }
    public string? Description { get; set; }



    public class UpdateTransportationExternalServiceCommandHandler : IRequestHandler<UpdateTransportationExternalServiceCommand, UpdatedTransportationExternalServiceResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransportationExternalServiceWriteRepository _transportationExternalServiceWriteRepository;
        private readonly ITransportationExternalServiceReadRepository _transportationExternalServiceReadRepository;
        private readonly TransportationExternalServiceBusinessRules _transportationExternalServiceBusinessRules;

        public UpdateTransportationExternalServiceCommandHandler(IMapper mapper, ITransportationExternalServiceWriteRepository transportationExternalServiceWriteRepository,
                                         TransportationExternalServiceBusinessRules transportationExternalServiceBusinessRules, ITransportationExternalServiceReadRepository transportationExternalServiceReadRepository)
        {
            _mapper = mapper;
            _transportationExternalServiceWriteRepository = transportationExternalServiceWriteRepository;
            _transportationExternalServiceBusinessRules = transportationExternalServiceBusinessRules;
            _transportationExternalServiceReadRepository = transportationExternalServiceReadRepository;
        }

        public async Task<UpdatedTransportationExternalServiceResponse> Handle(UpdateTransportationExternalServiceCommand request, CancellationToken cancellationToken)
        {
            X.TransportationExternalService? transportationExternalService = await _transportationExternalServiceReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.CurrencyFK).Include(x => x.OrganizationFK).Include(x => x.SCCompanyFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _transportationExternalServiceBusinessRules.TransportationExternalServiceShouldExistWhenSelected(transportationExternalService);
            transportationExternalService = _mapper.Map(request, transportationExternalService);

            _transportationExternalServiceWriteRepository.Update(transportationExternalService!);
            await _transportationExternalServiceWriteRepository.SaveAsync();
            GetByGidTransportationExternalServiceResponse obj = _mapper.Map<GetByGidTransportationExternalServiceResponse>(transportationExternalService);

            return new()
            {
                Title = TransportationExternalServicesBusinessMessages.ProcessCompleted,
                Message = TransportationExternalServicesBusinessMessages.SuccessCreatedTransportationExternalServiceMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
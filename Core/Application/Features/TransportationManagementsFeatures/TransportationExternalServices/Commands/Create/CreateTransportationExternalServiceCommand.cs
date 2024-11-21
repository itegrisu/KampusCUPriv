using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Constants;
using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Rules;
using Application.Repositories.TransportationRepos.TransportationExternalServiceRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationExternalServices.Commands.Create;

public class CreateTransportationExternalServiceCommand : IRequest<CreatedTransportationExternalServiceResponse>
{
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



    public class CreateTransportationExternalServiceCommandHandler : IRequestHandler<CreateTransportationExternalServiceCommand, CreatedTransportationExternalServiceResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransportationExternalServiceWriteRepository _transportationExternalServiceWriteRepository;
        private readonly ITransportationExternalServiceReadRepository _transportationExternalServiceReadRepository;
        private readonly TransportationExternalServiceBusinessRules _transportationExternalServiceBusinessRules;

        public CreateTransportationExternalServiceCommandHandler(IMapper mapper, ITransportationExternalServiceWriteRepository transportationExternalServiceWriteRepository,
                                         TransportationExternalServiceBusinessRules transportationExternalServiceBusinessRules, ITransportationExternalServiceReadRepository transportationExternalServiceReadRepository)
        {
            _mapper = mapper;
            _transportationExternalServiceWriteRepository = transportationExternalServiceWriteRepository;
            _transportationExternalServiceBusinessRules = transportationExternalServiceBusinessRules;
            _transportationExternalServiceReadRepository = transportationExternalServiceReadRepository;
        }

        public async Task<CreatedTransportationExternalServiceResponse> Handle(CreateTransportationExternalServiceCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _transportationExternalServiceReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.TransportationExternalService transportationExternalService = _mapper.Map<X.TransportationExternalService>(request);
            //transportationExternalService.RowNo = maxRowNo + 1;

            await _transportationExternalServiceWriteRepository.AddAsync(transportationExternalService);
            await _transportationExternalServiceWriteRepository.SaveAsync();

            X.TransportationExternalService savedTransportationExternalService = await _transportationExternalServiceReadRepository.GetAsync(predicate: x => x.Gid == transportationExternalService.Gid,
                include: x => x.Include(x => x.CurrencyFK).Include(x => x.OrganizationFK).Include(x => x.SCCompanyFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidTransportationExternalServiceResponse obj = _mapper.Map<GetByGidTransportationExternalServiceResponse>(savedTransportationExternalService);
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
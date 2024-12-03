using Application.Features.TransportationManagementFeatures.TransportationServices.Constants;
using Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationServices.Rules;
using Application.Repositories.TransportationRepos.TransportationServiceRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Create;

public class CreateTransportationServiceCommand : IRequest<CreatedTransportationServiceResponse>
{
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

    public class CreateTransportationServiceCommandHandler : IRequestHandler<CreateTransportationServiceCommand, CreatedTransportationServiceResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransportationServiceWriteRepository _transportationServiceWriteRepository;
        private readonly ITransportationServiceReadRepository _transportationServiceReadRepository;
        private readonly TransportationServiceBusinessRules _transportationServiceBusinessRules;

        public CreateTransportationServiceCommandHandler(IMapper mapper, ITransportationServiceWriteRepository transportationServiceWriteRepository,
                                         TransportationServiceBusinessRules transportationServiceBusinessRules, ITransportationServiceReadRepository transportationServiceReadRepository)
        {
            _mapper = mapper;
            _transportationServiceWriteRepository = transportationServiceWriteRepository;
            _transportationServiceBusinessRules = transportationServiceBusinessRules;
            _transportationServiceReadRepository = transportationServiceReadRepository;
        }

        public async Task<CreatedTransportationServiceResponse> Handle(CreateTransportationServiceCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _transportationServiceReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.TransportationService transportationService = _mapper.Map<X.TransportationService>(request);
            //transportationService.RowNo = maxRowNo + 1;

            await _transportationServiceWriteRepository.AddAsync(transportationService);
            await _transportationServiceWriteRepository.SaveAsync();

            X.TransportationService savedTransportationService = await _transportationServiceReadRepository.GetAsync(predicate: x => x.Gid == transportationService.Gid,
                include: x => x.Include(x => x.TransportationFK).Include(x => x.VehicleAllFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidTransportationServiceResponse obj = _mapper.Map<GetByGidTransportationServiceResponse>(savedTransportationService);
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
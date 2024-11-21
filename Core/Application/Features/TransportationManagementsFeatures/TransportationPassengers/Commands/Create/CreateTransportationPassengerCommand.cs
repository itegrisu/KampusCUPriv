using Application.Features.TransportationManagementFeatures.TransportationPassengers.Constants;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Rules;
using Application.Repositories.TransportationRepos.TransportationPassengerRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationPassengers.Commands.Create;

public class CreateTransportationPassengerCommand : IRequest<CreatedTransportationPassengerResponse>
{
    
public string Country { get; set; }
public string IdentityNo { get; set; }
public string FirstName { get; set; }
public string LastName { get; set; }
public EnumGender Gender { get; set; }
public string? Phone { get; set; }
public EnumPassengerStatus PassengerStatus { get; set; }



    public class CreateTransportationPassengerCommandHandler : IRequestHandler<CreateTransportationPassengerCommand, CreatedTransportationPassengerResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransportationPassengerWriteRepository _transportationPassengerWriteRepository;
        private readonly ITransportationPassengerReadRepository _transportationPassengerReadRepository;
        private readonly TransportationPassengerBusinessRules _transportationPassengerBusinessRules;

        public CreateTransportationPassengerCommandHandler(IMapper mapper, ITransportationPassengerWriteRepository transportationPassengerWriteRepository,
                                         TransportationPassengerBusinessRules transportationPassengerBusinessRules, ITransportationPassengerReadRepository transportationPassengerReadRepository)
        {
            _mapper = mapper;
            _transportationPassengerWriteRepository = transportationPassengerWriteRepository;
            _transportationPassengerBusinessRules = transportationPassengerBusinessRules;
            _transportationPassengerReadRepository = transportationPassengerReadRepository;
        }

        public async Task<CreatedTransportationPassengerResponse> Handle(CreateTransportationPassengerCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _transportationPassengerReadRepository.GetAll().MaxAsync(r => r.RowNo);
			X.TransportationPassenger transportationPassenger = _mapper.Map<X.TransportationPassenger>(request);
            //transportationPassenger.RowNo = maxRowNo + 1;

            await _transportationPassengerWriteRepository.AddAsync(transportationPassenger);
            await _transportationPassengerWriteRepository.SaveAsync();

			X.TransportationPassenger savedTransportationPassenger = await _transportationPassengerReadRepository.GetAsync(predicate: x => x.Gid == transportationPassenger.Gid);
			//INCLUDES Buraya Gelecek include varsa eklenecek
			//include: x => x.Include(x => x.UserFK));

            GetByGidTransportationPassengerResponse obj = _mapper.Map<GetByGidTransportationPassengerResponse>(savedTransportationPassenger);
            return new()
            {           
                Title = TransportationPassengersBusinessMessages.ProcessCompleted,
                Message = TransportationPassengersBusinessMessages.SuccessCreatedTransportationPassengerMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
using Application.Features.VehicleManagementFeatures.VehicleRequests.Constants;
using Application.Features.VehicleManagementFeatures.VehicleRequests.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleRequests.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleRequestRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleRequests.Commands.Create;

public class CreateVehicleRequestCommand : IRequest<CreatedVehicleRequestResponse>
{
    public Guid GidVehicleFK { get; set; }
    public Guid GidRequestUserFK { get; set; }
    public Guid GidApprovedUserFK { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string UseAim { get; set; }
    public EnumVehicleApprovedStatus VehicleApprovedStatus { get; set; }



    public class CreateVehicleRequestCommandHandler : IRequestHandler<CreateVehicleRequestCommand, CreatedVehicleRequestResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRequestWriteRepository _vehicleRequestWriteRepository;
        private readonly IVehicleRequestReadRepository _vehicleRequestReadRepository;
        private readonly VehicleRequestBusinessRules _vehicleRequestBusinessRules;

        public CreateVehicleRequestCommandHandler(IMapper mapper, IVehicleRequestWriteRepository vehicleRequestWriteRepository,
                                         VehicleRequestBusinessRules vehicleRequestBusinessRules, IVehicleRequestReadRepository vehicleRequestReadRepository)
        {
            _mapper = mapper;
            _vehicleRequestWriteRepository = vehicleRequestWriteRepository;
            _vehicleRequestBusinessRules = vehicleRequestBusinessRules;
            _vehicleRequestReadRepository = vehicleRequestReadRepository;
        }

        public async Task<CreatedVehicleRequestResponse> Handle(CreateVehicleRequestCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _vehicleRequestReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.VehicleRequest vehicleRequest = _mapper.Map<X.VehicleRequest>(request);
            //vehicleRequest.RowNo = maxRowNo + 1;

            await _vehicleRequestWriteRepository.AddAsync(vehicleRequest);
            await _vehicleRequestWriteRepository.SaveAsync();

            X.VehicleRequest savedVehicleRequest = await _vehicleRequestReadRepository.GetAsync(predicate: x => x.Gid == vehicleRequest.Gid, include: x => x.Include(x => x.VehicleAllFK).Include(x => x.RequestUserFK).Include(x => x.ApprovedUserFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidVehicleRequestResponse obj = _mapper.Map<GetByGidVehicleRequestResponse>(savedVehicleRequest);
            return new()
            {
                Title = VehicleRequestsBusinessMessages.ProcessCompleted,
                Message = VehicleRequestsBusinessMessages.SuccessCreatedVehicleRequestMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
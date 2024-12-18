using Application.Features.VehicleManagementFeatures.VehicleAccidents.Constants;
using Application.Features.VehicleManagementFeatures.VehicleAccidents.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleAccidents.Rules;
using AutoMapper;
using X = Domain.Entities.VehicleManagements;
using MediatR;
using Application.Repositories.VehicleManagementsRepos.VehicleAccidentRepo;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.VehicleManagementFeatures.VehicleAccidents.Commands.Update;

public class UpdateVehicleAccidentCommand : IRequest<UpdatedVehicleAccidentResponse>
{
    public Guid Gid { get; set; }
    public Guid GidVehicleFK { get; set; }
    public DateTime AccidentDate { get; set; }
    public string Driver { get; set; }
    public string? AccidentFile { get; set; }
    public string? AccidentImageFile { get; set; }

    public class UpdateVehicleAccidentCommandHandler : IRequestHandler<UpdateVehicleAccidentCommand, UpdatedVehicleAccidentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleAccidentWriteRepository _vehicleAccidentWriteRepository;
        private readonly IVehicleAccidentReadRepository _vehicleAccidentReadRepository;
        private readonly VehicleAccidentBusinessRules _vehicleAccidentBusinessRules;

        public UpdateVehicleAccidentCommandHandler(IMapper mapper, IVehicleAccidentWriteRepository vehicleAccidentWriteRepository,
                                         VehicleAccidentBusinessRules vehicleAccidentBusinessRules, IVehicleAccidentReadRepository vehicleAccidentReadRepository)
        {
            _mapper = mapper;
            _vehicleAccidentWriteRepository = vehicleAccidentWriteRepository;
            _vehicleAccidentBusinessRules = vehicleAccidentBusinessRules;
            _vehicleAccidentReadRepository = vehicleAccidentReadRepository;
        }

        public async Task<UpdatedVehicleAccidentResponse> Handle(UpdateVehicleAccidentCommand request, CancellationToken cancellationToken)
        {
            X.VehicleAccident? vehicleAccident = await _vehicleAccidentReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.VehicleAllFK));
            await _vehicleAccidentBusinessRules.VehicleAccidentShouldExistWhenSelected(vehicleAccident);
            vehicleAccident = _mapper.Map(request, vehicleAccident);

            _vehicleAccidentWriteRepository.Update(vehicleAccident!);
            await _vehicleAccidentWriteRepository.SaveAsync();
            GetByGidVehicleAccidentResponse obj = _mapper.Map<GetByGidVehicleAccidentResponse>(vehicleAccident);

            return new()
            {
                Title = VehicleAccidentsBusinessMessages.ProcessCompleted,
                Message = VehicleAccidentsBusinessMessages.SuccessCreatedVehicleAccidentMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
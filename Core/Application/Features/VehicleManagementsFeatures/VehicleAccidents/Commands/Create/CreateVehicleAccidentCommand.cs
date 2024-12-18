using Application.Features.VehicleManagementFeatures.VehicleAccidents.Constants;
using Application.Features.VehicleManagementFeatures.VehicleAccidents.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleAccidents.Rules;
using AutoMapper;
using X = Domain.Entities.VehicleManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.VehicleManagementsRepos.VehicleAccidentRepo;

namespace Application.Features.VehicleManagementFeatures.VehicleAccidents.Commands.Create;

public class CreateVehicleAccidentCommand : IRequest<CreatedVehicleAccidentResponse>
{
    public Guid GidVehicleFK { get; set; }
    public DateTime AccidentDate { get; set; }
    public string Driver { get; set; }
    public string? AccidentFile { get; set; }
    public string? AccidentImageFile { get; set; }



    public class CreateVehicleAccidentCommandHandler : IRequestHandler<CreateVehicleAccidentCommand, CreatedVehicleAccidentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleAccidentWriteRepository _vehicleAccidentWriteRepository;
        private readonly IVehicleAccidentReadRepository _vehicleAccidentReadRepository;
        private readonly VehicleAccidentBusinessRules _vehicleAccidentBusinessRules;

        public CreateVehicleAccidentCommandHandler(IMapper mapper, IVehicleAccidentWriteRepository vehicleAccidentWriteRepository,
                                         VehicleAccidentBusinessRules vehicleAccidentBusinessRules, IVehicleAccidentReadRepository vehicleAccidentReadRepository)
        {
            _mapper = mapper;
            _vehicleAccidentWriteRepository = vehicleAccidentWriteRepository;
            _vehicleAccidentBusinessRules = vehicleAccidentBusinessRules;
            _vehicleAccidentReadRepository = vehicleAccidentReadRepository;
        }

        public async Task<CreatedVehicleAccidentResponse> Handle(CreateVehicleAccidentCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _vehicleAccidentReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.VehicleAccident vehicleAccident = _mapper.Map<X.VehicleAccident>(request);
            //vehicleAccident.RowNo = maxRowNo + 1;

            await _vehicleAccidentWriteRepository.AddAsync(vehicleAccident);
            await _vehicleAccidentWriteRepository.SaveAsync();

            X.VehicleAccident savedVehicleAccident = await _vehicleAccidentReadRepository.GetAsync(predicate: x => x.Gid == vehicleAccident.Gid, include: x => x.Include(x => x.VehicleAllFK));

            GetByGidVehicleAccidentResponse obj = _mapper.Map<GetByGidVehicleAccidentResponse>(savedVehicleAccident);
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
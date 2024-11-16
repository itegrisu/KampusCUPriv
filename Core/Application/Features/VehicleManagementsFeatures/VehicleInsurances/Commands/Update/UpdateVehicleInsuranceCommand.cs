using Application.Features.VehicleManagementFeatures.VehicleInsurances.Constants;
using Application.Features.VehicleManagementFeatures.VehicleInsurances.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleInsurances.Rules;
using AutoMapper;
using X = Domain.Entities.VehicleManagements;
using MediatR;
using Domain.Enums;
using Application.Repositories.VehicleManagementsRepos.VehicleInsuranceRepo;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.VehicleManagementFeatures.VehicleInsurances.Commands.Update;

public class UpdateVehicleInsuranceCommand : IRequest<UpdatedVehicleInsuranceResponse>
{
    public Guid Gid { get; set; }

    public Guid GidVehicleFK { get; set; }

    public EnumInsuranceType InsuranceType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int InsuranceFee { get; set; }
    public string? DocumentFile { get; set; }
    public string? Description { get; set; }



    public class UpdateVehicleInsuranceCommandHandler : IRequestHandler<UpdateVehicleInsuranceCommand, UpdatedVehicleInsuranceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleInsuranceWriteRepository _vehicleInsuranceWriteRepository;
        private readonly IVehicleInsuranceReadRepository _vehicleInsuranceReadRepository;
        private readonly VehicleInsuranceBusinessRules _vehicleInsuranceBusinessRules;

        public UpdateVehicleInsuranceCommandHandler(IMapper mapper, IVehicleInsuranceWriteRepository vehicleInsuranceWriteRepository,
                                         VehicleInsuranceBusinessRules vehicleInsuranceBusinessRules, IVehicleInsuranceReadRepository vehicleInsuranceReadRepository)
        {
            _mapper = mapper;
            _vehicleInsuranceWriteRepository = vehicleInsuranceWriteRepository;
            _vehicleInsuranceBusinessRules = vehicleInsuranceBusinessRules;
            _vehicleInsuranceReadRepository = vehicleInsuranceReadRepository;
        }

        public async Task<UpdatedVehicleInsuranceResponse> Handle(UpdateVehicleInsuranceCommand request, CancellationToken cancellationToken)
        {
            X.VehicleInsurance? vehicleInsurance = await _vehicleInsuranceReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.VehicleAllFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _vehicleInsuranceBusinessRules.VehicleInsuranceShouldExistWhenSelected(vehicleInsurance);
            vehicleInsurance = _mapper.Map(request, vehicleInsurance);

            _vehicleInsuranceWriteRepository.Update(vehicleInsurance!);
            await _vehicleInsuranceWriteRepository.SaveAsync();
            GetByGidVehicleInsuranceResponse obj = _mapper.Map<GetByGidVehicleInsuranceResponse>(vehicleInsurance);

            return new()
            {
                Title = VehicleInsurancesBusinessMessages.ProcessCompleted,
                Message = VehicleInsurancesBusinessMessages.SuccessCreatedVehicleInsuranceMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
using Application.Features.VehicleManagementFeatures.VehicleInsurances.Constants;
using Application.Features.VehicleManagementFeatures.VehicleInsurances.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleInsurances.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleInsuranceRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleInsurances.Commands.Create;

public class CreateVehicleInsuranceCommand : IRequest<CreatedVehicleInsuranceResponse>
{
    public Guid GidVehicleFK { get; set; }

    public EnumInsuranceType InsuranceType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int InsuranceFee { get; set; }
    public string? DocumentFile { get; set; }
    public string? Description { get; set; }



    public class CreateVehicleInsuranceCommandHandler : IRequestHandler<CreateVehicleInsuranceCommand, CreatedVehicleInsuranceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleInsuranceWriteRepository _vehicleInsuranceWriteRepository;
        private readonly IVehicleInsuranceReadRepository _vehicleInsuranceReadRepository;
        private readonly VehicleInsuranceBusinessRules _vehicleInsuranceBusinessRules;

        public CreateVehicleInsuranceCommandHandler(IMapper mapper, IVehicleInsuranceWriteRepository vehicleInsuranceWriteRepository,
                                         VehicleInsuranceBusinessRules vehicleInsuranceBusinessRules, IVehicleInsuranceReadRepository vehicleInsuranceReadRepository)
        {
            _mapper = mapper;
            _vehicleInsuranceWriteRepository = vehicleInsuranceWriteRepository;
            _vehicleInsuranceBusinessRules = vehicleInsuranceBusinessRules;
            _vehicleInsuranceReadRepository = vehicleInsuranceReadRepository;
        }

        public async Task<CreatedVehicleInsuranceResponse> Handle(CreateVehicleInsuranceCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _vehicleInsuranceReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.VehicleInsurance vehicleInsurance = _mapper.Map<X.VehicleInsurance>(request);
            //vehicleInsurance.RowNo = maxRowNo + 1;

            await _vehicleInsuranceWriteRepository.AddAsync(vehicleInsurance);
            await _vehicleInsuranceWriteRepository.SaveAsync();

            X.VehicleInsurance savedVehicleInsurance = await _vehicleInsuranceReadRepository.GetAsync(predicate: x => x.Gid == vehicleInsurance.Gid, 
                include: x => x.Include(x => x.VehicleAllFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidVehicleInsuranceResponse obj = _mapper.Map<GetByGidVehicleInsuranceResponse>(savedVehicleInsurance);
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
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Constants;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Rules;
using AutoMapper;
using X = Domain.Entities.VehicleManagements;
using MediatR;
using Domain.Enums;
using Application.Repositories.VehicleManagementsRepos.VehicleTransactionRepo;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.VehicleManagementFeatures.VehicleTransactions.Commands.Update;

public class UpdateVehicleTransactionCommand : IRequest<UpdatedVehicleTransactionResponse>
{
    public Guid? Gid { get; set; }
    public Guid? GidVehicleFK { get; set; }
    public Guid? GidSupplierCustomerFK { get; set; }
    public Guid? GidVehicleUsePersonnelFK { get; set; }
    public int StartKM { get; set; }
    public int? EndKM { get; set; }
    public int? MonthlyRentalFee { get; set; }
    public DateTime? ContractStartDate { get; set; }
    public DateTime? ContractEndDate { get; set; }
    public string? ContactPerson { get; set; }
    public string? ContactPhone { get; set; }
    public string? ArventoAPIInfo { get; set; }
    public string? LicenseFile { get; set; }
    public string? ContractFile { get; set; }
    public string? Description { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public DateTime? EndDate { get; set; }
    public EnumVehicleStatus VehicleStatus { get; set; }

    public class UpdateVehicleTransactionCommandHandler : IRequestHandler<UpdateVehicleTransactionCommand, UpdatedVehicleTransactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleTransactionWriteRepository _vehicleTransactionWriteRepository;
        private readonly IVehicleTransactionReadRepository _vehicleTransactionReadRepository;
        private readonly VehicleTransactionBusinessRules _vehicleTransactionBusinessRules;

        public UpdateVehicleTransactionCommandHandler(IMapper mapper, IVehicleTransactionWriteRepository vehicleTransactionWriteRepository,
                                         VehicleTransactionBusinessRules vehicleTransactionBusinessRules, IVehicleTransactionReadRepository vehicleTransactionReadRepository)
        {
            _mapper = mapper;
            _vehicleTransactionWriteRepository = vehicleTransactionWriteRepository;
            _vehicleTransactionBusinessRules = vehicleTransactionBusinessRules;
            _vehicleTransactionReadRepository = vehicleTransactionReadRepository;
        }

        public async Task<UpdatedVehicleTransactionResponse> Handle(UpdateVehicleTransactionCommand request, CancellationToken cancellationToken)
        {
            X.VehicleTransaction? vehicleTransaction = await _vehicleTransactionReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.SCCompanyFK).Include(x => x.UserFK).Include(x => x.VehicleAllFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _vehicleTransactionBusinessRules.VehicleTransactionShouldExistWhenSelected(vehicleTransaction);
            vehicleTransaction = _mapper.Map(request, vehicleTransaction);

            _vehicleTransactionWriteRepository.Update(vehicleTransaction!);
            await _vehicleTransactionWriteRepository.SaveAsync();
            GetByGidVehicleTransactionResponse obj = _mapper.Map<GetByGidVehicleTransactionResponse>(vehicleTransaction);

            return new()
            {
                Title = VehicleTransactionsBusinessMessages.ProcessCompleted,
                Message = VehicleTransactionsBusinessMessages.SuccessCreatedVehicleTransactionMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
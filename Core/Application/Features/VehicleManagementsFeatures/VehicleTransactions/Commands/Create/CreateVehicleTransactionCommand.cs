using Application.Features.VehicleManagementFeatures.VehicleTransactions.Constants;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Rules;
using AutoMapper;
using X = Domain.Entities.VehicleManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;
using Application.Repositories.VehicleManagementsRepos.VehicleTransactionRepo;

namespace Application.Features.VehicleManagementFeatures.VehicleTransactions.Commands.Create;

public class CreateVehicleTransactionCommand : IRequest<CreatedVehicleTransactionResponse>
{
    public Guid GidVehicleAllFK { get; set; }
    public Guid GidSupplierCustomerFK { get; set; }
    public Guid GidVehicleUsePersonnelFK { get; set; }
    public int StartKM { get; set; }
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
    public DateTime? SaleDate { get; set; }
    public EnumVehicleStatus VehicleStatus { get; set; }



    public class CreateVehicleTransactionCommandHandler : IRequestHandler<CreateVehicleTransactionCommand, CreatedVehicleTransactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleTransactionWriteRepository _vehicleTransactionWriteRepository;
        private readonly IVehicleTransactionReadRepository _vehicleTransactionReadRepository;
        private readonly VehicleTransactionBusinessRules _vehicleTransactionBusinessRules;

        public CreateVehicleTransactionCommandHandler(IMapper mapper, IVehicleTransactionWriteRepository vehicleTransactionWriteRepository,
                                         VehicleTransactionBusinessRules vehicleTransactionBusinessRules, IVehicleTransactionReadRepository vehicleTransactionReadRepository)
        {
            _mapper = mapper;
            _vehicleTransactionWriteRepository = vehicleTransactionWriteRepository;
            _vehicleTransactionBusinessRules = vehicleTransactionBusinessRules;
            _vehicleTransactionReadRepository = vehicleTransactionReadRepository;
        }

        public async Task<CreatedVehicleTransactionResponse> Handle(CreateVehicleTransactionCommand request, CancellationToken cancellationToken)
        {
            if (request.VehicleStatus == EnumVehicleStatus.FirmaAraci)
                await _vehicleTransactionBusinessRules.VehicleAllReadyExist(request.GidVehicleAllFK);

            if (request.VehicleStatus == EnumVehicleStatus.KiralikVerilenArac)            
                await _vehicleTransactionBusinessRules.IsSuitableForHireVehicle(request.GidVehicleAllFK);
            

            if (request.VehicleStatus == EnumVehicleStatus.SatilanArac)            
                await _vehicleTransactionBusinessRules.IsSuitableForSaleVehicle(request.GidVehicleAllFK);

            if (request.VehicleStatus == EnumVehicleStatus.TahsisArac)
                await _vehicleTransactionBusinessRules.IsSuitableForAllocated(request.GidVehicleAllFK);


            //int maxRowNo = await _vehicleTransactionReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.VehicleTransaction vehicleTransaction = _mapper.Map<X.VehicleTransaction>(request);
            //vehicleTransaction.RowNo = maxRowNo + 1;

            await _vehicleTransactionWriteRepository.AddAsync(vehicleTransaction);
            await _vehicleTransactionWriteRepository.SaveAsync();

            X.VehicleTransaction savedVehicleTransaction = await _vehicleTransactionReadRepository.GetAsync(predicate: x => x.Gid == vehicleTransaction.Gid,
                include: x => x.Include(x => x.SCCompanyFK).Include(x => x.UserFK).Include(x => x.VehicleAllFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidVehicleTransactionResponse obj = _mapper.Map<GetByGidVehicleTransactionResponse>(savedVehicleTransaction);
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
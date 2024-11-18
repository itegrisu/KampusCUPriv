using Application.Features.VehicleManagementFeatures.VehicleTransactions.Constants;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Rules;
using AutoMapper;
using X = Domain.Entities.VehicleManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;
using Application.Repositories.VehicleManagementsRepos.VehicleTransactionRepo;
using Domain.Entities.VehicleManagements;
using Core.Enum;

namespace Application.Features.VehicleManagementFeatures.VehicleTransactions.Commands.Create;

public class CreateVehicleTransactionCommand : IRequest<CreatedVehicleTransactionResponse>
{
    public Guid GidVehicleFK { get; set; }
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
                await _vehicleTransactionBusinessRules.VehicleAllReadyExist(request.GidVehicleFK);

            if (request.VehicleStatus == EnumVehicleStatus.KiralikVerilenArac)            
                await _vehicleTransactionBusinessRules.IsSuitableForHireVehicle(request.GidVehicleFK);

            if (request.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac)
                await _vehicleTransactionBusinessRules.IsSuitableForTakeVehicle(request.GidVehicleFK);

            if (request.VehicleStatus == EnumVehicleStatus.SatilanArac)            
                await _vehicleTransactionBusinessRules.IsSuitableForSaleVehicle(request.GidVehicleFK);

            if (request.VehicleStatus == EnumVehicleStatus.TahsisArac)
                await _vehicleTransactionBusinessRules.IsSuitableForAllocated(request.GidVehicleFK);

            VehicleTransaction vehicleTransactionOld = await _vehicleTransactionReadRepository.GetSingleAsync(x => x.GidVehicleFK == request.GidVehicleFK); 
            VehicleTransaction vehicleTransactionNew = vehicleTransactionOld;
            // Þuanda requestten gelen yeni veriler ile eski verileri matchleyip yeni veriye aktarmamýz lazým ama hangi verilerin yeniden hangi verilerin eskiden alacaðýmýza karar vermemiz lazým.
            vehicleTransactionNew.Gid = Guid.NewGuid();
            if (vehicleTransactionOld != null)
            {
                //Firma Aracý
                if (vehicleTransactionOld.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac && request.VehicleStatus == EnumVehicleStatus.FirmaAraci)
                {
                    throw new Exception("Bu araç firma aracý olarak eklenemez");
                }

                //Kiralýk Verilen Araç

                if (vehicleTransactionOld.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac && request.VehicleStatus == EnumVehicleStatus.KiralikVerilenArac)
                {
                    throw new Exception("Bu araç Kiralýk Verilen Araç olarak eklenemez");
                }

                if (vehicleTransactionOld.VehicleStatus == EnumVehicleStatus.SatilanArac && request.VehicleStatus == EnumVehicleStatus.KiralikVerilenArac)
                {
                    throw new Exception("Bu araç Kiralýk Verilen Araç olarak eklenemez");
                }

                //Kiralýk Alýnan Araç

                if (vehicleTransactionOld.VehicleStatus == EnumVehicleStatus.FirmaAraci && request.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac)
                {
                    throw new Exception("Bu araç Kiralýk Alýnan Araç olarak eklenemez");
                }

                if (vehicleTransactionOld.VehicleStatus == EnumVehicleStatus.KiralikVerilenArac && request.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac)
                {
                    throw new Exception("Bu araç Kiralýk Alýnan Araç olarak eklenemez");
                }

                if (vehicleTransactionOld.VehicleStatus == EnumVehicleStatus.TahsisArac && request.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac)
                {
                    throw new Exception("Bu araç Kiralýk Alýnan Araç olarak eklenemez");
                }

                //Satýlan Araç

                if (vehicleTransactionOld.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac && request.VehicleStatus == EnumVehicleStatus.SatilanArac)
                {
                    throw new Exception("Bu araç Satýlan Araç olarak eklenemez");
                }

                //Tahsis Araç

                if (vehicleTransactionOld.VehicleStatus == EnumVehicleStatus.SatilanArac && request.VehicleStatus == EnumVehicleStatus.TahsisArac)
                {
                    throw new Exception("Bu araç Satýlan Araç olarak eklenemez");
                }

                if (request.VehicleStatus == EnumVehicleStatus.FirmaAraci)
                {
                    vehicleTransactionNew.StartKM = request.EndKM.Value;
                }


                vehicleTransactionOld.DataState = DataState.Archive;
                vehicleTransactionOld.EndDate = DateTime.Now;

                _vehicleTransactionWriteRepository.Update(vehicleTransactionOld);

                vehicleTransactionNew.VehicleStatus = request.VehicleStatus;
                await _vehicleTransactionWriteRepository.AddAsync(vehicleTransactionNew);

                await _vehicleTransactionWriteRepository.SaveAsync();

                X.VehicleTransaction savedVehicleTransaction = await _vehicleTransactionReadRepository.GetAsync(predicate: x => x.Gid == vehicleTransactionNew.Gid,
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

            //int maxRowNo = await _vehicleTransactionReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.VehicleTransaction vehicleTransaction = _mapper.Map<X.VehicleTransaction>(request);
            //vehicleTransaction.RowNo = maxRowNo + 1;

            await _vehicleTransactionWriteRepository.AddAsync(vehicleTransaction);
            await _vehicleTransactionWriteRepository.SaveAsync();

            X.VehicleTransaction savedVehicleTransactionNew = await _vehicleTransactionReadRepository.GetAsync(predicate: x => x.Gid == vehicleTransaction.Gid,
                include: x => x.Include(x => x.SCCompanyFK).Include(x => x.UserFK).Include(x => x.VehicleAllFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidVehicleTransactionResponse objNew = _mapper.Map<GetByGidVehicleTransactionResponse>(savedVehicleTransactionNew);
            return new()
            {
                Title = VehicleTransactionsBusinessMessages.ProcessCompleted,
                Message = VehicleTransactionsBusinessMessages.SuccessCreatedVehicleTransactionMessage,
                IsValid = true,
                Obj = objNew
            };
        }
    }
}
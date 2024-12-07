using Application.Abstractions.UnitOfWork;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Constants;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleTransactionRepo;
using AutoMapper;
using Core.CrossCuttingConcern.Exceptions;
using Core.Enum;
using Domain.Entities.VehicleManagements;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.VehicleManagementFeatures.VehicleTransactions.Commands.Create;

public class CreateVehicleTransactionCommand : IRequest<CreatedVehicleTransactionResponse>
{
    public Guid GidVehicleFK { get; set; }
    public Guid? GidSupplierCustomerFK { get; set; }
    public Guid? GidVehicleUsePersonnelFK { get; set; }
    public Guid? GidFeeCurrencyFK { get; set; }
    public int StartKM { get; set; }
    public int? EndKM { get; set; }
    public int? Fee { get; set; }
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
        private readonly IUnitOfWork _unitOfWork;

        public CreateVehicleTransactionCommandHandler(IMapper mapper, IVehicleTransactionWriteRepository vehicleTransactionWriteRepository,
                                         VehicleTransactionBusinessRules vehicleTransactionBusinessRules, IVehicleTransactionReadRepository vehicleTransactionReadRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _vehicleTransactionWriteRepository = vehicleTransactionWriteRepository;
            _vehicleTransactionBusinessRules = vehicleTransactionBusinessRules;
            _vehicleTransactionReadRepository = vehicleTransactionReadRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreatedVehicleTransactionResponse> Handle(CreateVehicleTransactionCommand request, CancellationToken cancellationToken)
        {
            var existingTransaction = await _vehicleTransactionReadRepository.GetSingleAsync(x => x.GidVehicleFK == request.GidVehicleFK);
            VehicleTransaction newTransaction;

            if (existingTransaction == null)
            {
                if (request.VehicleStatus == EnumVehicleStatus.FirmaAraci || request.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac)
                {
                    newTransaction = _mapper.Map<VehicleTransaction>(request);
                    await _vehicleTransactionWriteRepository.AddAsync(newTransaction);
                    await _vehicleTransactionWriteRepository.SaveAsync();

                    return await CreateResponse(newTransaction);
                }
                throw new Exception("Ara� Firma Arac� veya Kiral�k Al�nan Ara� olmad��� i�in eklenemiyor!");
            }
            else
            {
                await _vehicleTransactionBusinessRules.CheckKM(existingTransaction.EndKM, request.StartKM);

                return await HandleExistingTransaction(existingTransaction, request);
            }
        }
        private async Task<CreatedVehicleTransactionResponse> CreateResponse(VehicleTransaction transaction)
        {
            // Commit sonras� i�lem
            var savedTransaction = await _vehicleTransactionReadRepository.GetAsync(  //Elma
                predicate: x => x.Gid == transaction.Gid,
                include: x => x.Include(t => t.SCCompanyFK).Include(t => t.UserFK).Include(t => t.VehicleAllFK).Include(x => x.CurrencyFK), false);

            var responseObject = _mapper.Map<GetByGidVehicleTransactionResponse>(savedTransaction);

            return new CreatedVehicleTransactionResponse
            {
                Title = VehicleTransactionsBusinessMessages.ProcessCompleted,
                Message = VehicleTransactionsBusinessMessages.SuccessCreatedVehicleTransactionMessage,
                IsValid = true,
                Obj = responseObject
            };
        }

        private async Task<CreatedVehicleTransactionResponse> HandleExistingTransaction(VehicleTransaction existingTransaction, CreateVehicleTransactionCommand request)
        {
            VehicleTransaction newTransaction;

            switch (request.VehicleStatus)
            {
                case EnumVehicleStatus.FirmaAraci:
                    newTransaction = await HandleFirmaAraciTransaction(existingTransaction, request);
                    break;

                case EnumVehicleStatus.KiralikVerilenArac:
                    newTransaction = await HandleKiralikVerilenAracTransaction(existingTransaction, request);
                    break;

                case EnumVehicleStatus.KiralikAlinanArac:
                    newTransaction = await HandleKiralikAlinanAracTransaction(existingTransaction, request);
                    break;

                case EnumVehicleStatus.SatilanArac:
                    newTransaction = await HandleSatilanAracTransaction(existingTransaction, request);
                    break;

                case EnumVehicleStatus.TahsisArac:
                    newTransaction = await HandleTahsisAracTransaction(existingTransaction, request);
                    break;

                default:
                    throw new Exception("Bilinmeyen ara� durumu!");
            }

            return await CreateResponse(newTransaction);
        }

        private async Task<VehicleTransaction> HandleFirmaAraciTransaction(VehicleTransaction existingTransaction, CreateVehicleTransactionCommand request)
        {
            if (existingTransaction.VehicleStatus == EnumVehicleStatus.FirmaAraci)
                throw new Exception("Bu ara� zaten firma arac� durumundad�r.");

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.KiralikVerilenArac)
                return await ArchiveAndCreateNewTransaction(existingTransaction, request);

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac)
                return await ArchiveAndCreateNewTransaction(existingTransaction, request);

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.SatilanArac)
                return await ArchiveAndCreateNewTransaction(existingTransaction, request);

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.TahsisArac)
            {
                await _vehicleTransactionBusinessRules.IsCompanyEmployee(existingTransaction.GidVehicleUsePersonnelFK);
                return await ArchiveAndCreateNewTransaction(existingTransaction, request);
            }

            throw new Exception("Bilinmeyen ara� durumu!");

        }

        private async Task<VehicleTransaction> HandleKiralikVerilenAracTransaction(VehicleTransaction existingTransaction, CreateVehicleTransactionCommand request)
        {
            if (existingTransaction.VehicleStatus == EnumVehicleStatus.FirmaAraci)
                return await ArchiveAndCreateNewTransaction(existingTransaction, request);

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.KiralikVerilenArac)
                throw new Exception("Bu ara� zaten kiral�k verilen ara� durumundad�r.");

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac)
                throw new Exception("Bu ara� kiral�k al�nan ara� olarak eklenemez.");

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.SatilanArac)
                throw new Exception("Bu ara� sat�lan ara� olarak eklenemez.");

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.TahsisArac)
            {
                await _vehicleTransactionBusinessRules.IsCompanyEmployee(existingTransaction.GidVehicleUsePersonnelFK);
                return await ArchiveAndCreateNewTransaction(existingTransaction, request);
            }
            throw new Exception("Bilinmeyen ara� durumu!");
        }

        private async Task<VehicleTransaction> HandleKiralikAlinanAracTransaction(VehicleTransaction existingTransaction, CreateVehicleTransactionCommand request)
        {
            if (existingTransaction.VehicleStatus == EnumVehicleStatus.FirmaAraci)
                throw new Exception("Bu ara� firma arac� olarak eklenemez.");

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.KiralikVerilenArac)
                throw new Exception("Bu ara� firma arac� olarak eklenemez.");

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac)
                throw new Exception("Bu ara� zaten kiral�k al�nan ara� olarak eklenmi�.");

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.SatilanArac)
                return await ArchiveAndCreateNewTransaction(existingTransaction, request);

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.TahsisArac)
                throw new Exception("Bu ara� tahsis ara� olarak eklenemez.");
            throw new Exception("Bilinmeyen ara� durumu!");
        }

        private async Task<VehicleTransaction> HandleSatilanAracTransaction(VehicleTransaction existingTransaction, CreateVehicleTransactionCommand request)
        {
            if (existingTransaction.VehicleStatus == EnumVehicleStatus.FirmaAraci)
                return await ArchiveAndCreateNewTransaction(existingTransaction, request);

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.KiralikVerilenArac)
                return await ArchiveAndCreateNewTransaction(existingTransaction, request);

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac)
                throw new Exception("Bu ara� kiral�k al�nan ara� olarak eklenemez.");

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.SatilanArac)
                throw new Exception("Bu ara� zaten sat�lan ara� olarak eklenmi�.");

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.TahsisArac)
            {
                await _vehicleTransactionBusinessRules.IsCompanyEmployee(existingTransaction.GidVehicleUsePersonnelFK);
                return await ArchiveAndCreateNewTransaction(existingTransaction, request);
            }
            throw new Exception("Bilinmeyen ara� durumu!");
        }

        private async Task<VehicleTransaction> HandleTahsisAracTransaction(VehicleTransaction existingTransaction, CreateVehicleTransactionCommand request)
        {
            if (existingTransaction.VehicleStatus == EnumVehicleStatus.FirmaAraci)
                return await ArchiveAndCreateNewTransaction(existingTransaction, request);

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.KiralikVerilenArac)
                return await ArchiveAndCreateNewTransaction(existingTransaction, request);

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac)
                return await ArchiveAndCreateNewTransaction(existingTransaction, request);

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.SatilanArac)
                throw new Exception("Bu ara� sat�lan ara� olarak eklenemez.");

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.TahsisArac)
                throw new Exception("Bu ara� zaten tahsis arac� olarak eklenmi�.");
            throw new Exception("Bilinmeyen ara� durumu!");

        }

        private async Task<VehicleTransaction> ArchiveAndCreateNewTransaction(VehicleTransaction existingTransaction, CreateVehicleTransactionCommand request)
        {
            _unitOfWork.BeginTransaction(); // Transaction ba�lat�l�r
            try
            {
                // Mevcut i�lemi ar�ivle
                existingTransaction.DataState = DataState.Archive;
                existingTransaction.EndDate = DateTime.Now;
                existingTransaction.EndKM = request.StartKM;
                _vehicleTransactionWriteRepository.Update(existingTransaction);
                await _vehicleTransactionWriteRepository.SaveAsync();

                // Yeni i�lem olu�tur
                var newTransaction = _mapper.Map<VehicleTransaction>(request);

                await _vehicleTransactionWriteRepository.AddAsync(newTransaction);

                // De�i�iklikleri kaydet
                await _vehicleTransactionWriteRepository.SaveAsync();

                await _unitOfWork.CommitAsync();

                return newTransaction;







                // Transaction'� tamamla
                //TODO Hata veriyor
            }
            catch (Exception ex)
            {
                // Hata durumunda rollback
                await _unitOfWork.RollbackAsync();

                throw new BusinessException("Vehicle transaction i�lemi s�ras�nda bir hata olu�tu", ex);
            }

        }

    }
}
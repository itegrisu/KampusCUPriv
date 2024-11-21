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
using Application.Abstractions.UnitOfWork;
using Core.CrossCuttingConcern.Exceptions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Application.Features.VehicleManagementFeatures.VehicleTransactions.Commands.Create;

public class CreateVehicleTransactionCommand : IRequest<CreatedVehicleTransactionResponse>
{
    public Guid GidVehicleFK { get; set; }
    public Guid? GidSupplierCustomerFK { get; set; }
    public Guid? GidVehicleUsePersonnelFK { get; set; }
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
            var savedTransaction = await _vehicleTransactionReadRepository.GetAsync(
                predicate: x => x.Gid == transaction.Gid,
                include: x => x.Include(t => t.SCCompanyFK).Include(t => t.UserFK).Include(t => t.VehicleAllFK));

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
            switch (request.VehicleStatus)
            {
                case EnumVehicleStatus.FirmaAraci:
                    await HandleFirmaAraciTransaction(existingTransaction, request);
                    break;

                case EnumVehicleStatus.KiralikVerilenArac:
                    await HandleKiralikVerilenAracTransaction(existingTransaction, request);
                    break;

                case EnumVehicleStatus.KiralikAlinanArac:
                    await HandleKiralikAlinanAracTransaction(existingTransaction, request);
                    break;

                case EnumVehicleStatus.SatilanArac:
                    await HandleSatilanAracTransaction(existingTransaction, request);
                    break;

                case EnumVehicleStatus.TahsisArac:
                    await HandleTahsisAracTransaction(existingTransaction, request);
                    break;

                default:
                    throw new Exception("Bilinmeyen ara� durumu!");
            }

            return await CreateResponse(existingTransaction);
        }

        private async Task HandleFirmaAraciTransaction(VehicleTransaction existingTransaction, CreateVehicleTransactionCommand request)
        {
            if (existingTransaction.VehicleStatus == EnumVehicleStatus.FirmaAraci)
                throw new Exception("Bu ara� zaten firma arac� durumundad�r.");

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.KiralikVerilenArac)
                ArchiveAndCreateNewTransaction(existingTransaction, request);

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac)
                ArchiveAndCreateNewTransaction(existingTransaction, request);

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.SatilanArac)
                ArchiveAndCreateNewTransaction(existingTransaction, request);

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.TahsisArac)
            {
                await _vehicleTransactionBusinessRules.IsCompanyEmployee(existingTransaction.GidVehicleUsePersonnelFK);
                ArchiveAndCreateNewTransaction(existingTransaction, request);
            }
        }

        private async Task HandleKiralikVerilenAracTransaction(VehicleTransaction existingTransaction, CreateVehicleTransactionCommand request)
        {
            if (existingTransaction.VehicleStatus == EnumVehicleStatus.FirmaAraci)
                ArchiveAndCreateNewTransaction(existingTransaction, request);

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.KiralikVerilenArac)
                throw new Exception("Bu ara� zaten kiral�k verilen ara� durumundad�r.");

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac)
                throw new Exception("Bu ara� kiral�k al�nan ara� olarak eklenemez.");

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.SatilanArac)
                throw new Exception("Bu ara� sat�lan ara� olarak eklenemez.");

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.TahsisArac)
            {
                await _vehicleTransactionBusinessRules.IsCompanyEmployee(existingTransaction.GidVehicleUsePersonnelFK);
                ArchiveAndCreateNewTransaction(existingTransaction, request);
            }
        }

        private async Task HandleKiralikAlinanAracTransaction(VehicleTransaction existingTransaction, CreateVehicleTransactionCommand request)
        {
            if (existingTransaction.VehicleStatus == EnumVehicleStatus.FirmaAraci)
                throw new Exception("Bu ara� firma arac� olarak eklenemez.");

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.KiralikVerilenArac)
                throw new Exception("Bu ara� firma arac� olarak eklenemez.");

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac)
                throw new Exception("Bu ara� zaten kiral�k al�nan ara� olarak eklenmi�.");

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.SatilanArac)
                ArchiveAndCreateNewTransaction(existingTransaction, request);

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.TahsisArac)
                throw new Exception("Bu ara� tahsis ara� olarak eklenemez.");
        }

        private async Task HandleSatilanAracTransaction(VehicleTransaction existingTransaction, CreateVehicleTransactionCommand request)
        {
            if (existingTransaction.VehicleStatus == EnumVehicleStatus.FirmaAraci)
                ArchiveAndCreateNewTransaction(existingTransaction, request);

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.KiralikVerilenArac)
                ArchiveAndCreateNewTransaction(existingTransaction, request);

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac)
                throw new Exception("Bu ara� kiral�k al�nan ara� olarak eklenemez.");

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.SatilanArac)
                throw new Exception("Bu ara� zaten sat�lan ara� olarak eklenmi�.");

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.TahsisArac)
            {
                await _vehicleTransactionBusinessRules.IsCompanyEmployee(existingTransaction.GidVehicleUsePersonnelFK);
                ArchiveAndCreateNewTransaction(existingTransaction, request);
            }
        }

        private async Task HandleTahsisAracTransaction(VehicleTransaction existingTransaction, CreateVehicleTransactionCommand request)
        {
            if (existingTransaction.VehicleStatus == EnumVehicleStatus.FirmaAraci)
                ArchiveAndCreateNewTransaction(existingTransaction, request);

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.KiralikVerilenArac)
                ArchiveAndCreateNewTransaction(existingTransaction, request);

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac)
                ArchiveAndCreateNewTransaction(existingTransaction, request);

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.SatilanArac)
                throw new Exception("Bu ara� sat�lan ara� olarak eklenemez.");

            if (existingTransaction.VehicleStatus == EnumVehicleStatus.TahsisArac)
                throw new Exception("Bu ara� zaten tahsis arac� olarak eklenmi�.");
        }

        private async Task ArchiveAndCreateNewTransaction(VehicleTransaction existingTransaction, CreateVehicleTransactionCommand request)
        {
            _unitOfWork.BeginTransaction(); // Transaction ba�lat�l�r
            try
            {
                // Mevcut i�lemi ar�ivle
                existingTransaction.DataState = DataState.Archive;
                existingTransaction.EndDate = DateTime.Now;
                existingTransaction.EndKM = request.StartKM;
                _vehicleTransactionWriteRepository.Update(existingTransaction);
                await _unitOfWork.SaveChangesAsync();

                // Yeni i�lem olu�tur
                var newTransaction = _mapper.Map<VehicleTransaction>(request);

                await _vehicleTransactionWriteRepository.AddAsync(newTransaction);

                // De�i�iklikleri kaydet
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();

                // Transaction'� tamamla
                //TODO Hata veriyor
            }
            catch (Exception ex)
            {
                // Hata durumunda rollback
                await _unitOfWork.RollbackAsync();

                throw new BusinessException("Vehicle transaction i�lemi s�ras�nda bir hata olu�tu", ex);
            }
            finally
            {
                _unitOfWork.Dispose();
            }

        }

    }
}
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.VehicleManagementsRepos.VehicleTransactionRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Enums;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleTransactions.Rules;

public class VehicleTransactionBusinessRules : BaseBusinessRules
{
    private readonly IVehicleTransactionReadRepository _vehicleTransactionReadRepository;
    private readonly IUserReadRepository _userReadRepository;
    public VehicleTransactionBusinessRules(IVehicleTransactionReadRepository vehicleTransactionReadRepository, IUserReadRepository userReadRepository)
    {
        _vehicleTransactionReadRepository = vehicleTransactionReadRepository;
        _userReadRepository = userReadRepository;
    }

    public async Task VehicleTransactionShouldExistWhenSelected(X.VehicleTransaction? item)
    {
        if (item == null)
            throw new BusinessException(VehicleTransactionsBusinessMessages.VehicleTransactionNotExists);
    }

    public async Task VehicleAllReadyExist(Guid gidVehicleAllFK)
    {
        var existingTransaction = await _vehicleTransactionReadRepository.GetSingleAsync(x => x.GidVehicleFK == gidVehicleAllFK);
        if (existingTransaction != null)
        {
            throw new BusinessException(VehicleTransactionsBusinessMessages.VehicleTransactionAlreadyExists);
        }
    }

    public async Task IsSuitableForHireVehicle(Guid gidVehicleAllFK)
    {
        var existingTransaction = await _vehicleTransactionReadRepository.GetSingleAsync(x => x.GidVehicleFK == gidVehicleAllFK && x.VehicleStatus == EnumVehicleStatus.FirmaAraci);
        if (existingTransaction == null)
        {
            throw new BusinessException(VehicleTransactionsBusinessMessages.VehicleIsNotCompanyVehicleToHire);
        }
    }
    public async Task IsSuitableForTakeVehicle(Guid gidVehicleAllFK)
    {
        var existingTransaction = await _vehicleTransactionReadRepository.GetSingleAsync(x => x.GidVehicleFK == gidVehicleAllFK);
        if (existingTransaction == null)
        {
            throw new BusinessException(VehicleTransactionsBusinessMessages.VehicleNotSuitablForTake);
        }
    }

    public async Task IsSuitableForSaleVehicle(Guid gidVehicleAllFK)
    {
        var existingTransaction = await _vehicleTransactionReadRepository.GetSingleAsync(x => x.GidVehicleFK == gidVehicleAllFK && x.VehicleStatus == EnumVehicleStatus.FirmaAraci);
        if (existingTransaction == null)
        {
            throw new BusinessException(VehicleTransactionsBusinessMessages.VehicleIsNotCompanyVehicleToSale);
        }
    }

    public async Task IsSuitableForAllocated(Guid gidVehicleAllFK)
    {
        var existingTransaction = await _vehicleTransactionReadRepository.GetSingleAsync(x => x.GidVehicleFK == gidVehicleAllFK && (x.VehicleStatus == EnumVehicleStatus.FirmaAraci || x.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac));
        if (existingTransaction == null)
        {
            throw new BusinessException(VehicleTransactionsBusinessMessages.VehicleIsNotCompanyVehicleToAllocate);
        }
    }

    public async Task IsCompanyEmployee(Guid? gidUserFK)
    {
        var user = await _userReadRepository.GetSingleAsync(x => x.Gid == gidUserFK && x.WorkType == EnumWorkType.FirmaCalisani);
        if (user == null)
        {
            throw new BusinessException(VehicleTransactionsBusinessMessages.UserNotExists);
        }
    }

}

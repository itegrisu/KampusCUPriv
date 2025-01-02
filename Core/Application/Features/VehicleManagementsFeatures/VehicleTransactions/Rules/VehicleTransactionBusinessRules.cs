using Application.Features.VehicleManagementFeatures.VehicleTransactions.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.VehicleManagementsRepos.VehicleTransactionRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Enums;
using Org.BouncyCastle.Ocsp;
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
    public async Task IsCompanyEmployee(Guid? gidUserFK)
    {
        var user = await _userReadRepository.GetSingleAsync(x => x.Gid == gidUserFK && x.WorkType == EnumWorkType.FirmaCalisani);
        if (user == null)
        {
            throw new BusinessException(VehicleTransactionsBusinessMessages.UserNotExists);
        }
    }

    public async Task CheckKM(int? oldKM, int startKM)
    {
        if (oldKM > startKM)
        {
            throw new BusinessException(VehicleTransactionsBusinessMessages.NewKMShouldBeMoreThanOldKM);
        }
    }
}

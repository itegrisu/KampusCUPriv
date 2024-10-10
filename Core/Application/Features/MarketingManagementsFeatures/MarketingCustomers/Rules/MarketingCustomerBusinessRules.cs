using Application.Features.MarketingManagementFeatures.MarketingCustomers.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.MarketingManagements;

namespace Application.Features.MarketingManagementFeatures.MarketingCustomers.Rules;

public class MarketingCustomerBusinessRules : BaseBusinessRules
{
    public async Task MarketingCustomerShouldExistWhenSelected(X.MarketingCustomer? item)
    {
        if (item == null)
            throw new BusinessException(MarketingCustomersBusinessMessages.MarketingCustomerNotExists);
    }
}
using Application.Features.VehicleManagementFeatures.VehicleDocuments.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleDocuments.Rules;

public class VehicleDocumentBusinessRules : BaseBusinessRules
{
    public async Task VehicleDocumentShouldExistWhenSelected(X.VehicleDocument? item)
    {
        if (item == null)
            throw new BusinessException(VehicleDocumentsBusinessMessages.VehicleDocumentNotExists);
    }
}
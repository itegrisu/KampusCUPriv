using Application.Features.DefinitionManagementFeatures.DocumentTypes.Constants;
using Application.Repositories.DefinitionManagementRepos.DocumentTypeRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.DocumentTypes.Rules;

public class DocumentTypeBusinessRules : BaseBusinessRules
{
    public string BelgeAdi { get; set; } = string.Empty;

    private readonly IDocumentTypeReadRepository _documentTypeReadRepository;
    public async Task DocumentTypeShouldExistWhenSelected(X.DocumentType? item)
    {
        if (item == null)
            throw new BusinessException(DocumentTypesBusinessMessages.DocumentTypeNotExists);
    }

    public async Task DocumentNameIsUnique(string documentName, Guid? documentGid = null)
    {
        var documentType = await _documentTypeReadRepository.GetAsync(
                       predicate: x => x.Name == documentName && (documentGid == null || x.Gid != documentGid));

        if (documentType != null)
        {
            throw new BusinessException(DocumentTypesBusinessMessages.DocumentTypeAlreadyExists);
        }
    }



}
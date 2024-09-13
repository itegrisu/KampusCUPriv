using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Constants;
using Application.Repositories.DefinitionManagementRepos.DocumentTypeRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Rules;

public class PersonnelDocumentBusinessRules : BaseBusinessRules
{
    public Guid GidPersonelFK { get; set; }
    public Guid GidBelgeTuru { get; set; }
    private readonly IUserReadRepository _userReadRepository;
    private readonly IDocumentTypeReadRepository _documentTypeReadRepository;

    public PersonnelDocumentBusinessRules(IUserReadRepository userReadRepository, IDocumentTypeReadRepository documentTypeReadRepository)
    {
        _userReadRepository = userReadRepository;
        _documentTypeReadRepository = documentTypeReadRepository;
    }

    public async Task PersonnelDocumentShouldExistWhenSelected(X.PersonnelDocument? item)
    {
        if (item == null)
            throw new BusinessException(PersonnelDocumentsBusinessMessages.PersonnelDocumentNotExists);
    }

    public async Task UserShouldExistWhenSelected(Guid gid)
    {
        var user = await _userReadRepository.GetAsync(predicate: x => x.Gid == gid);
        if (user == null)
            throw new BusinessException(PersonnelDocumentsBusinessMessages.UserNotExists);
    }

    public async Task DocumentTypeShouldExistWhenSelected(Guid gid)
    {
        var documentType = await _documentTypeReadRepository.GetAsync(predicate: x => x.Gid == gid);
        if (documentType == null)
            throw new BusinessException(PersonnelDocumentsBusinessMessages.DocumentTypeNotExists);
    }

}
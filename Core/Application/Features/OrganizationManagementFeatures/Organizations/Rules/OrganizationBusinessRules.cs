using Application.Features.OrganizationManagementFeatures.Organizations.Constants;
using Application.Repositories.DefinitionManagementRepos.OrganizationTypeRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.OrganizationManagementRepos.OrganizationRepo;
using Application.Repositories.SupplierManagementRepos.SCCompanyRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.GeneralManagements;
using Domain.Entities.OrganizationManagements;
using Domain.Entities.SupplierCustomerManagements;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.Organizations.Rules;

public class OrganizationBusinessRules : BaseBusinessRules
{
    //public Guid GidCustomerFK { get; set; }should be exist
    //public Guid GidResponsibleUserFK { get; set; }should be exist
    //public Guid GidOrganizationTypeFK { get; set; } should be exist

    //public string OrganizationName { get; set; }  should be unique

    private readonly ISCCompanyReadRepository _sCCompanyReadRepository;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IOrganizationTypeReadRepository _organizationTypeReadRepository;
    private readonly IOrganizationReadRepository _organizationReadRepository;

    public OrganizationBusinessRules(ISCCompanyReadRepository sCCompanyReadRepository, IUserReadRepository userReadRepository, IOrganizationTypeReadRepository organizationTypeReadRepository, IOrganizationReadRepository organizationReadRepository)
    {
        _sCCompanyReadRepository = sCCompanyReadRepository;
        _userReadRepository = userReadRepository;
        _organizationTypeReadRepository = organizationTypeReadRepository;
        _organizationReadRepository = organizationReadRepository;
    }

    public async Task OrganizationShouldExistWhenSelected(X.Organization? item)
    {
        if (item == null)
            throw new BusinessException(OrganizationsBusinessMessages.OrganizationNotExists);
    }

    public async Task CustomerShouldExistWhenSelected(Guid gidCustomerFK)
    {
        SCCompany sCCompany = await _sCCompanyReadRepository.GetAsync(predicate: x => x.Gid == gidCustomerFK);
        if (sCCompany == null)
            throw new BusinessException(OrganizationsBusinessMessages.CustomerNotExists);
    }

    public async Task ResponsibleUserShouldExistWhenSelected(Guid gidResponsibleUserFK)
    {
        User user = await _userReadRepository.GetAsync(predicate: x => x.Gid == gidResponsibleUserFK);
        if (user == null)
            throw new BusinessException(OrganizationsBusinessMessages.ResponsibleUserNotExists);
    }

    public async Task OrganizationTypeShouldExistWhenSelected(Guid gidOrganizationTypeFK)
    {
        OrganizationType organizationType = await _organizationTypeReadRepository.GetAsync(predicate: x => x.Gid == gidOrganizationTypeFK);
        if (organizationType == null)
            throw new BusinessException(OrganizationsBusinessMessages.OrganizationTypeNotExists);
    }

    public async Task OrganizationNameShouldBeUnique(string organizationName, Guid? Gid = null)
    {
        Organization organization = await _organizationReadRepository.GetAsync(predicate: x => x.OrganizationName == organizationName && x.Gid != Gid);
        if (organization != null)
            throw new BusinessException(OrganizationsBusinessMessages.OrganizationNameShouldBeUnique);
    }



}
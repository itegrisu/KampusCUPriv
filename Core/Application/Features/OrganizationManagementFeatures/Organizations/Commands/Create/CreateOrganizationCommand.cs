using Application.Features.OrganizationManagementFeatures.Organizations.Constants;
using Application.Features.OrganizationManagementFeatures.Organizations.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.Organizations.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.Organizations.Commands.Create;

public class CreateOrganizationCommand : IRequest<CreatedOrganizationResponse>
{
    public Guid GidCustomerFK { get; set; }
    public Guid GidResponsibleUserFK { get; set; }
    public Guid GidOrganizationTypeFK { get; set; }

    public string OrganizationName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public EnumOrganizationStatus OrganizationStatus { get; set; }
    public string? Description { get; set; }



    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, CreatedOrganizationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationWriteRepository _organizationWriteRepository;
        private readonly IOrganizationReadRepository _organizationReadRepository;
        private readonly OrganizationBusinessRules _organizationBusinessRules;

        public CreateOrganizationCommandHandler(IMapper mapper, IOrganizationWriteRepository organizationWriteRepository,
                                         OrganizationBusinessRules organizationBusinessRules, IOrganizationReadRepository organizationReadRepository)
        {
            _mapper = mapper;
            _organizationWriteRepository = organizationWriteRepository;
            _organizationBusinessRules = organizationBusinessRules;
            _organizationReadRepository = organizationReadRepository;
        }

        public async Task<CreatedOrganizationResponse> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
        {


            await _organizationBusinessRules.CustomerShouldExistWhenSelected(request.GidCustomerFK);
            await _organizationBusinessRules.ResponsibleUserShouldExistWhenSelected(request.GidResponsibleUserFK);
            await _organizationBusinessRules.OrganizationTypeShouldExistWhenSelected(request.GidOrganizationTypeFK);
            await _organizationBusinessRules.OrganizationNameShouldBeUnique(request.OrganizationName);


            X.Organization organization = _mapper.Map<X.Organization>(request);


            await _organizationWriteRepository.AddAsync(organization);
            await _organizationWriteRepository.SaveAsync();

            X.Organization savedOrganization = await _organizationReadRepository.GetAsync(predicate: x => x.Gid == organization.Gid, include: x => x.Include(x => x.SCCompanyFK).Include(x => x.OrganizationTypeFK).Include(x => x.ResponsibleUserFK));

            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidOrganizationResponse obj = _mapper.Map<GetByGidOrganizationResponse>(savedOrganization);
            return new()
            {
                Title = OrganizationsBusinessMessages.ProcessCompleted,
                Message = OrganizationsBusinessMessages.SuccessCreatedOrganizationMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
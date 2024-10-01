using Application.Features.OrganizationManagementFeatures.Organizations.Constants;
using Application.Features.OrganizationManagementFeatures.Organizations.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.Organizations.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.Organizations.Commands.Update;

public class UpdateOrganizationCommand : IRequest<UpdatedOrganizationResponse>
{
    public Guid Gid { get; set; }

    public Guid GidCustomerFK { get; set; }
    public Guid GidResponsibleUserFK { get; set; }
    public Guid GidOrganizationTypeFK { get; set; }

    public string OrganizationName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public EnumOrganizationStatus OrganizationStatus { get; set; }
    public string? Description { get; set; }



    public class UpdateOrganizationCommandHandler : IRequestHandler<UpdateOrganizationCommand, UpdatedOrganizationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationWriteRepository _organizationWriteRepository;
        private readonly IOrganizationReadRepository _organizationReadRepository;
        private readonly OrganizationBusinessRules _organizationBusinessRules;

        public UpdateOrganizationCommandHandler(IMapper mapper, IOrganizationWriteRepository organizationWriteRepository,
                                         OrganizationBusinessRules organizationBusinessRules, IOrganizationReadRepository organizationReadRepository)
        {
            _mapper = mapper;
            _organizationWriteRepository = organizationWriteRepository;
            _organizationBusinessRules = organizationBusinessRules;
            _organizationReadRepository = organizationReadRepository;
        }

        public async Task<UpdatedOrganizationResponse> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
        {
            X.Organization? organization = await _organizationReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek

            await _organizationBusinessRules.OrganizationShouldExistWhenSelected(organization);
            await _organizationBusinessRules.CustomerShouldExistWhenSelected(request.GidCustomerFK);
            await _organizationBusinessRules.ResponsibleUserShouldExistWhenSelected(request.GidResponsibleUserFK);
            await _organizationBusinessRules.OrganizationTypeShouldExistWhenSelected(request.GidOrganizationTypeFK);
            await _organizationBusinessRules.OrganizationNameShouldBeUnique(request.OrganizationName, request.Gid);

            organization = _mapper.Map(request, organization);

            _organizationWriteRepository.Update(organization!);
            await _organizationWriteRepository.SaveAsync();

            X.Organization updatedOrganization = await _organizationReadRepository.GetAsync(predicate: x => x.Gid == organization.Gid, include: x => x.Include(x => x.SCCompanyFK).Include(x => x.OrganizationTypeFK).Include(x => x.ResponsibleUserFK));

            GetByGidOrganizationResponse obj = _mapper.Map<GetByGidOrganizationResponse>(updatedOrganization);

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
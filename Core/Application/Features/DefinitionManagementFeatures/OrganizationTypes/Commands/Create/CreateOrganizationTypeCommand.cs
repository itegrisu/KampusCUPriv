using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Constants;
using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.OrganizationTypeRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.OrganizationTypes.Commands.Create;

public class CreateOrganizationTypeCommand : IRequest<CreatedOrganizationTypeResponse>
{

    public string Name { get; set; }



    public class CreateOrganizationTypeCommandHandler : IRequestHandler<CreateOrganizationTypeCommand, CreatedOrganizationTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationTypeWriteRepository _organizationTypeWriteRepository;
        private readonly IOrganizationTypeReadRepository _organizationTypeReadRepository;
        private readonly OrganizationTypeBusinessRules _organizationTypeBusinessRules;

        public CreateOrganizationTypeCommandHandler(IMapper mapper, IOrganizationTypeWriteRepository organizationTypeWriteRepository,
                                         OrganizationTypeBusinessRules organizationTypeBusinessRules, IOrganizationTypeReadRepository organizationTypeReadRepository)
        {
            _mapper = mapper;
            _organizationTypeWriteRepository = organizationTypeWriteRepository;
            _organizationTypeBusinessRules = organizationTypeBusinessRules;
            _organizationTypeReadRepository = organizationTypeReadRepository;
        }

        public async Task<CreatedOrganizationTypeResponse> Handle(CreateOrganizationTypeCommand request, CancellationToken cancellationToken)
        {
            await _organizationTypeBusinessRules.OrganizationNameShouldBeUnique(request.Name);

            X.OrganizationType organizationType = _mapper.Map<X.OrganizationType>(request);


            await _organizationTypeWriteRepository.AddAsync(organizationType);
            await _organizationTypeWriteRepository.SaveAsync();

            X.OrganizationType savedOrganizationType = await _organizationTypeReadRepository.GetAsync(predicate: x => x.Gid == organizationType.Gid);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidOrganizationTypeResponse obj = _mapper.Map<GetByGidOrganizationTypeResponse>(savedOrganizationType);
            return new()
            {
                Title = OrganizationTypesBusinessMessages.ProcessCompleted,
                Message = OrganizationTypesBusinessMessages.SuccessCreatedOrganizationTypeMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
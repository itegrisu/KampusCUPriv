using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Constants;
using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.OrganizationTypeRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.OrganizationTypes.Commands.Update;

public class UpdateOrganizationTypeCommand : IRequest<UpdatedOrganizationTypeResponse>
{
    public Guid Gid { get; set; }


    public string Name { get; set; }



    public class UpdateOrganizationTypeCommandHandler : IRequestHandler<UpdateOrganizationTypeCommand, UpdatedOrganizationTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationTypeWriteRepository _organizationTypeWriteRepository;
        private readonly IOrganizationTypeReadRepository _organizationTypeReadRepository;
        private readonly OrganizationTypeBusinessRules _organizationTypeBusinessRules;

        public UpdateOrganizationTypeCommandHandler(IMapper mapper, IOrganizationTypeWriteRepository organizationTypeWriteRepository,
                                         OrganizationTypeBusinessRules organizationTypeBusinessRules, IOrganizationTypeReadRepository organizationTypeReadRepository)
        {
            _mapper = mapper;
            _organizationTypeWriteRepository = organizationTypeWriteRepository;
            _organizationTypeBusinessRules = organizationTypeBusinessRules;
            _organizationTypeReadRepository = organizationTypeReadRepository;
        }

        public async Task<UpdatedOrganizationTypeResponse> Handle(UpdateOrganizationTypeCommand request, CancellationToken cancellationToken)
        {
            X.OrganizationType? organizationType = await _organizationTypeReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _organizationTypeBusinessRules.OrganizationTypeShouldExistWhenSelected(organizationType);
            await _organizationTypeBusinessRules.OrganizationNameShouldBeUnique(request.Name, request.Gid);
            organizationType = _mapper.Map(request, organizationType);

            _organizationTypeWriteRepository.Update(organizationType!);
            await _organizationTypeWriteRepository.SaveAsync();
            GetByGidOrganizationTypeResponse obj = _mapper.Map<GetByGidOrganizationTypeResponse>(organizationType);

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
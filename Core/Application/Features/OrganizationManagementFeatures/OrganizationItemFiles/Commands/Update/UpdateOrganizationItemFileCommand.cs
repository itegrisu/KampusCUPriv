using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Constants;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationItemFileRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.Update;

public class UpdateOrganizationItemFileCommand : IRequest<UpdatedOrganizationItemFileResponse>
{
    public Guid Gid { get; set; }

    public Guid GidOrganizationItemFK { get; set; }

    public string Title { get; set; }
    public string? Description { get; set; }




    public class UpdateOrganizationItemFileCommandHandler : IRequestHandler<UpdateOrganizationItemFileCommand, UpdatedOrganizationItemFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationItemFileWriteRepository _organizationItemFileWriteRepository;
        private readonly IOrganizationItemFileReadRepository _organizationItemFileReadRepository;
        private readonly OrganizationItemFileBusinessRules _organizationItemFileBusinessRules;

        public UpdateOrganizationItemFileCommandHandler(IMapper mapper, IOrganizationItemFileWriteRepository organizationItemFileWriteRepository,
                                         OrganizationItemFileBusinessRules organizationItemFileBusinessRules, IOrganizationItemFileReadRepository organizationItemFileReadRepository)
        {
            _mapper = mapper;
            _organizationItemFileWriteRepository = organizationItemFileWriteRepository;
            _organizationItemFileBusinessRules = organizationItemFileBusinessRules;
            _organizationItemFileReadRepository = organizationItemFileReadRepository;
        }

        public async Task<UpdatedOrganizationItemFileResponse> Handle(UpdateOrganizationItemFileCommand request, CancellationToken cancellationToken)
        {
            X.OrganizationItemFile? organizationItemFile = await _organizationItemFileReadRepository.GetAsync(predicate: x => x.Gid == request.Gid,
                include: x => x.Include(x => x.OrganizationItemFK), cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _organizationItemFileBusinessRules.OrganizationItemFileShouldExistWhenSelected(organizationItemFile);
            organizationItemFile = _mapper.Map(request, organizationItemFile);

            _organizationItemFileWriteRepository.Update(organizationItemFile!);
            await _organizationItemFileWriteRepository.SaveAsync();
            GetByGidOrganizationItemFileResponse obj = _mapper.Map<GetByGidOrganizationItemFileResponse>(organizationItemFile);

            return new()
            {
                Title = OrganizationItemFilesBusinessMessages.ProcessCompleted,
                Message = OrganizationItemFilesBusinessMessages.SuccessCreatedOrganizationItemFileMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
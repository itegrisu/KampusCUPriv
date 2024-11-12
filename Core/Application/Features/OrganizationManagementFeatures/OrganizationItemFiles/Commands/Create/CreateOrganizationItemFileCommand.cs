using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Constants;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationItemFileRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.Create;

public class CreateOrganizationItemFileCommand : IRequest<CreatedOrganizationItemFileResponse>
{
    public Guid GidOrganizationItemFK { get; set; }

    public string Title { get; set; }
    public string? Description { get; set; }



    public class CreateOrganizationItemFileCommandHandler : IRequestHandler<CreateOrganizationItemFileCommand, CreatedOrganizationItemFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationItemFileWriteRepository _organizationItemFileWriteRepository;
        private readonly IOrganizationItemFileReadRepository _organizationItemFileReadRepository;
        private readonly OrganizationItemFileBusinessRules _organizationItemFileBusinessRules;

        public CreateOrganizationItemFileCommandHandler(IMapper mapper, IOrganizationItemFileWriteRepository organizationItemFileWriteRepository,
                                         OrganizationItemFileBusinessRules organizationItemFileBusinessRules, IOrganizationItemFileReadRepository organizationItemFileReadRepository)
        {
            _mapper = mapper;
            _organizationItemFileWriteRepository = organizationItemFileWriteRepository;
            _organizationItemFileBusinessRules = organizationItemFileBusinessRules;
            _organizationItemFileReadRepository = organizationItemFileReadRepository;
        }

        public async Task<CreatedOrganizationItemFileResponse> Handle(CreateOrganizationItemFileCommand request, CancellationToken cancellationToken)
        {

            var items = await _organizationItemFileReadRepository.GetAll().ToListAsync();
            int maxRowNo = items.Any() ? items.Max(r => r.RowNo) : 0;

            X.OrganizationItemFile organizationItemFile = _mapper.Map<X.OrganizationItemFile>(request);
            organizationItemFile.RowNo = maxRowNo + 1;

            await _organizationItemFileWriteRepository.AddAsync(organizationItemFile);
            await _organizationItemFileWriteRepository.SaveAsync();

            X.OrganizationItemFile savedOrganizationItemFile = await _organizationItemFileReadRepository.GetAsync(predicate: x => x.Gid == organizationItemFile.Gid,
            include: x => x.Include(x => x.OrganizationItemFK));

            GetByGidOrganizationItemFileResponse obj = _mapper.Map<GetByGidOrganizationItemFileResponse>(savedOrganizationItemFile);
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
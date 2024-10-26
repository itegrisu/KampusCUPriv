using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Constants;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationFileRepo;
using AutoMapper;
using X = Domain.Entities.OrganizationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.Update;

public class UpdateOrganizationFileCommand : IRequest<UpdatedOrganizationFileResponse>
{
    public Guid Gid { get; set; }

	public Guid GidOrganizationFK { get; set; }

public string Title { get; set; }
public string? Document { get; set; }
public string? Description { get; set; }
public int RowNo { get; set; }



    public class UpdateOrganizationFileCommandHandler : IRequestHandler<UpdateOrganizationFileCommand, UpdatedOrganizationFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationFileWriteRepository _organizationFileWriteRepository;
        private readonly IOrganizationFileReadRepository _organizationFileReadRepository;
        private readonly OrganizationFileBusinessRules _organizationFileBusinessRules;

        public UpdateOrganizationFileCommandHandler(IMapper mapper, IOrganizationFileWriteRepository organizationFileWriteRepository,
                                         OrganizationFileBusinessRules organizationFileBusinessRules, IOrganizationFileReadRepository organizationFileReadRepository)
        {
            _mapper = mapper;
            _organizationFileWriteRepository = organizationFileWriteRepository;
            _organizationFileBusinessRules = organizationFileBusinessRules;
            _organizationFileReadRepository = organizationFileReadRepository;
        }

        public async Task<UpdatedOrganizationFileResponse> Handle(UpdateOrganizationFileCommand request, CancellationToken cancellationToken)
        {
            X.OrganizationFile? organizationFile = await _organizationFileReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.OrganizationFK));
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _organizationFileBusinessRules.OrganizationFileShouldExistWhenSelected(organizationFile);
            organizationFile = _mapper.Map(request, organizationFile);

            _organizationFileWriteRepository.Update(organizationFile!);
            await _organizationFileWriteRepository.SaveAsync();
            GetByGidOrganizationFileResponse obj = _mapper.Map<GetByGidOrganizationFileResponse>(organizationFile);

            return new()
            {
                Title = OrganizationFilesBusinessMessages.ProcessCompleted,
                Message = OrganizationFilesBusinessMessages.SuccessCreatedOrganizationFileMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
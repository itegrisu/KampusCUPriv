using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Constants;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationFileRepo;
using AutoMapper;
using X = Domain.Entities.OrganizationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.Create;

public class CreateOrganizationFileCommand : IRequest<CreatedOrganizationFileResponse>
{
    public Guid GidOrganizationFK { get; set; }

    public string Title { get; set; }
    public string? Document { get; set; }
    public string? Description { get; set; }
    public int RowNo { get; set; }



    public class CreateOrganizationFileCommandHandler : IRequestHandler<CreateOrganizationFileCommand, CreatedOrganizationFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationFileWriteRepository _organizationFileWriteRepository;
        private readonly IOrganizationFileReadRepository _organizationFileReadRepository;
        private readonly OrganizationFileBusinessRules _organizationFileBusinessRules;

        public CreateOrganizationFileCommandHandler(IMapper mapper, IOrganizationFileWriteRepository organizationFileWriteRepository,
                                         OrganizationFileBusinessRules organizationFileBusinessRules, IOrganizationFileReadRepository organizationFileReadRepository)
        {
            _mapper = mapper;
            _organizationFileWriteRepository = organizationFileWriteRepository;
            _organizationFileBusinessRules = organizationFileBusinessRules;
            _organizationFileReadRepository = organizationFileReadRepository;
        }

        public async Task<CreatedOrganizationFileResponse> Handle(CreateOrganizationFileCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _organizationFileReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.OrganizationFile organizationFile = _mapper.Map<X.OrganizationFile>(request);
            //organizationFile.RowNo = maxRowNo + 1;

            await _organizationFileWriteRepository.AddAsync(organizationFile);
            await _organizationFileWriteRepository.SaveAsync();

            X.OrganizationFile savedOrganizationFile = await _organizationFileReadRepository.GetAsync(predicate: x => x.Gid == organizationFile.Gid, include: x => x.Include(x => x.OrganizationFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidOrganizationFileResponse obj = _mapper.Map<GetByGidOrganizationFileResponse>(savedOrganizationFile);
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
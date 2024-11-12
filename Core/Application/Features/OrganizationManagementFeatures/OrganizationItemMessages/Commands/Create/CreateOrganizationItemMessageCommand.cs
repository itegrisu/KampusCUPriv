using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Constants;
using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationItemMessageRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Commands.Create;

public class CreateOrganizationItemMessageCommand : IRequest<CreatedOrganizationItemMessageResponse>
{
    public Guid GidOrganizationItemFK { get; set; }
    public Guid GidSendMessageUserFK { get; set; }

    public string Message { get; set; }



    public class CreateOrganizationItemMessageCommandHandler : IRequestHandler<CreateOrganizationItemMessageCommand, CreatedOrganizationItemMessageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationItemMessageWriteRepository _organizationItemMessageWriteRepository;
        private readonly IOrganizationItemMessageReadRepository _organizationItemMessageReadRepository;
        private readonly OrganizationItemMessageBusinessRules _organizationItemMessageBusinessRules;

        public CreateOrganizationItemMessageCommandHandler(IMapper mapper, IOrganizationItemMessageWriteRepository organizationItemMessageWriteRepository,
                                         OrganizationItemMessageBusinessRules organizationItemMessageBusinessRules, IOrganizationItemMessageReadRepository organizationItemMessageReadRepository)
        {
            _mapper = mapper;
            _organizationItemMessageWriteRepository = organizationItemMessageWriteRepository;
            _organizationItemMessageBusinessRules = organizationItemMessageBusinessRules;
            _organizationItemMessageReadRepository = organizationItemMessageReadRepository;
        }

        public async Task<CreatedOrganizationItemMessageResponse> Handle(CreateOrganizationItemMessageCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _organizationItemMessageReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.OrganizationItemMessage organizationItemMessage = _mapper.Map<X.OrganizationItemMessage>(request);
            //organizationItemMessage.RowNo = maxRowNo + 1;

            await _organizationItemMessageWriteRepository.AddAsync(organizationItemMessage);
            await _organizationItemMessageWriteRepository.SaveAsync();

            X.OrganizationItemMessage savedOrganizationItemMessage = await _organizationItemMessageReadRepository.GetAsync(predicate: x => x.Gid == organizationItemMessage.Gid,
                include: i => i.Include(i => i.UserFK).Include(i => i.OrganizationItemFK));

            GetByGidOrganizationItemMessageResponse obj = _mapper.Map<GetByGidOrganizationItemMessageResponse>(savedOrganizationItemMessage);
            return new()
            {
                Title = OrganizationItemMessagesBusinessMessages.ProcessCompleted,
                Message = OrganizationItemMessagesBusinessMessages.SuccessCreatedOrganizationItemMessageMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
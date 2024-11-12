using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Constants;
using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationItemMessageRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Commands.Update;

public class UpdateOrganizationItemMessageCommand : IRequest<UpdatedOrganizationItemMessageResponse>
{
    public Guid Gid { get; set; }

    public Guid GidOrganizationItemFK { get; set; }
    public Guid GidSendMessageUserFK { get; set; }

    public string Message { get; set; }



    public class UpdateOrganizationItemMessageCommandHandler : IRequestHandler<UpdateOrganizationItemMessageCommand, UpdatedOrganizationItemMessageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationItemMessageWriteRepository _organizationItemMessageWriteRepository;
        private readonly IOrganizationItemMessageReadRepository _organizationItemMessageReadRepository;
        private readonly OrganizationItemMessageBusinessRules _organizationItemMessageBusinessRules;

        public UpdateOrganizationItemMessageCommandHandler(IMapper mapper, IOrganizationItemMessageWriteRepository organizationItemMessageWriteRepository,
                                         OrganizationItemMessageBusinessRules organizationItemMessageBusinessRules, IOrganizationItemMessageReadRepository organizationItemMessageReadRepository)
        {
            _mapper = mapper;
            _organizationItemMessageWriteRepository = organizationItemMessageWriteRepository;
            _organizationItemMessageBusinessRules = organizationItemMessageBusinessRules;
            _organizationItemMessageReadRepository = organizationItemMessageReadRepository;
        }

        public async Task<UpdatedOrganizationItemMessageResponse> Handle(UpdateOrganizationItemMessageCommand request, CancellationToken cancellationToken)
        {
            X.OrganizationItemMessage? organizationItemMessage = await _organizationItemMessageReadRepository.GetAsync(predicate: x => x.Gid == request.Gid,
                include: i => i.Include(i => i.UserFK).Include(i => i.OrganizationItemFK), cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _organizationItemMessageBusinessRules.OrganizationItemMessageShouldExistWhenSelected(organizationItemMessage);
            organizationItemMessage = _mapper.Map(request, organizationItemMessage);

            _organizationItemMessageWriteRepository.Update(organizationItemMessage!);
            await _organizationItemMessageWriteRepository.SaveAsync();
            GetByGidOrganizationItemMessageResponse obj = _mapper.Map<GetByGidOrganizationItemMessageResponse>(organizationItemMessage);

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
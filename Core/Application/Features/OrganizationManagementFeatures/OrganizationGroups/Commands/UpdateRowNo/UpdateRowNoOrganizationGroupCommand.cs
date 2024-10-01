using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Commands.Create;
using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Rules;
using Application.Helpers.UpdateRowNo;
using Application.Repositories.OrganizationManagementRepos.OrganizationGroupRepo;
using AutoMapper;
using X = Domain.Entities.OrganizationManagements;
using MediatR;

namespace Application.Features.OrganizationManagementFeatures.OrganizationGroups.Commands.UpdateRowNo
{
    public class UpdateRowNoOrganizationGroupCommand : IRequest<UpdateRowNoOrganizationGroupResponse>
    {
        public Guid Gid { get; set; }
        public bool IsUp { get; set; }

        public class UpdateRowNoOrganizationGroupCommandHandler : IRequestHandler<UpdateRowNoOrganizationGroupCommand, UpdateRowNoOrganizationGroupResponse>
        {
            private readonly IMapper _mapper;
            private readonly IOrganizationGroupWriteRepository _organizationGroupWriteRepository;
            private readonly IOrganizationGroupReadRepository _organizationGroupReadRepository;
            private readonly OrganizationGroupBusinessRules _organizationGroupBusinessRules;
            private readonly UpdateRowNoHelper<UpdateRowNoOrganizationGroupResponse, X.OrganizationGroup> _updateRowNoHelper;

            public UpdateRowNoOrganizationGroupCommandHandler(IMapper mapper, IOrganizationGroupWriteRepository organizationGroupWriteRepository,
                                             OrganizationGroupBusinessRules organizationGroupBusinessRules, IOrganizationGroupReadRepository organizationGroupReadRepository, UpdateRowNoHelper<UpdateRowNoOrganizationGroupResponse, X.OrganizationGroup> updateRowNoHelper)
            {
                _mapper = mapper;
                _organizationGroupWriteRepository = organizationGroupWriteRepository;
                _organizationGroupBusinessRules = organizationGroupBusinessRules;
                _organizationGroupReadRepository = organizationGroupReadRepository;
                _updateRowNoHelper = updateRowNoHelper;
            }

            public async Task<UpdateRowNoOrganizationGroupResponse> Handle(UpdateRowNoOrganizationGroupCommand request, CancellationToken cancellationToken)
            {
                List<X.OrganizationGroup> lst = _organizationGroupReadRepository.GetAll().OrderBy(a => a.RowNo).ToList();

                X.OrganizationGroup select = lst.Where(a => a.Gid == request.Gid).FirstOrDefault();

                UpdateRowNoOrganizationGroupResponse response = await _updateRowNoHelper.UpdateRowNo(lst, select, request.IsUp);

                await _organizationGroupWriteRepository.SaveAsync();

                return response;
            }
        }
    }
}
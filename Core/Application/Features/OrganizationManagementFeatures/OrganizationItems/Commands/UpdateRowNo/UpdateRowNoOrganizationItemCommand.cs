using Application.Features.OrganizationManagementFeatures.OrganizationItems.Commands.Create;
using Application.Features.OrganizationManagementFeatures.OrganizationItems.Rules;
using Application.Helpers.UpdateRowNo;
using Application.Repositories.OrganizationManagementRepos.OrganizationItemRepo;
using AutoMapper;
using X = Domain.Entities.OrganizationManagements;
using MediatR;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItems.Commands.UpdateRowNo
{
    public class UpdateRowNoOrganizationItemCommand : IRequest<UpdateRowNoOrganizationItemResponse>
    {
        public Guid Gid { get; set; }
        public bool IsUp { get; set; }

        public class UpdateRowNoOrganizationItemCommandHandler : IRequestHandler<UpdateRowNoOrganizationItemCommand, UpdateRowNoOrganizationItemResponse>
        {
            private readonly IMapper _mapper;
            private readonly IOrganizationItemWriteRepository _organizationItemWriteRepository;
            private readonly IOrganizationItemReadRepository _organizationItemReadRepository;
            private readonly OrganizationItemBusinessRules _organizationItemBusinessRules;
            private readonly UpdateRowNoHelper<UpdateRowNoOrganizationItemResponse, X.OrganizationItem> _updateRowNoHelper;

            public UpdateRowNoOrganizationItemCommandHandler(IMapper mapper, IOrganizationItemWriteRepository organizationItemWriteRepository,
                                             OrganizationItemBusinessRules organizationItemBusinessRules, IOrganizationItemReadRepository organizationItemReadRepository, UpdateRowNoHelper<UpdateRowNoOrganizationItemResponse, X.OrganizationItem> updateRowNoHelper)
            {
                _mapper = mapper;
                _organizationItemWriteRepository = organizationItemWriteRepository;
                _organizationItemBusinessRules = organizationItemBusinessRules;
                _organizationItemReadRepository = organizationItemReadRepository;
                _updateRowNoHelper = updateRowNoHelper;
            }

            public async Task<UpdateRowNoOrganizationItemResponse> Handle(UpdateRowNoOrganizationItemCommand request, CancellationToken cancellationToken)
            {
                List<X.OrganizationItem> lst = _organizationItemReadRepository.GetAll().OrderBy(a => a.RowNo).ToList();

                X.OrganizationItem select = lst.Where(a => a.Gid == request.Gid).FirstOrDefault();

                UpdateRowNoOrganizationItemResponse response = await _updateRowNoHelper.UpdateRowNo(lst, select, request.IsUp);

                await _organizationItemWriteRepository.SaveAsync();

                return response;
            }
        }
    }
}
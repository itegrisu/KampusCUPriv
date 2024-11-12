using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.Create;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Rules;
using Application.Helpers.UpdateRowNo;
using Application.Repositories.OrganizationManagementRepos.OrganizationItemFileRepo;
using AutoMapper;
using X = Domain.Entities.OrganizationManagements;
using MediatR;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.UpdateRowNo
{
    public class UpdateRowNoOrganizationItemFileCommand : IRequest<UpdateRowNoOrganizationItemFileResponse>
    {
        public Guid Gid { get; set; }
        public bool IsUp { get; set; }

        public class UpdateRowNoOrganizationItemFileCommandHandler : IRequestHandler<UpdateRowNoOrganizationItemFileCommand, UpdateRowNoOrganizationItemFileResponse>
        {
            private readonly IMapper _mapper;
            private readonly IOrganizationItemFileWriteRepository _organizationItemFileWriteRepository;
            private readonly IOrganizationItemFileReadRepository _organizationItemFileReadRepository;
            private readonly OrganizationItemFileBusinessRules _organizationItemFileBusinessRules;
            private readonly UpdateRowNoHelper<UpdateRowNoOrganizationItemFileResponse, X.OrganizationItemFile> _updateRowNoHelper;

            public UpdateRowNoOrganizationItemFileCommandHandler(IMapper mapper, IOrganizationItemFileWriteRepository organizationItemFileWriteRepository,
                                             OrganizationItemFileBusinessRules organizationItemFileBusinessRules, IOrganizationItemFileReadRepository organizationItemFileReadRepository, UpdateRowNoHelper<UpdateRowNoOrganizationItemFileResponse, X.OrganizationItemFile> updateRowNoHelper)
            {
                _mapper = mapper;
                _organizationItemFileWriteRepository = organizationItemFileWriteRepository;
                _organizationItemFileBusinessRules = organizationItemFileBusinessRules;
                _organizationItemFileReadRepository = organizationItemFileReadRepository;
                _updateRowNoHelper = updateRowNoHelper;
            }

            public async Task<UpdateRowNoOrganizationItemFileResponse> Handle(UpdateRowNoOrganizationItemFileCommand request, CancellationToken cancellationToken)
            {
                List<X.OrganizationItemFile> lst = _organizationItemFileReadRepository.GetAll().OrderBy(a => a.RowNo).ToList();

                X.OrganizationItemFile select = lst.Where(a => a.Gid == request.Gid).FirstOrDefault();

                UpdateRowNoOrganizationItemFileResponse response = await _updateRowNoHelper.UpdateRowNo(lst, select, request.IsUp);

                await _organizationItemFileWriteRepository.SaveAsync();

                return response;
            }
        }
    }
}
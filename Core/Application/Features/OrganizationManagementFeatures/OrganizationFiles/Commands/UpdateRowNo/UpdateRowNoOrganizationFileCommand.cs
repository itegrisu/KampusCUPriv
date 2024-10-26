using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.Create;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Rules;
using Application.Helpers.UpdateRowNo;
using Application.Repositories.OrganizationManagementRepos.OrganizationFileRepo;
using AutoMapper;
using X = Domain.Entities.OrganizationManagements;
using MediatR;

namespace Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.UpdateRowNo
{
    public class UpdateRowNoOrganizationFileCommand : IRequest<UpdateRowNoOrganizationFileResponse>
    {
        public Guid Gid { get; set; }
        public bool IsUp { get; set; }

        public class UpdateRowNoOrganizationFileCommandHandler : IRequestHandler<UpdateRowNoOrganizationFileCommand, UpdateRowNoOrganizationFileResponse>
        {
            private readonly IMapper _mapper;
            private readonly IOrganizationFileWriteRepository _organizationFileWriteRepository;
            private readonly IOrganizationFileReadRepository _organizationFileReadRepository;
            private readonly OrganizationFileBusinessRules _organizationFileBusinessRules;
            private readonly UpdateRowNoHelper<UpdateRowNoOrganizationFileResponse, X.OrganizationFile> _updateRowNoHelper;

            public UpdateRowNoOrganizationFileCommandHandler(IMapper mapper, IOrganizationFileWriteRepository organizationFileWriteRepository,
                                             OrganizationFileBusinessRules organizationFileBusinessRules, IOrganizationFileReadRepository organizationFileReadRepository, UpdateRowNoHelper<UpdateRowNoOrganizationFileResponse, X.OrganizationFile> updateRowNoHelper)
            {
                _mapper = mapper;
                _organizationFileWriteRepository = organizationFileWriteRepository;
                _organizationFileBusinessRules = organizationFileBusinessRules;
                _organizationFileReadRepository = organizationFileReadRepository;
                _updateRowNoHelper = updateRowNoHelper;
            }

            public async Task<UpdateRowNoOrganizationFileResponse> Handle(UpdateRowNoOrganizationFileCommand request, CancellationToken cancellationToken)
            {
                List<X.OrganizationFile> lst = _organizationFileReadRepository.GetAll().OrderBy(a => a.RowNo).ToList();

                X.OrganizationFile select = lst.Where(a => a.Gid == request.Gid).FirstOrDefault();

                UpdateRowNoOrganizationFileResponse response = await _updateRowNoHelper.UpdateRowNo(lst, select, request.IsUp);

                await _organizationFileWriteRepository.SaveAsync();

                return response;
            }
        }
    }
}
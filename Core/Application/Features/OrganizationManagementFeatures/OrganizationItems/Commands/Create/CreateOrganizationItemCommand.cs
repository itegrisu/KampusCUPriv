using Application.Features.OrganizationManagementFeatures.OrganizationItems.Constants;
using Application.Features.OrganizationManagementFeatures.OrganizationItems.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.OrganizationItems.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationItemRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItems.Commands.Create;

public class CreateOrganizationItemCommand : IRequest<CreatedOrganizationItemResponse>
{
    public Guid GidOrganizationGroupFK { get; set; }
    public string? GidMainResponsibleUserFK { get; set; }
    public string ItemName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Priority { get; set; }
    public bool IsStar { get; set; }
    public EnumItemStatus ItemStatus { get; set; }



    public class CreateOrganizationItemCommandHandler : IRequestHandler<CreateOrganizationItemCommand, CreatedOrganizationItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationItemWriteRepository _organizationItemWriteRepository;
        private readonly IOrganizationItemReadRepository _organizationItemReadRepository;
        private readonly OrganizationItemBusinessRules _organizationItemBusinessRules;

        public CreateOrganizationItemCommandHandler(IMapper mapper, IOrganizationItemWriteRepository organizationItemWriteRepository,
                                         OrganizationItemBusinessRules organizationItemBusinessRules, IOrganizationItemReadRepository organizationItemReadRepository)
        {
            _mapper = mapper;
            _organizationItemWriteRepository = organizationItemWriteRepository;
            _organizationItemBusinessRules = organizationItemBusinessRules;
            _organizationItemReadRepository = organizationItemReadRepository;
        }

        public async Task<CreatedOrganizationItemResponse> Handle(CreateOrganizationItemCommand request, CancellationToken cancellationToken)
        {
            await _organizationItemBusinessRules.OrganizationGroupShouldExistWhenSelected(request.GidOrganizationGroupFK);
            await _organizationItemBusinessRules.MainResponsibleUserShouldExistWhenSelected(request.GidMainResponsibleUserFK);

            List<X.OrganizationItem> organizationItems = await _organizationItemReadRepository.GetAll().ToListAsync();
            int maxRowNo = organizationItems.Count == 0 ? 0 : organizationItems.Max(x => x.RowNo);

            X.OrganizationItem organizationItem = _mapper.Map<X.OrganizationItem>(request);
            organizationItem.RowNo = maxRowNo + 1;

            await _organizationItemWriteRepository.AddAsync(organizationItem);
            await _organizationItemWriteRepository.SaveAsync();

            X.OrganizationItem savedOrganizationItem = await _organizationItemReadRepository.GetAsync(predicate: x => x.Gid == organizationItem.Gid, include: x => x.Include(x => x.OrganizationGroupFK).Include(x => x.MainResponsibleUserFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidOrganizationItemResponse obj = _mapper.Map<GetByGidOrganizationItemResponse>(savedOrganizationItem);
            return new()
            {
                Title = OrganizationItemsBusinessMessages.ProcessCompleted,
                Message = OrganizationItemsBusinessMessages.SuccessCreatedOrganizationItemMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
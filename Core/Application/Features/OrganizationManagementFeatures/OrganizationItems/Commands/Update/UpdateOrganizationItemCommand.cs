using Application.Features.OrganizationManagementFeatures.OrganizationItems.Constants;
using Application.Features.OrganizationManagementFeatures.OrganizationItems.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.OrganizationItems.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationItemRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItems.Commands.Update;

public class UpdateOrganizationItemCommand : IRequest<UpdatedOrganizationItemResponse>
{
    public Guid Gid { get; set; }

    public Guid GidOrganizationGroupFK { get; set; }
    public string? GidMainResponsibleUserFK { get; set; }

    public string ItemName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Priority { get; set; }
    public bool IsStar { get; set; }
    public EnumItemStatus ItemStatus { get; set; }
    public int RowNo { get; set; }



    public class UpdateOrganizationItemCommandHandler : IRequestHandler<UpdateOrganizationItemCommand, UpdatedOrganizationItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationItemWriteRepository _organizationItemWriteRepository;
        private readonly IOrganizationItemReadRepository _organizationItemReadRepository;
        private readonly OrganizationItemBusinessRules _organizationItemBusinessRules;

        public UpdateOrganizationItemCommandHandler(IMapper mapper, IOrganizationItemWriteRepository organizationItemWriteRepository,
                                         OrganizationItemBusinessRules organizationItemBusinessRules, IOrganizationItemReadRepository organizationItemReadRepository)
        {
            _mapper = mapper;
            _organizationItemWriteRepository = organizationItemWriteRepository;
            _organizationItemBusinessRules = organizationItemBusinessRules;
            _organizationItemReadRepository = organizationItemReadRepository;
        }

        public async Task<UpdatedOrganizationItemResponse> Handle(UpdateOrganizationItemCommand request, CancellationToken cancellationToken)
        {
            X.OrganizationItem? organizationItem = await _organizationItemReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _organizationItemBusinessRules.OrganizationItemShouldExistWhenSelected(organizationItem);
            await _organizationItemBusinessRules.OrganizationGroupShouldExistWhenSelected(request.GidOrganizationGroupFK);
            await _organizationItemBusinessRules.MainResponsibleUserShouldExistWhenSelected(request.GidMainResponsibleUserFK);
            await _organizationItemBusinessRules.DateRangeCheck(request.GidOrganizationGroupFK, request.StartDate, request.EndDate);


            organizationItem = _mapper.Map(request, organizationItem);

            _organizationItemWriteRepository.Update(organizationItem!);
            await _organizationItemWriteRepository.SaveAsync();

            X.OrganizationItem updatedOrganizationItem = await _organizationItemReadRepository.GetAsync(predicate: x => x.Gid == organizationItem.Gid, include: x => x.Include(x => x.OrganizationGroupFK).Include(x => x.MainResponsibleUserFK));

            GetByGidOrganizationItemResponse obj = _mapper.Map<GetByGidOrganizationItemResponse>(updatedOrganizationItem);

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
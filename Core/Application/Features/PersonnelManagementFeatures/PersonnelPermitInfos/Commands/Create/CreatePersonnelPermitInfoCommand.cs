using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelPermitInfoRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Commands.Create;

public class CreatePersonnelPermitInfoCommand : IRequest<CreatedPersonnelPermitInfoResponse>
{
    public Guid GidPersonnelFK { get; set; }
    public Guid GidPermitFK { get; set; }

    public DateTime PermitStartDate { get; set; }
    public DateTime PermitEndDate { get; set; }
    public string? Document { get; set; }
    public string? Description { get; set; }


    public class CreatePersonnelPermitInfoCommandHandler : IRequestHandler<CreatePersonnelPermitInfoCommand, CreatedPersonnelPermitInfoResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelPermitInfoWriteRepository _personnelPermitInfoWriteRepository;
        private readonly IPersonnelPermitInfoReadRepository _personnelPermitInfoReadRepository;
        private readonly PersonnelPermitInfoBusinessRules _personnelPermitInfoBusinessRules;

        public CreatePersonnelPermitInfoCommandHandler(IMapper mapper, IPersonnelPermitInfoWriteRepository personnelPermitInfoWriteRepository,
                                         PersonnelPermitInfoBusinessRules personnelPermitInfoBusinessRules, IPersonnelPermitInfoReadRepository personnelPermitInfoReadRepository)
        {
            _mapper = mapper;
            _personnelPermitInfoWriteRepository = personnelPermitInfoWriteRepository;
            _personnelPermitInfoBusinessRules = personnelPermitInfoBusinessRules;
            _personnelPermitInfoReadRepository = personnelPermitInfoReadRepository;
        }

        public async Task<CreatedPersonnelPermitInfoResponse> Handle(CreatePersonnelPermitInfoCommand request, CancellationToken cancellationToken)
        {
            await _personnelPermitInfoBusinessRules.UserShouldExistWhenSelected(request.GidPersonnelFK);
            await _personnelPermitInfoBusinessRules.PermitTypeShouldExistWhenSelected(request.GidPermitFK);

            X.PersonnelPermitInfo personnelPermitInfo = _mapper.Map<X.PersonnelPermitInfo>(request);

            await _personnelPermitInfoWriteRepository.AddAsync(personnelPermitInfo);
            await _personnelPermitInfoWriteRepository.SaveAsync();

            X.PersonnelPermitInfo savedPersonnelPermitInfo = await _personnelPermitInfoReadRepository.GetAsync(predicate: x => x.Gid == personnelPermitInfo.Gid, include:
                x => x.Include(x => x.UserFK).Include(x => x.PermitTypeFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidPersonnelPermitInfoResponse obj = _mapper.Map<GetByGidPersonnelPermitInfoResponse>(savedPersonnelPermitInfo);
            return new()
            {
                Title = PersonnelPermitInfosBusinessMessages.ProcessCompleted,
                Message = PersonnelPermitInfosBusinessMessages.SuccessCreatedPersonnelPermitInfoMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
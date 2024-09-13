using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelPermitInfoRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Commands.Update;

public class UpdatePersonnelPermitInfoCommand : IRequest<UpdatedPersonnelPermitInfoResponse>
{
    public Guid Gid { get; set; }

    public Guid GidPersonelFK { get; set; }
    public Guid GidPermitFK { get; set; }

    public DateTime IzinBaslamaTarihi { get; set; }
    public DateTime IzinBitisTarihi { get; set; }
    public string? Belge { get; set; }
    public string? Aciklama { get; set; }



    public class UpdatePersonnelPermitInfoCommandHandler : IRequestHandler<UpdatePersonnelPermitInfoCommand, UpdatedPersonnelPermitInfoResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelPermitInfoWriteRepository _personnelPermitInfoWriteRepository;
        private readonly IPersonnelPermitInfoReadRepository _personnelPermitInfoReadRepository;
        private readonly PersonnelPermitInfoBusinessRules _personnelPermitInfoBusinessRules;

        public UpdatePersonnelPermitInfoCommandHandler(IMapper mapper, IPersonnelPermitInfoWriteRepository personnelPermitInfoWriteRepository,
                                         PersonnelPermitInfoBusinessRules personnelPermitInfoBusinessRules, IPersonnelPermitInfoReadRepository personnelPermitInfoReadRepository)
        {
            _mapper = mapper;
            _personnelPermitInfoWriteRepository = personnelPermitInfoWriteRepository;
            _personnelPermitInfoBusinessRules = personnelPermitInfoBusinessRules;
            _personnelPermitInfoReadRepository = personnelPermitInfoReadRepository;
        }

        public async Task<UpdatedPersonnelPermitInfoResponse> Handle(UpdatePersonnelPermitInfoCommand request, CancellationToken cancellationToken)
        {
            X.PersonnelPermitInfo? personnelPermitInfo = await _personnelPermitInfoReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _personnelPermitInfoBusinessRules.PersonnelPermitInfoShouldExistWhenSelected(personnelPermitInfo);
            await _personnelPermitInfoBusinessRules.UserShouldExistWhenSelected(request.GidPersonelFK);
            await _personnelPermitInfoBusinessRules.PermitTypeShouldExistWhenSelected(request.GidPermitFK);
            personnelPermitInfo = _mapper.Map(request, personnelPermitInfo);

            _personnelPermitInfoWriteRepository.Update(personnelPermitInfo!);
            await _personnelPermitInfoWriteRepository.SaveAsync();

            X.PersonnelPermitInfo updatedPersonnelPermitInfo = await _personnelPermitInfoReadRepository.GetAsync(predicate: x => x.Gid == personnelPermitInfo.Gid, include:
                x => x.Include(x => x.UserFK).Include(x => x.PermitTypeFK));
            GetByGidPersonnelPermitInfoResponse obj = _mapper.Map<GetByGidPersonnelPermitInfoResponse>(updatedPersonnelPermitInfo);

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
using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelPermitInfoRepo;
using AutoMapper;
using X = Domain.Entities.PersonnelManagements;
using MediatR;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Commands.Delete;

public class DeletePersonnelPermitInfoCommand : IRequest<DeletedPersonnelPermitInfoResponse>
{
	public Guid Gid { get; set; }

    public class DeletePersonnelPermitInfoCommandHandler : IRequestHandler<DeletePersonnelPermitInfoCommand, DeletedPersonnelPermitInfoResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelPermitInfoReadRepository _personnelPermitInfoReadRepository;
        private readonly IPersonnelPermitInfoWriteRepository _personnelPermitInfoWriteRepository;
        private readonly PersonnelPermitInfoBusinessRules _personnelPermitInfoBusinessRules;

        public DeletePersonnelPermitInfoCommandHandler(IMapper mapper, IPersonnelPermitInfoReadRepository personnelPermitInfoReadRepository,
                                         PersonnelPermitInfoBusinessRules personnelPermitInfoBusinessRules, IPersonnelPermitInfoWriteRepository personnelPermitInfoWriteRepository)
        {
            _mapper = mapper;
            _personnelPermitInfoReadRepository = personnelPermitInfoReadRepository;
            _personnelPermitInfoBusinessRules = personnelPermitInfoBusinessRules;
            _personnelPermitInfoWriteRepository = personnelPermitInfoWriteRepository;
        }

        public async Task<DeletedPersonnelPermitInfoResponse> Handle(DeletePersonnelPermitInfoCommand request, CancellationToken cancellationToken)
        {
            X.PersonnelPermitInfo? personnelPermitInfo = await _personnelPermitInfoReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _personnelPermitInfoBusinessRules.PersonnelPermitInfoShouldExistWhenSelected(personnelPermitInfo);
            personnelPermitInfo.DataState = Core.Enum.DataState.Deleted;

            _personnelPermitInfoWriteRepository.Update(personnelPermitInfo);
            await _personnelPermitInfoWriteRepository.SaveAsync();

            return new()
            {
                Title = PersonnelPermitInfosBusinessMessages.ProcessCompleted,
                Message = PersonnelPermitInfosBusinessMessages.SuccessDeletedPersonnelPermitInfoMessage,
                IsValid = true
            };
        }
    }
}
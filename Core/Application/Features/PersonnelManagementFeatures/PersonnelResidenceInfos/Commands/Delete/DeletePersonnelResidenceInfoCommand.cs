using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelResidenceInfoRepo;
using AutoMapper;
using X = Domain.Entities.PersonnelManagements;
using MediatR;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Commands.Delete;

public class DeletePersonnelResidenceInfoCommand : IRequest<DeletedPersonnelResidenceInfoResponse>
{
	public Guid Gid { get; set; }

    public class DeletePersonnelResidenceInfoCommandHandler : IRequestHandler<DeletePersonnelResidenceInfoCommand, DeletedPersonnelResidenceInfoResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelResidenceInfoReadRepository _personnelResidenceInfoReadRepository;
        private readonly IPersonnelResidenceInfoWriteRepository _personnelResidenceInfoWriteRepository;
        private readonly PersonnelResidenceInfoBusinessRules _personnelResidenceInfoBusinessRules;

        public DeletePersonnelResidenceInfoCommandHandler(IMapper mapper, IPersonnelResidenceInfoReadRepository personnelResidenceInfoReadRepository,
                                         PersonnelResidenceInfoBusinessRules personnelResidenceInfoBusinessRules, IPersonnelResidenceInfoWriteRepository personnelResidenceInfoWriteRepository)
        {
            _mapper = mapper;
            _personnelResidenceInfoReadRepository = personnelResidenceInfoReadRepository;
            _personnelResidenceInfoBusinessRules = personnelResidenceInfoBusinessRules;
            _personnelResidenceInfoWriteRepository = personnelResidenceInfoWriteRepository;
        }

        public async Task<DeletedPersonnelResidenceInfoResponse> Handle(DeletePersonnelResidenceInfoCommand request, CancellationToken cancellationToken)
        {
            X.PersonnelResidenceInfo? personnelResidenceInfo = await _personnelResidenceInfoReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _personnelResidenceInfoBusinessRules.PersonnelResidenceInfoShouldExistWhenSelected(personnelResidenceInfo);
            personnelResidenceInfo.DataState = Core.Enum.DataState.Deleted;

            _personnelResidenceInfoWriteRepository.Update(personnelResidenceInfo);
            await _personnelResidenceInfoWriteRepository.SaveAsync();

            return new()
            {
                Title = PersonnelResidenceInfosBusinessMessages.ProcessCompleted,
                Message = PersonnelResidenceInfosBusinessMessages.SuccessDeletedPersonnelResidenceInfoMessage,
                IsValid = true
            };
        }
    }
}
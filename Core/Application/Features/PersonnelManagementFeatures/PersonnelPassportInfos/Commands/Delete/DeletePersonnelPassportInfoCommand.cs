using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelPassportInfoRepo;
using AutoMapper;
using X = Domain.Entities.PersonnelManagements;
using MediatR;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Commands.Delete;

public class DeletePersonnelPassportInfoCommand : IRequest<DeletedPersonnelPassportInfoResponse>
{
	public Guid Gid { get; set; }

    public class DeletePersonnelPassportInfoCommandHandler : IRequestHandler<DeletePersonnelPassportInfoCommand, DeletedPersonnelPassportInfoResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelPassportInfoReadRepository _personnelPassportInfoReadRepository;
        private readonly IPersonnelPassportInfoWriteRepository _personnelPassportInfoWriteRepository;
        private readonly PersonnelPassportInfoBusinessRules _personnelPassportInfoBusinessRules;

        public DeletePersonnelPassportInfoCommandHandler(IMapper mapper, IPersonnelPassportInfoReadRepository personnelPassportInfoReadRepository,
                                         PersonnelPassportInfoBusinessRules personnelPassportInfoBusinessRules, IPersonnelPassportInfoWriteRepository personnelPassportInfoWriteRepository)
        {
            _mapper = mapper;
            _personnelPassportInfoReadRepository = personnelPassportInfoReadRepository;
            _personnelPassportInfoBusinessRules = personnelPassportInfoBusinessRules;
            _personnelPassportInfoWriteRepository = personnelPassportInfoWriteRepository;
        }

        public async Task<DeletedPersonnelPassportInfoResponse> Handle(DeletePersonnelPassportInfoCommand request, CancellationToken cancellationToken)
        {
            X.PersonnelPassportInfo? personnelPassportInfo = await _personnelPassportInfoReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _personnelPassportInfoBusinessRules.PersonnelPassportInfoShouldExistWhenSelected(personnelPassportInfo);
            personnelPassportInfo.DataState = Core.Enum.DataState.Deleted;

            _personnelPassportInfoWriteRepository.Update(personnelPassportInfo);
            await _personnelPassportInfoWriteRepository.SaveAsync();

            return new()
            {
                Title = PersonnelPassportInfosBusinessMessages.ProcessCompleted,
                Message = PersonnelPassportInfosBusinessMessages.SuccessDeletedPersonnelPassportInfoMessage,
                IsValid = true
            };
        }
    }
}
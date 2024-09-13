using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelAddressRepo;
using AutoMapper;
using X = Domain.Entities.PersonnelManagements;
using MediatR;

namespace Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Commands.Delete;

public class DeletePersonnelAddressCommand : IRequest<DeletedPersonnelAddressResponse>
{
	public Guid Gid { get; set; }

    public class DeletePersonnelAddressCommandHandler : IRequestHandler<DeletePersonnelAddressCommand, DeletedPersonnelAddressResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelAddressReadRepository _personnelAddressReadRepository;
        private readonly IPersonnelAddressWriteRepository _personnelAddressWriteRepository;
        private readonly PersonnelAddressBusinessRules _personnelAddressBusinessRules;

        public DeletePersonnelAddressCommandHandler(IMapper mapper, IPersonnelAddressReadRepository personnelAddressReadRepository,
                                         PersonnelAddressBusinessRules personnelAddressBusinessRules, IPersonnelAddressWriteRepository personnelAddressWriteRepository)
        {
            _mapper = mapper;
            _personnelAddressReadRepository = personnelAddressReadRepository;
            _personnelAddressBusinessRules = personnelAddressBusinessRules;
            _personnelAddressWriteRepository = personnelAddressWriteRepository;
        }

        public async Task<DeletedPersonnelAddressResponse> Handle(DeletePersonnelAddressCommand request, CancellationToken cancellationToken)
        {
            X.PersonnelAddress? personnelAddress = await _personnelAddressReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _personnelAddressBusinessRules.PersonnelAddressShouldExistWhenSelected(personnelAddress);
            personnelAddress.DataState = Core.Enum.DataState.Deleted;

            _personnelAddressWriteRepository.Update(personnelAddress);
            await _personnelAddressWriteRepository.SaveAsync();

            return new()
            {
                Title = PersonnelAddressesBusinessMessages.ProcessCompleted,
                Message = PersonnelAddressesBusinessMessages.SuccessDeletedPersonnelAddressMessage,
                IsValid = true
            };
        }
    }
}
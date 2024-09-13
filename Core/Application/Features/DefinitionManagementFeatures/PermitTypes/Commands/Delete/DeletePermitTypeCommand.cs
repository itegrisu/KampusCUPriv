using Application.Features.DefinitionManagementFeatures.PermitTypes.Constants;
using Application.Features.DefinitionManagementFeatures.PermitTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.PermitTypeRepo;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;

namespace Application.Features.DefinitionManagementFeatures.PermitTypes.Commands.Delete;

public class DeletePermitTypeCommand : IRequest<DeletedPermitTypeResponse>
{
	public Guid Gid { get; set; }

    public class DeletePermitTypeCommandHandler : IRequestHandler<DeletePermitTypeCommand, DeletedPermitTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPermitTypeReadRepository _permitTypeReadRepository;
        private readonly IPermitTypeWriteRepository _permitTypeWriteRepository;
        private readonly PermitTypeBusinessRules _permitTypeBusinessRules;

        public DeletePermitTypeCommandHandler(IMapper mapper, IPermitTypeReadRepository permitTypeReadRepository,
                                         PermitTypeBusinessRules permitTypeBusinessRules, IPermitTypeWriteRepository permitTypeWriteRepository)
        {
            _mapper = mapper;
            _permitTypeReadRepository = permitTypeReadRepository;
            _permitTypeBusinessRules = permitTypeBusinessRules;
            _permitTypeWriteRepository = permitTypeWriteRepository;
        }

        public async Task<DeletedPermitTypeResponse> Handle(DeletePermitTypeCommand request, CancellationToken cancellationToken)
        {
            X.PermitType? permitType = await _permitTypeReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _permitTypeBusinessRules.PermitTypeShouldExistWhenSelected(permitType);
            permitType.DataState = Core.Enum.DataState.Deleted;

            _permitTypeWriteRepository.Update(permitType);
            await _permitTypeWriteRepository.SaveAsync();

            return new()
            {
                Title = PermitTypesBusinessMessages.ProcessCompleted,
                Message = PermitTypesBusinessMessages.SuccessDeletedPermitTypeMessage,
                IsValid = true
            };
        }
    }
}
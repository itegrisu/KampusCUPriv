using Application.Features.DefinitionManagementFeatures.TyreTypes.Constants;
using Application.Features.DefinitionManagementFeatures.TyreTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.TyreTypeRepo;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;

namespace Application.Features.DefinitionManagementFeatures.TyreTypes.Commands.Delete;

public class DeleteTyreTypeCommand : IRequest<DeletedTyreTypeResponse>
{
	public Guid Gid { get; set; }

    public class DeleteTyreTypeCommandHandler : IRequestHandler<DeleteTyreTypeCommand, DeletedTyreTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITyreTypeReadRepository _tyreTypeReadRepository;
        private readonly ITyreTypeWriteRepository _tyreTypeWriteRepository;
        private readonly TyreTypeBusinessRules _tyreTypeBusinessRules;

        public DeleteTyreTypeCommandHandler(IMapper mapper, ITyreTypeReadRepository tyreTypeReadRepository,
                                         TyreTypeBusinessRules tyreTypeBusinessRules, ITyreTypeWriteRepository tyreTypeWriteRepository)
        {
            _mapper = mapper;
            _tyreTypeReadRepository = tyreTypeReadRepository;
            _tyreTypeBusinessRules = tyreTypeBusinessRules;
            _tyreTypeWriteRepository = tyreTypeWriteRepository;
        }

        public async Task<DeletedTyreTypeResponse> Handle(DeleteTyreTypeCommand request, CancellationToken cancellationToken)
        {
            X.TyreType? tyreType = await _tyreTypeReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _tyreTypeBusinessRules.TyreTypeShouldExistWhenSelected(tyreType);
            tyreType.DataState = Core.Enum.DataState.Deleted;

            _tyreTypeWriteRepository.Update(tyreType);
            await _tyreTypeWriteRepository.SaveAsync();

            return new()
            {
                Title = TyreTypesBusinessMessages.ProcessCompleted,
                Message = TyreTypesBusinessMessages.SuccessDeletedTyreTypeMessage,
                IsValid = true
            };
        }
    }
}
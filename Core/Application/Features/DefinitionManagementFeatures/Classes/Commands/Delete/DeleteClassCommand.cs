using Application.Features.DefinitionFeatures.Classes.Constants;
using Application.Features.DefinitionFeatures.Classes.Rules;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using Application.Repositories.DefinitionManagementRepo.ClassRepo;

namespace Application.Features.DefinitionFeatures.Classes.Commands.Delete;

public class DeleteClassCommand : IRequest<DeletedClassResponse>
{
	public Guid Gid { get; set; }

    public class DeleteClassCommandHandler : IRequestHandler<DeleteClassCommand, DeletedClassResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassReadRepository _classReadRepository;
        private readonly IClassWriteRepository _classWriteRepository;
        private readonly ClassBusinessRules _classBusinessRules;

        public DeleteClassCommandHandler(IMapper mapper, IClassReadRepository classReadRepository,
                                         ClassBusinessRules classBusinessRules, IClassWriteRepository classWriteRepository)
        {
            _mapper = mapper;
            _classReadRepository = classReadRepository;
            _classBusinessRules = classBusinessRules;
            _classWriteRepository = classWriteRepository;
        }

        public async Task<DeletedClassResponse> Handle(DeleteClassCommand request, CancellationToken cancellationToken)
        {
            X.Class? class1 = await _classReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _classBusinessRules.ClassShouldExistWhenSelected(class1);
            class1.DataState = Core.Enum.DataState.Deleted;

            _classWriteRepository.Update(class1);
            await _classWriteRepository.SaveAsync();

            return new()
            {
                Title = ClassesBusinessMessages.ProcessCompleted,
                Message = ClassesBusinessMessages.SuccessDeletedClassMessage,
                IsValid = true
            };
        }
    }
}
using Application.Features.DefinitionManagementFeatures.MeasureTypes.Constants;
using Application.Features.DefinitionManagementFeatures.MeasureTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.MeasureTypeRepo;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;

namespace Application.Features.DefinitionManagementFeatures.MeasureTypes.Commands.Delete;

public class DeleteMeasureTypeCommand : IRequest<DeletedMeasureTypeResponse>
{
	public Guid Gid { get; set; }

    public class DeleteMeasureTypeCommandHandler : IRequestHandler<DeleteMeasureTypeCommand, DeletedMeasureTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMeasureTypeReadRepository _measureTypeReadRepository;
        private readonly IMeasureTypeWriteRepository _measureTypeWriteRepository;
        private readonly MeasureTypeBusinessRules _measureTypeBusinessRules;

        public DeleteMeasureTypeCommandHandler(IMapper mapper, IMeasureTypeReadRepository measureTypeReadRepository,
                                         MeasureTypeBusinessRules measureTypeBusinessRules, IMeasureTypeWriteRepository measureTypeWriteRepository)
        {
            _mapper = mapper;
            _measureTypeReadRepository = measureTypeReadRepository;
            _measureTypeBusinessRules = measureTypeBusinessRules;
            _measureTypeWriteRepository = measureTypeWriteRepository;
        }

        public async Task<DeletedMeasureTypeResponse> Handle(DeleteMeasureTypeCommand request, CancellationToken cancellationToken)
        {
            X.MeasureType? measureType = await _measureTypeReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _measureTypeBusinessRules.MeasureTypeShouldExistWhenSelected(measureType);
            measureType.DataState = Core.Enum.DataState.Deleted;

            _measureTypeWriteRepository.Update(measureType);
            await _measureTypeWriteRepository.SaveAsync();

            return new()
            {
                Title = MeasureTypesBusinessMessages.ProcessCompleted,
                Message = MeasureTypesBusinessMessages.SuccessDeletedMeasureTypeMessage,
                IsValid = true
            };
        }
    }
}
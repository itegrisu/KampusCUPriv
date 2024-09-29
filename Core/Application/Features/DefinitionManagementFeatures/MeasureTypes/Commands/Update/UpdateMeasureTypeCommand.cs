using Application.Features.DefinitionManagementFeatures.MeasureTypes.Constants;
using Application.Features.DefinitionManagementFeatures.MeasureTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.MeasureTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.MeasureTypeRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.MeasureTypes.Commands.Update;

public class UpdateMeasureTypeCommand : IRequest<UpdatedMeasureTypeResponse>
{
    public Guid Gid { get; set; }
    public string Name { get; set; }


    public class UpdateMeasureTypeCommandHandler : IRequestHandler<UpdateMeasureTypeCommand, UpdatedMeasureTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMeasureTypeWriteRepository _measureTypeWriteRepository;
        private readonly IMeasureTypeReadRepository _measureTypeReadRepository;
        private readonly MeasureTypeBusinessRules _measureTypeBusinessRules;

        public UpdateMeasureTypeCommandHandler(IMapper mapper, IMeasureTypeWriteRepository measureTypeWriteRepository,
                                         MeasureTypeBusinessRules measureTypeBusinessRules, IMeasureTypeReadRepository measureTypeReadRepository)
        {
            _mapper = mapper;
            _measureTypeWriteRepository = measureTypeWriteRepository;
            _measureTypeBusinessRules = measureTypeBusinessRules;
            _measureTypeReadRepository = measureTypeReadRepository;
        }

        public async Task<UpdatedMeasureTypeResponse> Handle(UpdateMeasureTypeCommand request, CancellationToken cancellationToken)
        {
            X.MeasureType? measureType = await _measureTypeReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);

            await _measureTypeBusinessRules.MeasureTypeShouldExistWhenSelected(measureType);
            await _measureTypeBusinessRules.CheckMeasureTypeNameIsUnique(request.Name, request.Gid);
            measureType = _mapper.Map(request, measureType);

            _measureTypeWriteRepository.Update(measureType!);
            await _measureTypeWriteRepository.SaveAsync();
            GetByGidMeasureTypeResponse obj = _mapper.Map<GetByGidMeasureTypeResponse>(measureType);

            return new()
            {
                Title = MeasureTypesBusinessMessages.ProcessCompleted,
                Message = MeasureTypesBusinessMessages.SuccessCreatedMeasureTypeMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
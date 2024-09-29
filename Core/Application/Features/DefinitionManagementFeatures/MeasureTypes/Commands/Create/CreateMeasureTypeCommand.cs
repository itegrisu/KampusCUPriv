using Application.Features.DefinitionManagementFeatures.MeasureTypes.Constants;
using Application.Features.DefinitionManagementFeatures.MeasureTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.MeasureTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.MeasureTypeRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.MeasureTypes.Commands.Create;

public class CreateMeasureTypeCommand : IRequest<CreatedMeasureTypeResponse>
{

    public string Name { get; set; }


    public class CreateMeasureTypeCommandHandler : IRequestHandler<CreateMeasureTypeCommand, CreatedMeasureTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMeasureTypeWriteRepository _measureTypeWriteRepository;
        private readonly IMeasureTypeReadRepository _measureTypeReadRepository;
        private readonly MeasureTypeBusinessRules _measureTypeBusinessRules;

        public CreateMeasureTypeCommandHandler(IMapper mapper, IMeasureTypeWriteRepository measureTypeWriteRepository,
                                         MeasureTypeBusinessRules measureTypeBusinessRules, IMeasureTypeReadRepository measureTypeReadRepository)
        {
            _mapper = mapper;
            _measureTypeWriteRepository = measureTypeWriteRepository;
            _measureTypeBusinessRules = measureTypeBusinessRules;
            _measureTypeReadRepository = measureTypeReadRepository;
        }

        public async Task<CreatedMeasureTypeResponse> Handle(CreateMeasureTypeCommand request, CancellationToken cancellationToken)
        {
            await _measureTypeBusinessRules.CheckMeasureTypeNameIsUnique(request.Name);
            X.MeasureType measureType = _mapper.Map<X.MeasureType>(request);

            await _measureTypeWriteRepository.AddAsync(measureType);
            await _measureTypeWriteRepository.SaveAsync();

            X.MeasureType savedMeasureType = await _measureTypeReadRepository.GetAsync(predicate: x => x.Gid == measureType.Gid);


            GetByGidMeasureTypeResponse obj = _mapper.Map<GetByGidMeasureTypeResponse>(savedMeasureType);
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
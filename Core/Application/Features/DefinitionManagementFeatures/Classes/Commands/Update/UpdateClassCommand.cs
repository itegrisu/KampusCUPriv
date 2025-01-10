using Application.Features.DefinitionFeatures.Classes.Constants;
using Application.Features.DefinitionFeatures.Classes.Queries.GetByGid;
using Application.Features.DefinitionFeatures.Classes.Rules;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using Application.Repositories.DefinitionManagementRepo.ClassRepo;

namespace Application.Features.DefinitionFeatures.Classes.Commands.Update;

public class UpdateClassCommand : IRequest<UpdatedClassResponse>
{
    public Guid Gid { get; set; }
    public string Name { get; set; }

    public class UpdateClassCommandHandler : IRequestHandler<UpdateClassCommand, UpdatedClassResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassWriteRepository _classWriteRepository;
        private readonly IClassReadRepository _classReadRepository;
        private readonly ClassBusinessRules _classBusinessRules;

        public UpdateClassCommandHandler(IMapper mapper, IClassWriteRepository classWriteRepository,
                                         ClassBusinessRules classBusinessRules, IClassReadRepository classReadRepository)
        {
            _mapper = mapper;
            _classWriteRepository = classWriteRepository;
            _classBusinessRules = classBusinessRules;
            _classReadRepository = classReadRepository;
        }

        public async Task<UpdatedClassResponse> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
        {
            X.Class? class1 = await _classReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _classBusinessRules.ClassShouldExistWhenSelected(class1);
            class1 = _mapper.Map(request, class1);

            _classWriteRepository.Update(class1!);
            await _classWriteRepository.SaveAsync();
            GetByGidClassResponse obj = _mapper.Map<GetByGidClassResponse>(class1);

            return new()
            {
                Title = ClassesBusinessMessages.ProcessCompleted,
                Message = ClassesBusinessMessages.SuccessCreatedClassMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
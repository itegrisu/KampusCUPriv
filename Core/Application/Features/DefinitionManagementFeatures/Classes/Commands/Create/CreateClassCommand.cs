using Application.Features.DefinitionFeatures.Classes.Constants;
using Application.Features.DefinitionFeatures.Classes.Queries.GetByGid;
using Application.Features.DefinitionFeatures.Classes.Rules;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.DefinitionManagementRepo.ClassRepo;

namespace Application.Features.DefinitionFeatures.Classes.Commands.Create;

public class CreateClassCommand : IRequest<CreatedClassResponse>
{
    public string Name { get; set; }

    public class CreateClassCommandHandler : IRequestHandler<CreateClassCommand, CreatedClassResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassWriteRepository _classWriteRepository;
        private readonly IClassReadRepository _classReadRepository;
        private readonly ClassBusinessRules _classBusinessRules;

        public CreateClassCommandHandler(IMapper mapper, IClassWriteRepository classWriteRepository,
                                         ClassBusinessRules classBusinessRules, IClassReadRepository classReadRepository)
        {
            _mapper = mapper;
            _classWriteRepository = classWriteRepository;
            _classBusinessRules = classBusinessRules;
            _classReadRepository = classReadRepository;
        }

        public async Task<CreatedClassResponse> Handle(CreateClassCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _classReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.Class class1 = _mapper.Map<X.Class>(request);
            //class.RowNo = maxRowNo + 1;

            await _classWriteRepository.AddAsync(class1);
            await _classWriteRepository.SaveAsync();

            X.Class savedClass = await _classReadRepository.GetAsync(predicate: x => x.Gid == class1.Gid);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidClassResponse obj = _mapper.Map<GetByGidClassResponse>(savedClass);
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
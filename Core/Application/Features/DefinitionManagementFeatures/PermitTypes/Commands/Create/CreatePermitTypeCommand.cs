using Application.Features.DefinitionManagementFeatures.PermitTypes.Constants;
using Application.Features.DefinitionManagementFeatures.PermitTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.PermitTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.PermitTypeRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.PermitTypes.Commands.Create;

public class CreatePermitTypeCommand : IRequest<CreatedPermitTypeResponse>
{

    public string IzinAdi { get; set; }

    public class CreatePermitTypeCommandHandler : IRequestHandler<CreatePermitTypeCommand, CreatedPermitTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPermitTypeWriteRepository _permitTypeWriteRepository;
        private readonly IPermitTypeReadRepository _permitTypeReadRepository;
        private readonly PermitTypeBusinessRules _permitTypeBusinessRules;

        public CreatePermitTypeCommandHandler(IMapper mapper, IPermitTypeWriteRepository permitTypeWriteRepository,
                                         PermitTypeBusinessRules permitTypeBusinessRules, IPermitTypeReadRepository permitTypeReadRepository)
        {
            _mapper = mapper;
            _permitTypeWriteRepository = permitTypeWriteRepository;
            _permitTypeBusinessRules = permitTypeBusinessRules;
            _permitTypeReadRepository = permitTypeReadRepository;
        }

        public async Task<CreatedPermitTypeResponse> Handle(CreatePermitTypeCommand request, CancellationToken cancellationToken)
        {
            await _permitTypeBusinessRules.CheckPermitTypeNameIsUnique(request.IzinAdi);

            X.PermitType permitType = _mapper.Map<X.PermitType>(request);

            await _permitTypeWriteRepository.AddAsync(permitType);
            await _permitTypeWriteRepository.SaveAsync();

            X.PermitType savedPermitType = await _permitTypeReadRepository.GetAsync(predicate: x => x.Gid == permitType.Gid);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidPermitTypeResponse obj = _mapper.Map<GetByGidPermitTypeResponse>(savedPermitType);
            return new()
            {
                Title = PermitTypesBusinessMessages.ProcessCompleted,
                Message = PermitTypesBusinessMessages.SuccessCreatedPermitTypeMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
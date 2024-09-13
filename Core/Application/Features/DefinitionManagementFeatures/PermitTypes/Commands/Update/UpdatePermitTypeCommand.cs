using Application.Features.DefinitionManagementFeatures.PermitTypes.Constants;
using Application.Features.DefinitionManagementFeatures.PermitTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.PermitTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.PermitTypeRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.PermitTypes.Commands.Update;

public class UpdatePermitTypeCommand : IRequest<UpdatedPermitTypeResponse>
{
    public Guid Gid { get; set; }
    public string IzinAdi { get; set; }



    public class UpdatePermitTypeCommandHandler : IRequestHandler<UpdatePermitTypeCommand, UpdatedPermitTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPermitTypeWriteRepository _permitTypeWriteRepository;
        private readonly IPermitTypeReadRepository _permitTypeReadRepository;
        private readonly PermitTypeBusinessRules _permitTypeBusinessRules;

        public UpdatePermitTypeCommandHandler(IMapper mapper, IPermitTypeWriteRepository permitTypeWriteRepository,
                                         PermitTypeBusinessRules permitTypeBusinessRules, IPermitTypeReadRepository permitTypeReadRepository)
        {
            _mapper = mapper;
            _permitTypeWriteRepository = permitTypeWriteRepository;
            _permitTypeBusinessRules = permitTypeBusinessRules;
            _permitTypeReadRepository = permitTypeReadRepository;
        }

        public async Task<UpdatedPermitTypeResponse> Handle(UpdatePermitTypeCommand request, CancellationToken cancellationToken)
        {
            X.PermitType? permitType = await _permitTypeReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);

            await _permitTypeBusinessRules.PermitTypeShouldExistWhenSelected(permitType);
            await _permitTypeBusinessRules.CheckPermitTypeNameIsUnique(request.IzinAdi);
            permitType = _mapper.Map(request, permitType);

            _permitTypeWriteRepository.Update(permitType!);
            await _permitTypeWriteRepository.SaveAsync();
            GetByGidPermitTypeResponse obj = _mapper.Map<GetByGidPermitTypeResponse>(permitType);

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
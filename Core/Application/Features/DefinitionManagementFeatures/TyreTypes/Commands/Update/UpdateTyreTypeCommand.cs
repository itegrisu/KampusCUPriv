using Application.Features.DefinitionManagementFeatures.TyreTypes.Constants;
using Application.Features.DefinitionManagementFeatures.TyreTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.TyreTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.TyreTypeRepo;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using Domain.Enums;

namespace Application.Features.DefinitionManagementFeatures.TyreTypes.Commands.Update;

public class UpdateTyreTypeCommand : IRequest<UpdatedTyreTypeResponse>
{
    public Guid Gid { get; set; }
    public string Title { get; set; }
    public EnumTyreTypeName TyreTypeName { get; set; }
    public string? Size { get; set; }



    public class UpdateTyreTypeCommandHandler : IRequestHandler<UpdateTyreTypeCommand, UpdatedTyreTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITyreTypeWriteRepository _tyreTypeWriteRepository;
        private readonly ITyreTypeReadRepository _tyreTypeReadRepository;
        private readonly TyreTypeBusinessRules _tyreTypeBusinessRules;

        public UpdateTyreTypeCommandHandler(IMapper mapper, ITyreTypeWriteRepository tyreTypeWriteRepository,
                                         TyreTypeBusinessRules tyreTypeBusinessRules, ITyreTypeReadRepository tyreTypeReadRepository)
        {
            _mapper = mapper;
            _tyreTypeWriteRepository = tyreTypeWriteRepository;
            _tyreTypeBusinessRules = tyreTypeBusinessRules;
            _tyreTypeReadRepository = tyreTypeReadRepository;
        }

        public async Task<UpdatedTyreTypeResponse> Handle(UpdateTyreTypeCommand request, CancellationToken cancellationToken)
        {
            X.TyreType? tyreType = await _tyreTypeReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _tyreTypeBusinessRules.TyreTypeShouldExistWhenSelected(tyreType);
            tyreType = _mapper.Map(request, tyreType);

            _tyreTypeWriteRepository.Update(tyreType!);
            await _tyreTypeWriteRepository.SaveAsync();
            GetByGidTyreTypeResponse obj = _mapper.Map<GetByGidTyreTypeResponse>(tyreType);

            return new()
            {
                Title = TyreTypesBusinessMessages.ProcessCompleted,
                Message = TyreTypesBusinessMessages.SuccessCreatedTyreTypeMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
using Application.Features.DefinitionManagementFeatures.TyreTypes.Constants;
using Application.Features.DefinitionManagementFeatures.TyreTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.TyreTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.TyreTypeRepo;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace Application.Features.DefinitionManagementFeatures.TyreTypes.Commands.Create;

public class CreateTyreTypeCommand : IRequest<CreatedTyreTypeResponse>
{

    public string Title { get; set; }
    public EnumTyreTypeName TyreTypeName { get; set; }
    public string? Size { get; set; }



    public class CreateTyreTypeCommandHandler : IRequestHandler<CreateTyreTypeCommand, CreatedTyreTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITyreTypeWriteRepository _tyreTypeWriteRepository;
        private readonly ITyreTypeReadRepository _tyreTypeReadRepository;
        private readonly TyreTypeBusinessRules _tyreTypeBusinessRules;

        public CreateTyreTypeCommandHandler(IMapper mapper, ITyreTypeWriteRepository tyreTypeWriteRepository,
                                         TyreTypeBusinessRules tyreTypeBusinessRules, ITyreTypeReadRepository tyreTypeReadRepository)
        {
            _mapper = mapper;
            _tyreTypeWriteRepository = tyreTypeWriteRepository;
            _tyreTypeBusinessRules = tyreTypeBusinessRules;
            _tyreTypeReadRepository = tyreTypeReadRepository;
        }

        public async Task<CreatedTyreTypeResponse> Handle(CreateTyreTypeCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _tyreTypeReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.TyreType tyreType = _mapper.Map<X.TyreType>(request);
            //tyreType.RowNo = maxRowNo + 1;

            await _tyreTypeWriteRepository.AddAsync(tyreType);
            await _tyreTypeWriteRepository.SaveAsync();

            X.TyreType savedTyreType = await _tyreTypeReadRepository.GetAsync(predicate: x => x.Gid == tyreType.Gid);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidTyreTypeResponse obj = _mapper.Map<GetByGidTyreTypeResponse>(savedTyreType);
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
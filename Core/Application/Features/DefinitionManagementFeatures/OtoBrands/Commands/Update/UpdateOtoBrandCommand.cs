using Application.Features.DefinitionManagementFeatures.OtoBrands.Constants;
using Application.Features.DefinitionManagementFeatures.OtoBrands.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.OtoBrands.Rules;
using Application.Repositories.DefinitionManagementRepos.OtoBrandRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.OtoBrands.Commands.Update;

public class UpdateOtoBrandCommand : IRequest<UpdatedOtoBrandResponse>
{
    public Guid Gid { get; set; }
    public string Name { get; set; }


    public class UpdateOtoBrandCommandHandler : IRequestHandler<UpdateOtoBrandCommand, UpdatedOtoBrandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOtoBrandWriteRepository _otoBrandWriteRepository;
        private readonly IOtoBrandReadRepository _otoBrandReadRepository;
        private readonly OtoBrandBusinessRules _otoBrandBusinessRules;

        public UpdateOtoBrandCommandHandler(IMapper mapper, IOtoBrandWriteRepository otoBrandWriteRepository,
                                         OtoBrandBusinessRules otoBrandBusinessRules, IOtoBrandReadRepository otoBrandReadRepository)
        {
            _mapper = mapper;
            _otoBrandWriteRepository = otoBrandWriteRepository;
            _otoBrandBusinessRules = otoBrandBusinessRules;
            _otoBrandReadRepository = otoBrandReadRepository;
        }

        public async Task<UpdatedOtoBrandResponse> Handle(UpdateOtoBrandCommand request, CancellationToken cancellationToken)
        {
            X.OtoBrand? otoBrand = await _otoBrandReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);

            await _otoBrandBusinessRules.OtoBrandShouldExistWhenSelected(otoBrand);
            await _otoBrandBusinessRules.OtoBrandNameIsUnique(request.Name, request.Gid);
            otoBrand = _mapper.Map(request, otoBrand);

            _otoBrandWriteRepository.Update(otoBrand!);
            await _otoBrandWriteRepository.SaveAsync();
            GetByGidOtoBrandResponse obj = _mapper.Map<GetByGidOtoBrandResponse>(otoBrand);

            return new()
            {
                Title = OtoBrandsBusinessMessages.ProcessCompleted,
                Message = OtoBrandsBusinessMessages.SuccessCreatedOtoBrandMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
using Application.Features.DefinitionManagementFeatures.OtoBrands.Constants;
using Application.Features.DefinitionManagementFeatures.OtoBrands.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.OtoBrands.Rules;
using Application.Repositories.DefinitionManagementRepos.OtoBrandRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.OtoBrands.Commands.Create;

public class CreateOtoBrandCommand : IRequest<CreatedOtoBrandResponse>
{

    public string AracMarkaAdi { get; set; }



    public class CreateOtoBrandCommandHandler : IRequestHandler<CreateOtoBrandCommand, CreatedOtoBrandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOtoBrandWriteRepository _otoBrandWriteRepository;
        private readonly IOtoBrandReadRepository _otoBrandReadRepository;
        private readonly OtoBrandBusinessRules _otoBrandBusinessRules;

        public CreateOtoBrandCommandHandler(IMapper mapper, IOtoBrandWriteRepository otoBrandWriteRepository,
                                         OtoBrandBusinessRules otoBrandBusinessRules, IOtoBrandReadRepository otoBrandReadRepository)
        {
            _mapper = mapper;
            _otoBrandWriteRepository = otoBrandWriteRepository;
            _otoBrandBusinessRules = otoBrandBusinessRules;
            _otoBrandReadRepository = otoBrandReadRepository;
        }

        public async Task<CreatedOtoBrandResponse> Handle(CreateOtoBrandCommand request, CancellationToken cancellationToken)
        {

            await _otoBrandBusinessRules.OtoBrandNameIsUnique(request.AracMarkaAdi);
            X.OtoBrand otoBrand = _mapper.Map<X.OtoBrand>(request);


            await _otoBrandWriteRepository.AddAsync(otoBrand);
            await _otoBrandWriteRepository.SaveAsync();

            X.OtoBrand savedOtoBrand = await _otoBrandReadRepository.GetAsync(predicate: x => x.Gid == otoBrand.Gid);


            GetByGidOtoBrandResponse obj = _mapper.Map<GetByGidOtoBrandResponse>(savedOtoBrand);
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
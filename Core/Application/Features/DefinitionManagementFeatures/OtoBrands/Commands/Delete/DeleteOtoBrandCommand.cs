using Application.Features.DefinitionManagementFeatures.OtoBrands.Constants;
using Application.Features.DefinitionManagementFeatures.OtoBrands.Rules;
using Application.Repositories.DefinitionManagementRepos.OtoBrandRepo;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;

namespace Application.Features.DefinitionManagementFeatures.OtoBrands.Commands.Delete;

public class DeleteOtoBrandCommand : IRequest<DeletedOtoBrandResponse>
{
	public Guid Gid { get; set; }

    public class DeleteOtoBrandCommandHandler : IRequestHandler<DeleteOtoBrandCommand, DeletedOtoBrandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOtoBrandReadRepository _otoBrandReadRepository;
        private readonly IOtoBrandWriteRepository _otoBrandWriteRepository;
        private readonly OtoBrandBusinessRules _otoBrandBusinessRules;

        public DeleteOtoBrandCommandHandler(IMapper mapper, IOtoBrandReadRepository otoBrandReadRepository,
                                         OtoBrandBusinessRules otoBrandBusinessRules, IOtoBrandWriteRepository otoBrandWriteRepository)
        {
            _mapper = mapper;
            _otoBrandReadRepository = otoBrandReadRepository;
            _otoBrandBusinessRules = otoBrandBusinessRules;
            _otoBrandWriteRepository = otoBrandWriteRepository;
        }

        public async Task<DeletedOtoBrandResponse> Handle(DeleteOtoBrandCommand request, CancellationToken cancellationToken)
        {
            X.OtoBrand? otoBrand = await _otoBrandReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _otoBrandBusinessRules.OtoBrandShouldExistWhenSelected(otoBrand);
            otoBrand.DataState = Core.Enum.DataState.Deleted;

            _otoBrandWriteRepository.Update(otoBrand);
            await _otoBrandWriteRepository.SaveAsync();

            return new()
            {
                Title = OtoBrandsBusinessMessages.ProcessCompleted,
                Message = OtoBrandsBusinessMessages.SuccessDeletedOtoBrandMessage,
                IsValid = true
            };
        }
    }
}
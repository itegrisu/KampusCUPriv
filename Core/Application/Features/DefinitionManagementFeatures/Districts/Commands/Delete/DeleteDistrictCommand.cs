using Application.Features.DefinitionManagementFeatures.Districts.Constants;
using Application.Features.DefinitionManagementFeatures.Districts.Rules;
using Application.Repositories.DefinitionManagementRepos.DistrictRepo;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;

namespace Application.Features.DefinitionManagementFeatures.Districts.Commands.Delete;

public class DeleteDistrictCommand : IRequest<DeletedDistrictResponse>
{
	public Guid Gid { get; set; }

    public class DeleteDistrictCommandHandler : IRequestHandler<DeleteDistrictCommand, DeletedDistrictResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDistrictReadRepository _districtReadRepository;
        private readonly IDistrictWriteRepository _districtWriteRepository;
        private readonly DistrictBusinessRules _districtBusinessRules;

        public DeleteDistrictCommandHandler(IMapper mapper, IDistrictReadRepository districtReadRepository,
                                         DistrictBusinessRules districtBusinessRules, IDistrictWriteRepository districtWriteRepository)
        {
            _mapper = mapper;
            _districtReadRepository = districtReadRepository;
            _districtBusinessRules = districtBusinessRules;
            _districtWriteRepository = districtWriteRepository;
        }

        public async Task<DeletedDistrictResponse> Handle(DeleteDistrictCommand request, CancellationToken cancellationToken)
        {
            X.District? district = await _districtReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _districtBusinessRules.DistrictShouldExistWhenSelected(district);
            district.DataState = Core.Enum.DataState.Deleted;

            _districtWriteRepository.Update(district);
            await _districtWriteRepository.SaveAsync();

            return new()
            {
                Title = DistrictsBusinessMessages.ProcessCompleted,
                Message = DistrictsBusinessMessages.SuccessDeletedDistrictMessage,
                IsValid = true
            };
        }
    }
}
using Application.Features.DefinitionManagementFeatures.Cities.Constants;
using Application.Features.DefinitionManagementFeatures.Cities.Rules;
using Application.Repositories.DefinitionManagementRepos.CityRepo;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;

namespace Application.Features.DefinitionManagementFeatures.Cities.Commands.Delete;

public class DeleteCityCommand : IRequest<DeletedCityResponse>
{
	public Guid Gid { get; set; }

    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, DeletedCityResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICityReadRepository _cityReadRepository;
        private readonly ICityWriteRepository _cityWriteRepository;
        private readonly CityBusinessRules _cityBusinessRules;

        public DeleteCityCommandHandler(IMapper mapper, ICityReadRepository cityReadRepository,
                                         CityBusinessRules cityBusinessRules, ICityWriteRepository cityWriteRepository)
        {
            _mapper = mapper;
            _cityReadRepository = cityReadRepository;
            _cityBusinessRules = cityBusinessRules;
            _cityWriteRepository = cityWriteRepository;
        }

        public async Task<DeletedCityResponse> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            X.City? city = await _cityReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _cityBusinessRules.CityShouldExistWhenSelected(city);
            city.DataState = Core.Enum.DataState.Deleted;

            _cityWriteRepository.Update(city);
            await _cityWriteRepository.SaveAsync();

            return new()
            {
                Title = CitiesBusinessMessages.ProcessCompleted,
                Message = CitiesBusinessMessages.SuccessDeletedCityMessage,
                IsValid = true
            };
        }
    }
}
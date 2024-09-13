using Application.Features.DefinitionManagementFeatures.Cities.Constants;
using Application.Features.DefinitionManagementFeatures.Cities.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.Cities.Rules;
using Application.Repositories.DefinitionManagementRepos.CityRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Cities.Commands.Update;

public class UpdateCityCommand : IRequest<UpdatedCityResponse>
{
    public Guid Gid { get; set; }
    public Guid GidUlkeFK { get; set; }
    public string SehirAdi { get; set; }
    public string? PlakaKodu { get; set; }

    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, UpdatedCityResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICityWriteRepository _cityWriteRepository;
        private readonly ICityReadRepository _cityReadRepository;
        private readonly CityBusinessRules _cityBusinessRules;

        public UpdateCityCommandHandler(IMapper mapper, ICityWriteRepository cityWriteRepository,
                                         CityBusinessRules cityBusinessRules, ICityReadRepository cityReadRepository)
        {
            _mapper = mapper;
            _cityWriteRepository = cityWriteRepository;
            _cityBusinessRules = cityBusinessRules;
            _cityReadRepository = cityReadRepository;
        }

        public async Task<UpdatedCityResponse> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            X.City? city = await _cityReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _cityBusinessRules.CityShouldExistWhenSelected(city);
            await _cityBusinessRules.CountryShouldExistWhenSelected(request.GidUlkeFK);
            await _cityBusinessRules.CheckCityNameIsUnique(request.SehirAdi, request.Gid);

            city = _mapper.Map(request, city);

            _cityWriteRepository.Update(city!);
            await _cityWriteRepository.SaveAsync();
            X.City updatedCity = await _cityReadRepository.GetAsync(predicate: x => x.Gid == city.Gid, include: x => x.Include(x => x.CountryFK));

            GetByGidCityResponse obj = _mapper.Map<GetByGidCityResponse>(updatedCity);

            return new()
            {
                Title = CitiesBusinessMessages.ProcessCompleted,
                Message = CitiesBusinessMessages.SuccessCreatedCityMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
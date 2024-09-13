using Application.Features.DefinitionManagementFeatures.Cities.Constants;
using Application.Features.DefinitionManagementFeatures.Cities.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.Cities.Rules;
using Application.Repositories.DefinitionManagementRepos.CityRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Cities.Commands.Create;

public class CreateCityCommand : IRequest<CreatedCityResponse>
{
    public Guid GidUlkeFK { get; set; }
    public string SehirAdi { get; set; }
    public string? PlakaKodu { get; set; }



    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, CreatedCityResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICityWriteRepository _cityWriteRepository;
        private readonly ICityReadRepository _cityReadRepository;
        private readonly CityBusinessRules _cityBusinessRules;

        public CreateCityCommandHandler(IMapper mapper, ICityWriteRepository cityWriteRepository,
                                         CityBusinessRules cityBusinessRules, ICityReadRepository cityReadRepository)
        {
            _mapper = mapper;
            _cityWriteRepository = cityWriteRepository;
            _cityBusinessRules = cityBusinessRules;
            _cityReadRepository = cityReadRepository;
        }

        public async Task<CreatedCityResponse> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {

            await _cityBusinessRules.CountryShouldExistWhenSelected(request.GidUlkeFK);
            await _cityBusinessRules.CheckCityNameIsUnique(request.SehirAdi);

            X.City city = _mapper.Map<X.City>(request);

            await _cityWriteRepository.AddAsync(city);
            await _cityWriteRepository.SaveAsync();

            X.City savedCity = await _cityReadRepository.GetAsync(predicate: x => x.Gid == city.Gid, include: x => x.Include(x => x.CountryFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidCityResponse obj = _mapper.Map<GetByGidCityResponse>(savedCity);
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
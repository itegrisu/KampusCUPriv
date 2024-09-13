using Application.Features.DefinitionManagementFeatures.Countries.Commands.Create;
using Application.Features.DefinitionManagementFeatures.Countries.Rules;
using Application.Helpers.UpdateRowNo;
using Application.Repositories.DefinitionManagementRepos.CountryRepo;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;

namespace Application.Features.DefinitionManagementFeatures.Countries.Commands.UpdateRowNo
{
    public class UpdateRowNoCountryCommand : IRequest<UpdateRowNoCountryResponse>
    {
        public Guid Gid { get; set; }
        public bool IsUp { get; set; }

        public class UpdateRowNoCountryCommandHandler : IRequestHandler<UpdateRowNoCountryCommand, UpdateRowNoCountryResponse>
        {
            private readonly IMapper _mapper;
            private readonly ICountryWriteRepository _countryWriteRepository;
            private readonly ICountryReadRepository _countryReadRepository;
            private readonly CountryBusinessRules _countryBusinessRules;
            private readonly UpdateRowNoHelper<UpdateRowNoCountryResponse, X.Country> _updateRowNoHelper;

            public UpdateRowNoCountryCommandHandler(IMapper mapper, ICountryWriteRepository countryWriteRepository,
                                             CountryBusinessRules countryBusinessRules, ICountryReadRepository countryReadRepository, UpdateRowNoHelper<UpdateRowNoCountryResponse, X.Country> updateRowNoHelper)
            {
                _mapper = mapper;
                _countryWriteRepository = countryWriteRepository;
                _countryBusinessRules = countryBusinessRules;
                _countryReadRepository = countryReadRepository;
                _updateRowNoHelper = updateRowNoHelper;
            }

            public async Task<UpdateRowNoCountryResponse> Handle(UpdateRowNoCountryCommand request, CancellationToken cancellationToken)
            {
                List<X.Country> lst = _countryReadRepository.GetAll().OrderBy(a => a.RowNo).ToList();

                X.Country select = lst.Where(a => a.Gid == request.Gid).FirstOrDefault();

                UpdateRowNoCountryResponse response = await _updateRowNoHelper.UpdateRowNo(lst, select, request.IsUp);

                await _countryWriteRepository.SaveAsync();

                return response;
            }
        }
    }
}
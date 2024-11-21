using Application.Features.DefinitionManagementFeatures.Districts.Constants;
using Application.Features.DefinitionManagementFeatures.Districts.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.Districts.Rules;
using Application.Repositories.DefinitionManagementRepos.DistrictRepo;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.DefinitionManagementFeatures.Districts.Commands.Create;

public class CreateDistrictCommand : IRequest<CreatedDistrictResponse>
{
    public Guid GidCityFK { get; set; }

    public int DistrictCode { get; set; }
    public string DistrictName { get; set; }



    public class CreateDistrictCommandHandler : IRequestHandler<CreateDistrictCommand, CreatedDistrictResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDistrictWriteRepository _districtWriteRepository;
        private readonly IDistrictReadRepository _districtReadRepository;
        private readonly DistrictBusinessRules _districtBusinessRules;

        public CreateDistrictCommandHandler(IMapper mapper, IDistrictWriteRepository districtWriteRepository,
                                         DistrictBusinessRules districtBusinessRules, IDistrictReadRepository districtReadRepository)
        {
            _mapper = mapper;
            _districtWriteRepository = districtWriteRepository;
            _districtBusinessRules = districtBusinessRules;
            _districtReadRepository = districtReadRepository;
        }

        public async Task<CreatedDistrictResponse> Handle(CreateDistrictCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _districtReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.District district = _mapper.Map<X.District>(request);
            //district.RowNo = maxRowNo + 1;

            await _districtWriteRepository.AddAsync(district);
            await _districtWriteRepository.SaveAsync();

            X.District savedDistrict = await _districtReadRepository.GetAsync(predicate: x => x.Gid == district.Gid,
                include: x => x.Include(x => x.CityFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidDistrictResponse obj = _mapper.Map<GetByGidDistrictResponse>(savedDistrict);
            return new()
            {
                Title = DistrictsBusinessMessages.ProcessCompleted,
                Message = DistrictsBusinessMessages.SuccessCreatedDistrictMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
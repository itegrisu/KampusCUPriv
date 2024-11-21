using Application.Features.DefinitionManagementFeatures.Districts.Constants;
using Application.Features.DefinitionManagementFeatures.Districts.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.Districts.Rules;
using Application.Repositories.DefinitionManagementRepos.DistrictRepo;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.DefinitionManagementFeatures.Districts.Commands.Update;

public class UpdateDistrictCommand : IRequest<UpdatedDistrictResponse>
{
    public Guid Gid { get; set; }

	public Guid GidCityFK { get; set; }

public int DistrictCode { get; set; }
public string DistrictName { get; set; }



    public class UpdateDistrictCommandHandler : IRequestHandler<UpdateDistrictCommand, UpdatedDistrictResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDistrictWriteRepository _districtWriteRepository;
        private readonly IDistrictReadRepository _districtReadRepository;
        private readonly DistrictBusinessRules _districtBusinessRules;

        public UpdateDistrictCommandHandler(IMapper mapper, IDistrictWriteRepository districtWriteRepository,
                                         DistrictBusinessRules districtBusinessRules, IDistrictReadRepository districtReadRepository)
        {
            _mapper = mapper;
            _districtWriteRepository = districtWriteRepository;
            _districtBusinessRules = districtBusinessRules;
            _districtReadRepository = districtReadRepository;
        }

        public async Task<UpdatedDistrictResponse> Handle(UpdateDistrictCommand request, CancellationToken cancellationToken)
        {
            X.District? district = await _districtReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken,
                  include: x => x.Include(x => x.CityFK));
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _districtBusinessRules.DistrictShouldExistWhenSelected(district);
            district = _mapper.Map(request, district);

            _districtWriteRepository.Update(district!);
            await _districtWriteRepository.SaveAsync();
            GetByGidDistrictResponse obj = _mapper.Map<GetByGidDistrictResponse>(district);

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
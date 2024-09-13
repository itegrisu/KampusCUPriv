using Application.Features.PortalManagementFeatures.PortalParameters.Constants;
using Application.Features.PortalManagementFeatures.PortalParameters.Queries.GetByGid;
using Application.Features.PortalManagementFeatures.PortalParameters.Rules;
using Application.Repositories.PortalManagementRepos.PortalParameterRepo;
using AutoMapper;
using X = Domain.Entities.PortalManagements;
using MediatR;
using Domain.Enums;

namespace Application.Features.PortalManagementFeatures.PortalParameters.Commands.Update;

public class UpdatePortalParameterCommand : IRequest<UpdatedPortalParameterResponse>
{
    public Guid Gid { get; set; }

	
public string Name { get; set; }
public EnumParameterValueType ParameterValueType { get; set; }
public string? StringValue { get; set; }
public int? IntegerValue { get; set; }
public decimal? DecimalValue { get; set; }
public DateTime? DateTimeValue { get; set; }
public string? Description { get; set; }



    public class UpdatePortalParameterCommandHandler : IRequestHandler<UpdatePortalParameterCommand, UpdatedPortalParameterResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPortalParameterWriteRepository _portalParameterWriteRepository;
        private readonly IPortalParameterReadRepository _portalParameterReadRepository;
        private readonly PortalParameterBusinessRules _portalParameterBusinessRules;

        public UpdatePortalParameterCommandHandler(IMapper mapper, IPortalParameterWriteRepository portalParameterWriteRepository,
                                         PortalParameterBusinessRules portalParameterBusinessRules, IPortalParameterReadRepository portalParameterReadRepository)
        {
            _mapper = mapper;
            _portalParameterWriteRepository = portalParameterWriteRepository;
            _portalParameterBusinessRules = portalParameterBusinessRules;
            _portalParameterReadRepository = portalParameterReadRepository;
        }

        public async Task<UpdatedPortalParameterResponse> Handle(UpdatePortalParameterCommand request, CancellationToken cancellationToken)
        {
            X.PortalParameter? portalParameter = await _portalParameterReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _portalParameterBusinessRules.PortalParameterShouldExistWhenSelected(portalParameter);
            portalParameter = _mapper.Map(request, portalParameter);

            _portalParameterWriteRepository.Update(portalParameter!);
            await _portalParameterWriteRepository.SaveAsync();
            GetByGidPortalParameterResponse obj = _mapper.Map<GetByGidPortalParameterResponse>(portalParameter);

            return new()
            {
                Title = PortalParametersBusinessMessages.ProcessCompleted,
                Message = PortalParametersBusinessMessages.SuccessUpdatedPortalParameterMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
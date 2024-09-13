using Application.Features.PortalManagementFeatures.PortalParameters.Constants;
using Application.Features.PortalManagementFeatures.PortalParameters.Queries.GetByGid;
using Application.Features.PortalManagementFeatures.PortalParameters.Rules;
using Application.Repositories.PortalManagementRepos.PortalParameterRepo;
using AutoMapper;
using X = Domain.Entities.PortalManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace Application.Features.PortalManagementFeatures.PortalParameters.Commands.Create;

public class CreatePortalParameterCommand : IRequest<CreatedPortalParameterResponse>
{
    
public string Name { get; set; }
public EnumParameterValueType ParameterValueType { get; set; }
public string? StringValue { get; set; }
public int? IntegerValue { get; set; }
public decimal? DecimalValue { get; set; }
public DateTime? DateTimeValue { get; set; }
public string? Description { get; set; }



    public class CreatePortalParameterCommandHandler : IRequestHandler<CreatePortalParameterCommand, CreatedPortalParameterResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPortalParameterWriteRepository _portalParameterWriteRepository;
        private readonly IPortalParameterReadRepository _portalParameterReadRepository;
        private readonly PortalParameterBusinessRules _portalParameterBusinessRules;

        public CreatePortalParameterCommandHandler(IMapper mapper, IPortalParameterWriteRepository portalParameterWriteRepository,
                                         PortalParameterBusinessRules portalParameterBusinessRules, IPortalParameterReadRepository portalParameterReadRepository)
        {
            _mapper = mapper;
            _portalParameterWriteRepository = portalParameterWriteRepository;
            _portalParameterBusinessRules = portalParameterBusinessRules;
            _portalParameterReadRepository = portalParameterReadRepository;
        }

        public async Task<CreatedPortalParameterResponse> Handle(CreatePortalParameterCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _portalParameterReadRepository.GetAll().MaxAsync(r => r.RowNo);
			X.PortalParameter portalParameter = _mapper.Map<X.PortalParameter>(request);
            //portalParameter.RowNo = maxRowNo + 1;

            await _portalParameterWriteRepository.AddAsync(portalParameter);
            await _portalParameterWriteRepository.SaveAsync();

			X.PortalParameter savedPortalParameter = await _portalParameterReadRepository.GetAsync(predicate: x => x.Gid == portalParameter.Gid);
			//INCLUDES Buraya Gelecek include varsa eklenecek
			//include: x => x.Include(x => x.UserFK));

            GetByGidPortalParameterResponse obj = _mapper.Map<GetByGidPortalParameterResponse>(savedPortalParameter);
            return new()
            {           
                Title = PortalParametersBusinessMessages.ProcessCompleted,
                Message = PortalParametersBusinessMessages.SuccessCreatedPortalParameterMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
using Application.Abstractions;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Commands.Delete;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Constants;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Rules;
using Application.Repositories.TransportationRepos.TransportationGroupRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementsFeatures.TransportationGroups.Commands.CancelReport
{
    public class CancelTransportationGroupCommand : IRequest<CanceledTransportationGroupResponse>
    {
        public string RefNoTransportationGroup { get; set; }

        public class CancelTransportationGroupCommandHandler : IRequestHandler<CancelTransportationGroupCommand, CanceledTransportationGroupResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITransportationGroupReadRepository _transportationGroupReadRepository;
            private readonly ITransportationGroupWriteRepository _transportationGroupWriteRepository;
            private readonly TransportationGroupBusinessRules _transportationGroupBusinessRules;
            private readonly IUlasımService _ulasımService;
            public CancelTransportationGroupCommandHandler(IMapper mapper, ITransportationGroupReadRepository transportationGroupReadRepository,
                                             TransportationGroupBusinessRules transportationGroupBusinessRules, ITransportationGroupWriteRepository transportationGroupWriteRepository, IUlasımService ulasımService)
            {
                _mapper = mapper;
                _transportationGroupReadRepository = transportationGroupReadRepository;
                _transportationGroupBusinessRules = transportationGroupBusinessRules;
                _transportationGroupWriteRepository = transportationGroupWriteRepository;
                _ulasımService = ulasımService;
            }

            public async Task<CanceledTransportationGroupResponse> Handle(CancelTransportationGroupCommand request, CancellationToken cancellationToken)
            {
                X.TransportationGroup? transportationGroup = await _transportationGroupReadRepository.GetAsync(include: x => x.Include(x => x.TransportationServiceFK), predicate: x => x.RefNoTransportationGroup == request.RefNoTransportationGroup, cancellationToken: cancellationToken);
                await _transportationGroupBusinessRules.TransportationGroupShouldExistWhenSelected(transportationGroup);

                var response = await _ulasımService.GrupIptalAsync(long.Parse(transportationGroup.TransportationServiceFK.RefNoTransportation),long.Parse(transportationGroup.RefNoTransportationGroup));

                if (!response.Contains("HATA"))
                {
                    transportationGroup.RefNoTransportationGroup = null;

                    _transportationGroupWriteRepository.Update(transportationGroup);
                    await _transportationGroupWriteRepository.SaveAsync();

                    return new()
                    {
                        Title = TransportationGroupsBusinessMessages.ProcessCompleted,
                        Message = TransportationGroupsBusinessMessages.SuccessCanceledTransportationGroupMessage,
                        IsValid = true
                    };
                }
              
                return new()
                {
                    Title = TransportationGroupsBusinessMessages.TechnicalError,
                    Message = response,
                    IsValid = false
                };
            }
        }
    }
}

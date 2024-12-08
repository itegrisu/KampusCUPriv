using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Commands.Delete;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Constants;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Rules;
using Application.Repositories.TransportationRepos.TransportationPersonnelRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementsFeatures.TransportationPersonnels.Commands.CancelReport
{
    public class CancelTransportationPersonnelCommand : IRequest<CanceledTransportationPersonnelResponse>
    {
        public Guid Gid { get; set; }

        public class CancelTransportationPersonnelCommandHandler : IRequestHandler<CancelTransportationPersonnelCommand, CanceledTransportationPersonnelResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITransportationPersonnelReadRepository _transportationPersonnelReadRepository;
            private readonly ITransportationPersonnelWriteRepository _transportationPersonnelWriteRepository;
            private readonly TransportationPersonnelBusinessRules _transportationPersonnelBusinessRules;
            private readonly IUlasımService _ulasımService;
            public CancelTransportationPersonnelCommandHandler(IMapper mapper, ITransportationPersonnelReadRepository transportationPersonnelReadRepository,
                                             TransportationPersonnelBusinessRules transportationPersonnelBusinessRules, ITransportationPersonnelWriteRepository transportationPersonnelWriteRepository, IUlasımService ulasımService)
            {
                _mapper = mapper;
                _transportationPersonnelReadRepository = transportationPersonnelReadRepository;
                _transportationPersonnelBusinessRules = transportationPersonnelBusinessRules;
                _transportationPersonnelWriteRepository = transportationPersonnelWriteRepository;
                _ulasımService = ulasımService;
            }

            public async Task<CanceledTransportationPersonnelResponse> Handle(CancelTransportationPersonnelCommand request, CancellationToken cancellationToken)
            {
                X.TransportationPersonnel? transportationPersonnel = await _transportationPersonnelReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.TransportationServiceFK).Include(x => x.UserFK));
                await _transportationPersonnelBusinessRules.TransportationPersonnelShouldExistWhenSelected(transportationPersonnel);

                var response = await _ulasımService.PersonelIptalAsync(transportationPersonnel, long.Parse(transportationPersonnel.TransportationServiceFK.RefNoTransportation));

                if (!response.Contains("HATA"))
                {
                    transportationPersonnel.StaffStatus = EnumStaffStatus.Iptal;

                    _transportationPersonnelWriteRepository.Update(transportationPersonnel);
                    await _transportationPersonnelWriteRepository.SaveAsync();

                    return new()
                    {
                        Title = TransportationPersonnelsBusinessMessages.ProcessCompleted,
                        Message = TransportationPersonnelsBusinessMessages.SuccessReportCancelTransportationPersonnelMessage,
                        IsValid = true
                    };
                }

                return new()
                {
                    Title = TransportationPersonnelsBusinessMessages.TechnicalError,
                    Message = response,
                    IsValid = false
                };

            }
        }
    }
}

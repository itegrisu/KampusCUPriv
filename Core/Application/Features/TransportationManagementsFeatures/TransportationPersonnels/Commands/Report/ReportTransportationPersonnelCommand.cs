using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Commands.Update;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Constants;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Rules;
using Application.Repositories.TransportationRepos.TransportationPersonnelRepo;
using Application.Repositories.TransportationRepos.TransportationServiceRepo;
using AutoMapper;
using Domain.Entities.SupplierCustomerManagements;
using Domain.Entities.TransportationManagements;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;
namespace Application.Features.TransportationManagementsFeatures.TransportationPersonnels.Commands.Report
{
    public class ReportTransportationPersonnelCommand : IRequest<ReportedTransportationPersonnelResponse>
    {
        public List<TransportationPersonnel> PersonnelList { get; set; }

        public class ReportTransportationPersonnelCommandHandler : IRequestHandler<ReportTransportationPersonnelCommand, ReportedTransportationPersonnelResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITransportationPersonnelWriteRepository _transportationPersonnelWriteRepository;
            private readonly ITransportationPersonnelReadRepository _transportationPersonnelReadRepository;
            private readonly TransportationPersonnelBusinessRules _transportationPersonnelBusinessRules;
            private readonly IUlasımService _ulasımService;
            private readonly ITransportationServiceReadRepository _transportationServiceReadRepository;
            public ReportTransportationPersonnelCommandHandler(IMapper mapper, ITransportationPersonnelWriteRepository transportationPersonnelWriteRepository,
                                             TransportationPersonnelBusinessRules transportationPersonnelBusinessRules, ITransportationPersonnelReadRepository transportationPersonnelReadRepository, IUlasımService ulasımService, ITransportationServiceReadRepository transportationServiceReadRepository)
            {
                _mapper = mapper;
                _transportationPersonnelWriteRepository = transportationPersonnelWriteRepository;
                _transportationPersonnelBusinessRules = transportationPersonnelBusinessRules;
                _transportationPersonnelReadRepository = transportationPersonnelReadRepository;
                _ulasımService = ulasımService;
                _transportationServiceReadRepository = transportationServiceReadRepository;
            }

            public async Task<ReportedTransportationPersonnelResponse> Handle(ReportTransportationPersonnelCommand request, CancellationToken cancellationToken)
            {
                var updatedPersonnelList = new List<GetByGidTransportationPersonnelResponse>();
                var reportPersonnelList = new List<TransportationPersonnel>();
                foreach (var personnelDto in request.PersonnelList)
                {
                    X.TransportationPersonnel? transportationPersonnel = await _transportationPersonnelReadRepository
                        .GetAsync(predicate: x => x.Gid == personnelDto.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.TransportationServiceFK).Include(x => x.UserFK).Include(x => x.UserFK).ThenInclude(x => x.CountryFK));

                    reportPersonnelList.Add(transportationPersonnel);

                    await _transportationPersonnelBusinessRules.TransportationPersonnelShouldExistWhenSelected(transportationPersonnel);
                }

                var service = await _transportationServiceReadRepository.GetSingleAsync(x => x.Gid == request.PersonnelList[0].GidTransportationServiceFK);
                // Buraya sonra bak boyle olmamali
                var sonuc = await _ulasımService.PersonelEkleAsync(reportPersonnelList, long.Parse(service.RefNoTransportation));

                if (!sonuc.Contains("HATA"))
                {
                    return new()
                    {
                        Title = TransportationPersonnelsBusinessMessages.ProcessCompleted,
                        Message = TransportationPersonnelsBusinessMessages.SuccessReportedTransportationPersonnelMessage,
                        IsValid = true,
                    };
                }

                    return new()
                {
                    Title = TransportationPersonnelsBusinessMessages.ProcessCompleted,
                    Message = sonuc,
                    IsValid = false,
                };
            }

        }
    }
}

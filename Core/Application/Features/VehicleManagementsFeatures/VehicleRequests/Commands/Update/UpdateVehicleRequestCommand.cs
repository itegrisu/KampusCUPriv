using Application.Features.VehicleManagementFeatures.VehicleRequests.Constants;
using Application.Features.VehicleManagementFeatures.VehicleRequests.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleRequests.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleRequestRepo;
using AutoMapper;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleRequests.Commands.Update;

public class UpdateVehicleRequestCommand : IRequest<UpdatedVehicleRequestResponse>
{
    public Guid Gid { get; set; }
    public Guid GidVehicleFK { get; set; }
    public Guid GidRequestUserFK { get; set; }
    public Guid? GidApprovedUserFK { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string UseAim { get; set; }
    public EnumVehicleApprovedStatus VehicleApprovedStatus { get; set; }

    public class UpdateVehicleRequestCommandHandler : IRequestHandler<UpdateVehicleRequestCommand, UpdatedVehicleRequestResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRequestWriteRepository _vehicleRequestWriteRepository;
        private readonly IVehicleRequestReadRepository _vehicleRequestReadRepository;
        private readonly VehicleRequestBusinessRules _vehicleRequestBusinessRules;

        public UpdateVehicleRequestCommandHandler(IMapper mapper, IVehicleRequestWriteRepository vehicleRequestWriteRepository,
                                         VehicleRequestBusinessRules vehicleRequestBusinessRules, IVehicleRequestReadRepository vehicleRequestReadRepository)
        {
            _mapper = mapper;
            _vehicleRequestWriteRepository = vehicleRequestWriteRepository;
            _vehicleRequestBusinessRules = vehicleRequestBusinessRules;
            _vehicleRequestReadRepository = vehicleRequestReadRepository;
        }

        public async Task<UpdatedVehicleRequestResponse> Handle(UpdateVehicleRequestCommand request, CancellationToken cancellationToken)
        {
            X.VehicleRequest? vehicleRequest = await _vehicleRequestReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.VehicleAllFK).Include(x => x.RequestUserFK).Include(x => x.ApprovedUserFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _vehicleRequestBusinessRules.VehicleRequestShouldExistWhenSelected(vehicleRequest);
            vehicleRequest = _mapper.Map(request, vehicleRequest);

            // Eðer talep onaylanacaksa, araç uygun mu kontrol ediyoruz
            if (request.VehicleApprovedStatus == EnumVehicleApprovedStatus.Onaylandi)
            {
                // Tarih çakýþmasý kontrolü
                bool isVehicleAvailable = await _vehicleRequestBusinessRules.CheckVehicleAvailability(
                    request.GidVehicleFK,
                    request.StartDate,
                    request.EndDate
                );

                if (!isVehicleAvailable)
                {
                    throw new BusinessException("Bu araç belirtilen tarihlerde baþka bir sefer veya onaylanmis talep ile çakýþýyor.");
                }
            }

            _vehicleRequestWriteRepository.Update(vehicleRequest!);
            await _vehicleRequestWriteRepository.SaveAsync();
            GetByGidVehicleRequestResponse obj = _mapper.Map<GetByGidVehicleRequestResponse>(vehicleRequest);

            return new()
            {
                Title = VehicleRequestsBusinessMessages.ProcessCompleted,
                Message = VehicleRequestsBusinessMessages.SuccessCreatedVehicleRequestMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
using Application.Abstractions;
using Application.Abstractions.UnitOfWork;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Rules;
using Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Update;
using Application.Features.TransportationManagementFeatures.TransportationServices.Constants;
using Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationServices.Rules;
using Application.Repositories.TransportationRepos.TransportationGroupRepo;
using Application.Repositories.TransportationRepos.TransportationServiceRepo;
using AutoMapper;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.TransportationManagements;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.UpdateServiceWithGroup
{
    public class UpdateServiceWithGroupTransportationServiceCommand : IRequest<UpdateServiceWithGroupTransportationServiceResponse>
    {
        public Guid Gid { get; set; }
        public Guid GidTransportationFK { get; set; }
        public Guid GidVehicleFK { get; set; }
        public string ServiceNo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? StartKM { get; set; }
        public int? EndKM { get; set; }
        public string? VehiclePhone { get; set; }
        public EnumTransportationServiceStatus TransportationServiceStatus { get; set; }
        public string? TransportationFile { get; set; }
        public string? Description { get; set; }
        public string? RefNoTransportation { get; set; }

        public Guid GroupGid { get; set; }
        public Guid GidTransportationServiceFK { get; set; }
        public Guid GidStartCountryFK { get; set; }
        public Guid GidStartCityFK { get; set; }
        public Guid GidStartDistrictFK { get; set; }
        public Guid GidEndCountryFK { get; set; }
        public Guid GidEndCityFK { get; set; }
        public Guid GidEndDistrictFK { get; set; }
        public string GroupName { get; set; }
        public decimal TransportationFee { get; set; }
        public string StartPlace { get; set; }
        public string EndPlace { get; set; }
        public string? GroupDescription { get; set; }

        public class UpdateServiceWithGroupTransportationServiceCommandHandler : IRequestHandler<UpdateServiceWithGroupTransportationServiceCommand, UpdateServiceWithGroupTransportationServiceResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITransportationServiceWriteRepository _transportationServiceWriteRepository;
            private readonly ITransportationServiceReadRepository _transportationServiceReadRepository;
            private readonly ITransportationGroupWriteRepository _transportationGroupWriteRepository;
            private readonly ITransportationGroupReadRepository _transportationGroupReadRepository;
            private readonly TransportationServiceBusinessRules _transportationServiceBusinessRules;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUlasımService _ulasımService;
            public UpdateServiceWithGroupTransportationServiceCommandHandler(IMapper mapper, ITransportationServiceWriteRepository transportationServiceWriteRepository,
                                             TransportationServiceBusinessRules transportationServiceBusinessRules, ITransportationServiceReadRepository transportationServiceReadRepository, ITransportationGroupWriteRepository transportationGroupWriteRepository, IUnitOfWork unitOfWork, ITransportationGroupReadRepository transportationGroupReadRepository, IUlasımService ulasımService)
            {
                _mapper = mapper;
                _transportationServiceWriteRepository = transportationServiceWriteRepository;
                _transportationServiceBusinessRules = transportationServiceBusinessRules;
                _transportationServiceReadRepository = transportationServiceReadRepository;
                _transportationGroupWriteRepository = transportationGroupWriteRepository;
                _unitOfWork = unitOfWork;
                _transportationGroupReadRepository = transportationGroupReadRepository;
                _ulasımService = ulasımService;
            }

            public async Task<UpdateServiceWithGroupTransportationServiceResponse> Handle(UpdateServiceWithGroupTransportationServiceCommand request, CancellationToken cancellationToken)
            {
                // Transaction başlatılır
                _unitOfWork.BeginTransaction();

                // TransportationService güncellenmesi için verileri al ve kontrol et
                X.TransportationService? transportationService = await _transportationServiceReadRepository.GetAsync(
                    predicate: x => x.Gid == request.Gid,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.TransportationFK).Include(x => x.VehicleAllFK)
                );

                try
                {

                    await _transportationServiceBusinessRules.TransportationServiceShouldExistWhenSelected(transportationService);

                    // Güncellenmiş verileri map et
                    transportationService = _mapper.Map(request, transportationService);

                    X.TransportationGroup updateTransportationGroup = await _transportationGroupReadRepository.GetAsync(predicate: x => x.GidTransportationServiceFK == request.Gid);

                    if(request.RefNoTransportation != null)
                    {
                        var response = await _ulasımService.SeferGuncelleAsync(transportationService, long.Parse(transportationService.RefNoTransportation));

                        if (!response.Contains("HATA"))
                        {
                            transportationService.RefNoTransportation = response;
                        }
                        else
                        {
                            throw new BusinessException($"Transaction sırasında bir hata oluştu: {response}");
                        }
                    }
                  

                    // TransportationService güncellenir
                    _transportationServiceWriteRepository.Update(transportationService!);
                    await _transportationServiceWriteRepository.SaveAsync();

                    X.TransportationGroup? transportationGroup = await _transportationGroupReadRepository.GetAsync(predicate: x => x.GidTransportationServiceFK == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.StartCountryFK).Include(x => x.StartCityFK).Include(x => x.StartDistrictFK).Include(x => x.EndCountryFK).Include(x => x.EndCityFK).Include(x => x.EndDistrictFK).Include(x => x.TransportationServiceFK));

                    // Gelen request'ten TransportationGroup için gerekli güncellemeler yapılır
                    transportationGroup.GroupName = request.GroupName;
                    transportationGroup.TransportationFee = request.TransportationFee;
                    transportationGroup.StartPlace = request.StartPlace;
                    transportationGroup.EndPlace = request.EndPlace;
                    transportationGroup.Description = request.GroupDescription;
                    transportationGroup.GidStartCountryFK = request.GidStartCountryFK;
                    transportationGroup.GidStartCityFK = request.GidStartCityFK;
                    transportationGroup.GidStartDistrictFK = request.GidStartDistrictFK;
                    transportationGroup.GidEndCountryFK = request.GidEndCountryFK;
                    transportationGroup.GidEndCityFK = request.GidEndCityFK;
                    transportationGroup.GidEndDistrictFK = request.GidEndDistrictFK;

                    _transportationGroupWriteRepository.Update(transportationGroup!);
                    await _transportationGroupWriteRepository.SaveAsync();

              


                    // Tüm değişiklikler kaydedilir
                    await _unitOfWork.CommitAsync();
                }
                catch (Exception ex)
                {
                    // Hata durumunda rollback yapılır
                    await _unitOfWork.RollbackAsync();
                    throw new BusinessException("Transaction sırasında bir hata oluştu.", ex);
                }
                finally
                {
                    // Transaction nesnesi dispose edilir
                    _unitOfWork.Dispose();
                }

                // Response nesnesi hazırlanır
                GetByGidTransportationServiceResponse obj = _mapper.Map<GetByGidTransportationServiceResponse>(transportationService);

                return new()
                {
                    Title = TransportationServicesBusinessMessages.ProcessCompleted,
                    Message = TransportationServicesBusinessMessages.SuccessUpdatedTransportationServiceMessage,
                    IsValid = true,
                    Obj = obj
                };
            }

        }
    }
}

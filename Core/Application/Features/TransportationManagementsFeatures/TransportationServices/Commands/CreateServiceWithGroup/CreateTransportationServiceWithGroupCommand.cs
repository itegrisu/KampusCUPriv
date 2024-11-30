using Application.Abstractions.UnitOfWork;
using Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Create;
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

namespace Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.CreateServiceWithGroup
{
    public class CreateTransportationServiceWithGroupCommand : IRequest<CreatedTransportationServiceWithGroupResponse>
    {
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


        public class CreateTransportationServiceWithGroupCommandHandler : IRequestHandler<CreateTransportationServiceWithGroupCommand, CreatedTransportationServiceWithGroupResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITransportationServiceWriteRepository _transportationServiceWriteRepository;
            private readonly ITransportationGroupWriteRepository _transportationGroupWriteRepository;
            private readonly ITransportationServiceReadRepository _transportationServiceReadRepository;
            private readonly TransportationServiceBusinessRules _transportationServiceBusinessRules;
            private readonly IUnitOfWork _unitOfWork;
            public CreateTransportationServiceWithGroupCommandHandler(IMapper mapper, ITransportationServiceWriteRepository transportationServiceWriteRepository,
                                             TransportationServiceBusinessRules transportationServiceBusinessRules, ITransportationServiceReadRepository transportationServiceReadRepository, IUnitOfWork unitOfWork, ITransportationGroupWriteRepository transportationGroupWriteRepository)
            {
                _mapper = mapper;
                _transportationServiceWriteRepository = transportationServiceWriteRepository;
                _transportationServiceBusinessRules = transportationServiceBusinessRules;
                _transportationServiceReadRepository = transportationServiceReadRepository;
                _unitOfWork = unitOfWork;
                _transportationGroupWriteRepository = transportationGroupWriteRepository;
            }

            public async Task<CreatedTransportationServiceWithGroupResponse> Handle(CreateTransportationServiceWithGroupCommand request, CancellationToken cancellationToken)
            {    // Transaction başlatılır
                _unitOfWork.BeginTransaction();
                    var transportationService = _mapper.Map<X.TransportationService>(request);
                try
                {
                    // İlk entity için veriler hazırlanır ve kaydedilir
                    await _transportationServiceWriteRepository.AddAsync(transportationService);
                    await _transportationServiceWriteRepository.SaveAsync();

                    X.TransportationService savedTransportationService = await _transportationServiceReadRepository.GetAsync(predicate: x => x.Gid == transportationService.Gid,
             include: x => x.Include(x => x.TransportationFK).Include(x => x.VehicleAllFK));

                    GetByGidTransportationServiceResponse responseObj = _mapper.Map<GetByGidTransportationServiceResponse>(savedTransportationService);

                    // İlk entity kaydedildikten sonra oluşan Gid alınır
                    var transportationServiceGid = responseObj.Gid;

                    // İkinci entity için veriler hazırlanır ve ilişkilendirilir
                    var transportationGroup = new X.TransportationGroup
                    {
                        GidTransportationServiceFK = transportationServiceGid,
                        GroupName = request.GroupName,
                        TransportationFee = request.TransportationFee,
                        StartPlace = request.StartPlace,
                        EndPlace = request.EndPlace,
                        Description = request.GroupDescription,
                        GidStartCountryFK = request.GidStartCountryFK,
                        GidStartCityFK = request.GidStartCityFK,
                        GidStartDistrictFK = request.GidStartDistrictFK,
                        GidEndCountryFK = request.GidEndCountryFK,
                        GidEndCityFK = request.GidEndCityFK,
                        GidEndDistrictFK = request.GidEndDistrictFK
                    };
                    await _transportationGroupWriteRepository.AddAsync(transportationGroup);

                    // Değişiklikler kaydedilir ve transaction tamamlanır
                    await _transportationServiceWriteRepository.SaveAsync();
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
                    _unitOfWork.Dispose();
                }

                GetByGidTransportationServiceResponse obj = _mapper.Map<GetByGidTransportationServiceResponse>(transportationService);
                
                return new()
                {
                    Title = TransportationServicesBusinessMessages.ProcessCompleted,
                    Message = TransportationServicesBusinessMessages.SuccessCreatedTransportationServiceMessage,
                    IsValid = true,
                    Obj = obj
                };
            }
        }
    }
}

using Application.Features.VehicleManagementFeatures.VehicleDocuments.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleDocumentRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleDocuments.Queries.GetByGid
{
    public class GetByGidVehicleDocumentQuery : IRequest<GetByGidVehicleDocumentResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidVehicleDocumentQueryHandler : IRequestHandler<GetByGidVehicleDocumentQuery, GetByGidVehicleDocumentResponse>
        {
            private readonly IMapper _mapper;
            private readonly IVehicleDocumentReadRepository _vehicleDocumentReadRepository;
            private readonly VehicleDocumentBusinessRules _vehicleDocumentBusinessRules;

            public GetByGidVehicleDocumentQueryHandler(IMapper mapper, IVehicleDocumentReadRepository vehicleDocumentReadRepository, VehicleDocumentBusinessRules vehicleDocumentBusinessRules)
            {
                _mapper = mapper;
                _vehicleDocumentReadRepository = vehicleDocumentReadRepository;
                _vehicleDocumentBusinessRules = vehicleDocumentBusinessRules;
            }

            public async Task<GetByGidVehicleDocumentResponse> Handle(GetByGidVehicleDocumentQuery request, CancellationToken cancellationToken)
            {
                X.VehicleDocument? vehicleDocument = await _vehicleDocumentReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.VehicleAllFK).Include(x => x.DocumentTypeFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _vehicleDocumentBusinessRules.VehicleDocumentShouldExistWhenSelected(vehicleDocument);

                GetByGidVehicleDocumentResponse response = _mapper.Map<GetByGidVehicleDocumentResponse>(vehicleDocument);
                return response;
            }
        }
    }
}
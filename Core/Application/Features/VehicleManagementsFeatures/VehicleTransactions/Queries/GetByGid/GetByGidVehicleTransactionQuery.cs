using AutoMapper;
using MediatR;
using X = Domain.Entities.VehicleManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Rules;
using Domain.Enums;
using Application.Repositories.VehicleManagementsRepos.VehicleTransactionRepo;

namespace Application.Features.VehicleManagementFeatures.VehicleTransactions.Queries.GetByGid
{
    public class GetByGidVehicleTransactionQuery : IRequest<GetByGidVehicleTransactionResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidVehicleTransactionQueryHandler : IRequestHandler<GetByGidVehicleTransactionQuery, GetByGidVehicleTransactionResponse>
        {
            private readonly IMapper _mapper;
            private readonly IVehicleTransactionReadRepository _vehicleTransactionReadRepository;
            private readonly VehicleTransactionBusinessRules _vehicleTransactionBusinessRules;

            public GetByGidVehicleTransactionQueryHandler(IMapper mapper, IVehicleTransactionReadRepository vehicleTransactionReadRepository, VehicleTransactionBusinessRules vehicleTransactionBusinessRules)
            {
                _mapper = mapper;
                _vehicleTransactionReadRepository = vehicleTransactionReadRepository;
                _vehicleTransactionBusinessRules = vehicleTransactionBusinessRules;
            }

            public async Task<GetByGidVehicleTransactionResponse> Handle(GetByGidVehicleTransactionQuery request, CancellationToken cancellationToken)
            {
                X.VehicleTransaction? vehicleTransaction = await _vehicleTransactionReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.SCCompanyFK).Include(x => x.UserFK).Include(x => x.VehicleAllFK).Include(x => x.CurrencyFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _vehicleTransactionBusinessRules.VehicleTransactionShouldExistWhenSelected(vehicleTransaction);

                GetByGidVehicleTransactionResponse response = _mapper.Map<GetByGidVehicleTransactionResponse>(vehicleTransaction);
                return response;
            }
        }
    }
}
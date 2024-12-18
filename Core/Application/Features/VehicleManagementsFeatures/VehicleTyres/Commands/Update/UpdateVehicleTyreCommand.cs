using Application.Features.VehicleManagementFeatures.Tyres.Constants;
using Application.Features.VehicleManagementFeatures.Tyres.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.Tyres.Rules;
using Application.Repositories.VehicleManagementsRepos.TyreRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.Tyres.Commands.Update;

public class UpdateVehicleTyreCommand : IRequest<UpdatedVehicleTyreResponse>
{
    public Guid Gid { get; set; }

    public Guid GidTyreTypeFK { get; set; }

    public string TyreNo { get; set; }
    public int? ProductionYear { get; set; }
    public DateTime? DateOfPurchase { get; set; }
    public EnumTyreStatus TyreStatus { get; set; }
    public string? Description { get; set; }



    public class UpdateTyreCommandHandler : IRequestHandler<UpdateVehicleTyreCommand, UpdatedVehicleTyreResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleTyreWriteRepository _tyreWriteRepository;
        private readonly IVehicleTyreReadRepository _tyreReadRepository;
        private readonly VehicleTyreBusinessRules _tyreBusinessRules;

        public UpdateTyreCommandHandler(IMapper mapper, IVehicleTyreWriteRepository tyreWriteRepository,
                                         VehicleTyreBusinessRules tyreBusinessRules, IVehicleTyreReadRepository tyreReadRepository)
        {
            _mapper = mapper;
            _tyreWriteRepository = tyreWriteRepository;
            _tyreBusinessRules = tyreBusinessRules;
            _tyreReadRepository = tyreReadRepository;
        }

        public async Task<UpdatedVehicleTyreResponse> Handle(UpdateVehicleTyreCommand request, CancellationToken cancellationToken)
        {
            X.VehicleTyre? tyre = await _tyreReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.TyreTypeFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _tyreBusinessRules.TyreShouldExistWhenSelected(tyre);
            tyre = _mapper.Map(request, tyre);

            _tyreWriteRepository.Update(tyre!);
            await _tyreWriteRepository.SaveAsync();
            GetByGidVehicleTyreResponse obj = _mapper.Map<GetByGidVehicleTyreResponse>(tyre);

            return new()
            {
                Title = TyresBusinessMessages.ProcessCompleted,
                Message = TyresBusinessMessages.SuccessCreatedTyreMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
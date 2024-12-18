using Application.Features.VehicleManagementFeatures.Tyres.Constants;
using Application.Features.VehicleManagementFeatures.Tyres.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.Tyres.Rules;
using Application.Repositories.VehicleManagementsRepos.TyreRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.Tyres.Commands.Create;

public class CreateVehicleTyreCommand : IRequest<CreatedVehicleTyreResponse>
{
    public Guid GidTyreTypeFK { get; set; }

    public string TyreNo { get; set; }
    public int? ProductionYear { get; set; }
    public DateTime? DateOfPurchase { get; set; }
    public EnumTyreStatus TyreStatus { get; set; }
    public string? Description { get; set; }



    public class CreateTyreCommandHandler : IRequestHandler<CreateVehicleTyreCommand, CreatedVehicleTyreResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleTyreWriteRepository _tyreWriteRepository;
        private readonly IVehicleTyreReadRepository _tyreReadRepository;
        private readonly VehicleTyreBusinessRules _tyreBusinessRules;

        public CreateTyreCommandHandler(IMapper mapper, IVehicleTyreWriteRepository tyreWriteRepository,
                                         VehicleTyreBusinessRules tyreBusinessRules, IVehicleTyreReadRepository tyreReadRepository)
        {
            _mapper = mapper;
            _tyreWriteRepository = tyreWriteRepository;
            _tyreBusinessRules = tyreBusinessRules;
            _tyreReadRepository = tyreReadRepository;
        }

        public async Task<CreatedVehicleTyreResponse> Handle(CreateVehicleTyreCommand request, CancellationToken cancellationToken)
        {
            await _tyreBusinessRules.IsAllreadyExist(request.TyreNo);
            //int maxRowNo = await _tyreReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.VehicleTyre tyre = _mapper.Map<X.VehicleTyre>(request);
            //tyre.RowNo = maxRowNo + 1;

            await _tyreWriteRepository.AddAsync(tyre);
            await _tyreWriteRepository.SaveAsync();

            X.VehicleTyre savedTyre = await _tyreReadRepository.GetAsync(predicate: x => x.Gid == tyre.Gid, include: x => x.Include(x => x.TyreTypeFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidVehicleTyreResponse obj = _mapper.Map<GetByGidVehicleTyreResponse>(savedTyre);
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
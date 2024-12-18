using Application.Features.VehicleManagementFeatures.Tyres.Constants;
using Application.Features.VehicleManagementFeatures.Tyres.Rules;
using Application.Repositories.VehicleManagementsRepos.TyreRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.Tyres.Commands.Delete;

public class DeleteVehicleTyreCommand : IRequest<DeletedVehicleTyreResponse>
{
	public Guid Gid { get; set; }

    public class DeleteTyreCommandHandler : IRequestHandler<DeleteVehicleTyreCommand, DeletedVehicleTyreResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleTyreReadRepository _tyreReadRepository;
        private readonly IVehicleTyreWriteRepository _tyreWriteRepository;
        private readonly VehicleTyreBusinessRules _tyreBusinessRules;

        public DeleteTyreCommandHandler(IMapper mapper, IVehicleTyreReadRepository tyreReadRepository,
                                         VehicleTyreBusinessRules tyreBusinessRules, IVehicleTyreWriteRepository tyreWriteRepository)
        {
            _mapper = mapper;
            _tyreReadRepository = tyreReadRepository;
            _tyreBusinessRules = tyreBusinessRules;
            _tyreWriteRepository = tyreWriteRepository;
        }

        public async Task<DeletedVehicleTyreResponse> Handle(DeleteVehicleTyreCommand request, CancellationToken cancellationToken)
        {
            X.VehicleTyre? tyre = await _tyreReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.TyreTypeFK));
            await _tyreBusinessRules.TyreShouldExistWhenSelected(tyre);
            tyre.DataState = Core.Enum.DataState.Deleted;

            _tyreWriteRepository.Update(tyre);
            await _tyreWriteRepository.SaveAsync();

            return new()
            {
                Title = TyresBusinessMessages.ProcessCompleted,
                Message = TyresBusinessMessages.SuccessDeletedTyreMessage,
                IsValid = true
            };
        }
    }
}
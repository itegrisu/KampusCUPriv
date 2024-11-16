using Application.Features.VehicleManagementFeatures.Tyres.Constants;
using Application.Features.VehicleManagementFeatures.Tyres.Rules;
using Application.Repositories.VehicleManagementsRepos.TyreRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.Tyres.Commands.Delete;

public class DeleteTyreCommand : IRequest<DeletedTyreResponse>
{
	public Guid Gid { get; set; }

    public class DeleteTyreCommandHandler : IRequestHandler<DeleteTyreCommand, DeletedTyreResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITyreReadRepository _tyreReadRepository;
        private readonly ITyreWriteRepository _tyreWriteRepository;
        private readonly TyreBusinessRules _tyreBusinessRules;

        public DeleteTyreCommandHandler(IMapper mapper, ITyreReadRepository tyreReadRepository,
                                         TyreBusinessRules tyreBusinessRules, ITyreWriteRepository tyreWriteRepository)
        {
            _mapper = mapper;
            _tyreReadRepository = tyreReadRepository;
            _tyreBusinessRules = tyreBusinessRules;
            _tyreWriteRepository = tyreWriteRepository;
        }

        public async Task<DeletedTyreResponse> Handle(DeleteTyreCommand request, CancellationToken cancellationToken)
        {
            X.Tyre? tyre = await _tyreReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.TyreTypeFK));
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
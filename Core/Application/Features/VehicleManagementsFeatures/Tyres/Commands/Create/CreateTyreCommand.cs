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

public class CreateTyreCommand : IRequest<CreatedTyreResponse>
{
    public Guid GidTyreTypeFK { get; set; }

    public string TyreNo { get; set; }
    public int? ProductionYear { get; set; }
    public DateTime? DateOfPurchase { get; set; }
    public EnumTyreStatus TyreStatus { get; set; }
    public string? Description { get; set; }



    public class CreateTyreCommandHandler : IRequestHandler<CreateTyreCommand, CreatedTyreResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITyreWriteRepository _tyreWriteRepository;
        private readonly ITyreReadRepository _tyreReadRepository;
        private readonly TyreBusinessRules _tyreBusinessRules;

        public CreateTyreCommandHandler(IMapper mapper, ITyreWriteRepository tyreWriteRepository,
                                         TyreBusinessRules tyreBusinessRules, ITyreReadRepository tyreReadRepository)
        {
            _mapper = mapper;
            _tyreWriteRepository = tyreWriteRepository;
            _tyreBusinessRules = tyreBusinessRules;
            _tyreReadRepository = tyreReadRepository;
        }

        public async Task<CreatedTyreResponse> Handle(CreateTyreCommand request, CancellationToken cancellationToken)
        {
            await _tyreBusinessRules.IsAllreadyExist(request.TyreNo);
            //int maxRowNo = await _tyreReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.Tyre tyre = _mapper.Map<X.Tyre>(request);
            //tyre.RowNo = maxRowNo + 1;

            await _tyreWriteRepository.AddAsync(tyre);
            await _tyreWriteRepository.SaveAsync();

            X.Tyre savedTyre = await _tyreReadRepository.GetAsync(predicate: x => x.Gid == tyre.Gid, include: x => x.Include(x => x.TyreTypeFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidTyreResponse obj = _mapper.Map<GetByGidTyreResponse>(savedTyre);
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
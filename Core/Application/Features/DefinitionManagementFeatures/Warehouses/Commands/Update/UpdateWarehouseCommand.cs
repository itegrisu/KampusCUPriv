using Application.Features.DefinationManagementFeatures.Warehouses.Constants;
using Application.Features.DefinationManagementFeatures.Warehouses.Queries.GetByGid;
using Application.Features.DefinationManagementFeatures.Warehouses.Rules;
using Application.Repositories.DefinitonManagementRepos.WarehouseRepo;
using AutoMapper;
using Domain.Entities.DefinitionManagements;
using MediatR;

namespace Application.Features.DefinationManagementFeatures.Warehouses.Commands.Update;

public class UpdateWarehouseCommand : IRequest<UpdatedWarehouseResponse>
{
    public Guid Gid { get; set; }


    public string WarehouseName { get; set; }
    public string Location { get; set; }



    public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, UpdatedWarehouseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWarehouseWriteRepository _warehouseWriteRepository;
        private readonly IWarehouseReadRepository _warehouseReadRepository;
        private readonly WarehouseBusinessRules _warehouseBusinessRules;

        public UpdateWarehouseCommandHandler(IMapper mapper, IWarehouseWriteRepository warehouseWriteRepository,
                                         WarehouseBusinessRules warehouseBusinessRules, IWarehouseReadRepository warehouseReadRepository)
        {
            _mapper = mapper;
            _warehouseWriteRepository = warehouseWriteRepository;
            _warehouseBusinessRules = warehouseBusinessRules;
            _warehouseReadRepository = warehouseReadRepository;
        }

        public async Task<UpdatedWarehouseResponse> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            Warehouse? warehouse = await _warehouseReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _warehouseBusinessRules.WarehouseShouldExistWhenSelected(warehouse);
            warehouse = _mapper.Map(request, warehouse);

            _warehouseWriteRepository.Update(warehouse!);
            await _warehouseWriteRepository.SaveAsync();
            GetByGidWarehouseResponse obj = _mapper.Map<GetByGidWarehouseResponse>(warehouse);

            return new()
            {
                Title = WarehousesBusinessMessages.ProcessCompleted,
                Message = WarehousesBusinessMessages.SuccessCreatedWarehouseMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
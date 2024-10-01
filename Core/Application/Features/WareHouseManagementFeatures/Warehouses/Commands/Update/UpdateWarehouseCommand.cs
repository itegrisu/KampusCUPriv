using Application.Features.WarehouseManagementFeatures.Warehouses.Constants;
using Application.Features.WarehouseManagementFeatures.Warehouses.Queries.GetByGid;
using Application.Features.WarehouseManagementFeatures.Warehouses.Rules;
using Application.Repositories.WarehouseManagementRepos.WarehouseRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.WarehouseManagements;

namespace Application.Features.WarehouseManagementFeatures.Warehouses.Commands.Update;

public class UpdateWarehouseCommand : IRequest<UpdatedWarehouseResponse>
{
    public Guid Gid { get; set; }

    public string? GidOrganizationFK { get; set; }

    public string Name { get; set; }
    public EnumWarehouseType WarehouseType { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }



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
            X.Warehouse? warehouse = await _warehouseReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _warehouseBusinessRules.WarehouseShouldExistWhenSelected(warehouse);
            await _warehouseBusinessRules.OrganizationShouldExistWhenSelected(request.GidOrganizationFK);
            await _warehouseBusinessRules.WarehouseNameShouldBeUnique(request.Name, request.Gid);
            warehouse = _mapper.Map(request, warehouse);

            _warehouseWriteRepository.Update(warehouse!);
            await _warehouseWriteRepository.SaveAsync();

            X.Warehouse updatedWarehouse = await _warehouseReadRepository.GetAsync(predicate: x => x.Gid == warehouse.Gid, include: x => x.Include(x => x.OrganizationFK));

            GetByGidWarehouseResponse obj = _mapper.Map<GetByGidWarehouseResponse>(updatedWarehouse);

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
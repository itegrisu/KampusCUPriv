using Application.Features.WarehouseManagementFeatures.Warehouses.Constants;
using Application.Features.WarehouseManagementFeatures.Warehouses.Queries.GetByGid;
using Application.Features.WarehouseManagementFeatures.Warehouses.Rules;
using Application.Repositories.WarehouseManagementRepos.WarehouseRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.WarehouseManagements;

namespace Application.Features.WarehouseManagementFeatures.Warehouses.Commands.Create;

public class CreateWarehouseCommand : IRequest<CreatedWarehouseResponse>
{
    public string? GidOrganizationFK { get; set; }
    public string Name { get; set; }
    public EnumWarehouseType WarehouseType { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }



    public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, CreatedWarehouseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWarehouseWriteRepository _warehouseWriteRepository;
        private readonly IWarehouseReadRepository _warehouseReadRepository;
        private readonly WarehouseBusinessRules _warehouseBusinessRules;

        public CreateWarehouseCommandHandler(IMapper mapper, IWarehouseWriteRepository warehouseWriteRepository,
                                         WarehouseBusinessRules warehouseBusinessRules, IWarehouseReadRepository warehouseReadRepository)
        {
            _mapper = mapper;
            _warehouseWriteRepository = warehouseWriteRepository;
            _warehouseBusinessRules = warehouseBusinessRules;
            _warehouseReadRepository = warehouseReadRepository;
        }

        public async Task<CreatedWarehouseResponse> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {

            await _warehouseBusinessRules.OrganizationShouldExistWhenSelected(request.GidOrganizationFK);
            await _warehouseBusinessRules.WarehouseNameShouldBeUnique(request.Name);

            X.Warehouse warehouse = _mapper.Map<X.Warehouse>(request);


            await _warehouseWriteRepository.AddAsync(warehouse);
            await _warehouseWriteRepository.SaveAsync();

            X.Warehouse savedWarehouse = await _warehouseReadRepository.GetAsync(predicate: x => x.Gid == warehouse.Gid, include: x => x.Include(x => x.OrganizationFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidWarehouseResponse obj = _mapper.Map<GetByGidWarehouseResponse>(savedWarehouse);
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
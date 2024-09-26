using Application.Features.DefinationManagementFeatures.Warehouses.Constants;
using Application.Features.DefinationManagementFeatures.Warehouses.Queries.GetByGid;
using Application.Features.DefinationManagementFeatures.Warehouses.Rules;
using Application.Repositories.DefinitonManagementRepos.WarehouseRepo;
using AutoMapper;
using Domain.Entities.DefinitionManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.DefinationManagementFeatures.Warehouses.Commands.Create;

public class CreateWarehouseCommand : IRequest<CreatedWarehouseResponse>
{
    
public string WarehouseName { get; set; }
public string Location { get; set; }



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
            //int maxRowNo = await _warehouseReadRepository.GetAll().MaxAsync(r => r.RowNo);
			Warehouse warehouse = _mapper.Map<Warehouse>(request);
            //warehouse.RowNo = maxRowNo + 1;

            await _warehouseWriteRepository.AddAsync(warehouse);
            await _warehouseWriteRepository.SaveAsync();

			Warehouse savedWarehouse = await _warehouseReadRepository.GetAsync(predicate: x => x.Gid == warehouse.Gid);
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
using Core.Repositories.Abstracts;
using Domain.Entities.WarehouseManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.WarehouseManagementRepos.StockMovementRepo
{
    public interface IStockMovementWriteRepository : IWriteRepository<StockMovement>
    {

    }
}

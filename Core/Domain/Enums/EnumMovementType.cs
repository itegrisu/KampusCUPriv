using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum EnumMovementType
    {
        InvoicedProductEntry = 1,
        ManualProductEntry = 2,
        InvoicedProductOutput = 3,
        CasualtyLoss = 4,
        ManualExit = 5,
        DefectiveUnderWarranty = 6,
        StockRelocation = 7,
    }
}

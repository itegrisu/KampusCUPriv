using Core.Application.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleTransactions.Queries.GetForCalendar
{
    public class GetForCalendarListItemDto : IDto
    {
        public DateTime Date { get; set; }
        public List<string> BusyVehicles { get; set; }
        public List<string> EmptyVehicles { get; set; }
        public List<BusyVehicleDetail> BusyVehicleDetails { get; set; } // Yeni alan
    }

    public class BusyVehicleDetail
    {
        public string PlateNumber { get; set; } // Araç plakası
        public string Reason { get; set; }     // Doluluk sebebi (Sefer veya Talep)
    }

}

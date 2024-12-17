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
        public DateTime Date { get; set; }                           // Tarih
        public List<string> BusyVehicles { get; set; }              // Dolu araç plakaları
        public List<string> EmptyVehicles { get; set; }             // Boş araç plakaları
    }
}

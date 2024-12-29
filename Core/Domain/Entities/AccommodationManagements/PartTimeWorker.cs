using Core.Entities;

namespace Domain.Entities.AccommodationManagements
{
    public class PartTimeWorker : BaseEntity
    {
        public string IdentityNo { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsLoginStatus { get; set; }
        public string Gsm { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }

        public ICollection<PartTimeWorkerForeignLanguage>? PartTimeWorkerForeignLanguages { get; set; }
        public ICollection<PartTimeWorkerFile>? PartTimeWorkerFiles { get; set; }
        public ICollection<ReservationHotelPartTimeWorker>? ReservationHotelPartTimeWorkers { get; set; }

    }
}

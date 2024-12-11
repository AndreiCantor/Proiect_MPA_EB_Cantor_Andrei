using Microsoft.AspNetCore.Identity;

namespace Proiect_MPA_EB_Cantor_Andrei.Models
{
    public class Reservation
    {

        public int Id { get; set; }
        public int EventId { get; set; }
        public Event? Event { get; set; } = null!;

        public string? UserId { get; set; }
        public IdentityUser? User { get; set; } = null!; 

        public DateTime ReservationDate { get; set; } 
        public int NumberOfSeats { get; set; }
    }
}

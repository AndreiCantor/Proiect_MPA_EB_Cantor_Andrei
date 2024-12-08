namespace Proiect_MPA_EB_Cantor_Andrei.Models
{
    public class Reservation
    {

        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty; 
        public int Seats { get; set; } 
        public DateTime ReservationDate { get; set; } 

      
        public int EventId { get; set; }
        public Event? Event { get; set; }
    }
}

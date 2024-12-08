namespace Proiect_MPA_EB_Cantor_Andrei.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty; 
        public DateTime Date { get; set; } 
        public int Capacity { get; set; } 
        public int AvailableSeats { get; set; } 

   
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}

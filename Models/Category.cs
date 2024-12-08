﻿namespace Proiect_MPA_EB_Cantor_Andrei.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; 

      
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
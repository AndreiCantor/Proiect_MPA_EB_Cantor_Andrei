using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proiect_MPA_EB_Cantor_Andrei.Models;

namespace Proiect_MPA_EB_Cantor_Andrei.Data
{
    public class Proiect_MPA_EB_Cantor_AndreiContext : DbContext
    {
        public Proiect_MPA_EB_Cantor_AndreiContext (DbContextOptions<Proiect_MPA_EB_Cantor_AndreiContext> options)
            : base(options)
        {
        }

        public DbSet<Proiect_MPA_EB_Cantor_Andrei.Models.Event> Event { get; set; } = default!;
        public DbSet<Proiect_MPA_EB_Cantor_Andrei.Models.Category> Category { get; set; } = default!;
    }
}

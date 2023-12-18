using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proiect.Models;

namespace Proiect.Data
{
    public class ProiectContext : DbContext
    {
        public ProiectContext (DbContextOptions<ProiectContext> options)
            : base(options)
        {
        }

        public DbSet<Proiect.Models.Event> Event { get; set; } = default!;
        public DbSet<Proiect.Models.Category> Category { get; set; } = default!;
        public DbSet<Proiect.Models.EventCategory> EventCategory { get; set; } = default!;
        public DbSet<Proiect.Models.Participant> Participant { get; set; } = default!;
        public DbSet<Proiect.Models.Payment> Payment { get; set; } = default!;
    }
}

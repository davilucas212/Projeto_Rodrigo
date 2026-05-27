using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Projeto_Rodrigo.Models;

namespace Projeto_Rodrigo.Data
{
   
    

    public class ReuniaoDbContext : DbContext
    {
        public ReuniaoDbContext(DbContextOptions<ReuniaoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Sala> Salas { get; set; }

        public DbSet<Reserva> Reservas { get; set; }
    }
}

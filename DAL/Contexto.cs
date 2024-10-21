using System;
using Microsoft.EntityFrameworkCore;
using NOVAASSIST.Entidades;

namespace NOVAASSIST.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Asistencias>? Asistencias { get; set; }

        public DbSet<Empleados>? Empleados { get; set; }

        public DbSet<Areas>? Areas { get; set; }

        public DbSet<Excepciones>? Excepciones { get; set; }
        public DbSet<Tardanzas> Tardanzas { get; set; }
        public DbSet<SalidasAnticipadas> SalidasAnticipadas { get; set; }
        public DbSet<HorasTrabajadas> HorasTrabajadas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-636KDOT;Database=Dbboto;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
           

            modelBuilder.Entity<Areas>().HasData(
                new Areas { AreaId = 1, Nombre = "Contabilidad", Descripcion = "Contable",   },
                new Areas { AreaId = 2, Nombre = "Sistemas", Descripcion = "Programador",   }

            );
        }
    }
}
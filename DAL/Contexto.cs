using System;
using Microsoft.EntityFrameworkCore;
using NOVAASSIST.Entidades;

namespace NOVAASSIST.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Asistencias>? Asistencias { get; set; }

        public DbSet<Empleados>? Empleados { get; set; }

        public DbSet<Horarios>? Horarios { get; set; }

        public DbSet<Areas>? Areas { get; set; }

        public DbSet<Vacaciones>? Vacaciones { get; set; }

        public DbSet<Excepciones>? Excepciones { get; set; }
        public DbSet<ReporteHoras> ReporteHoras { get; set; }
        public DbSet<Tardanzas> Tardanzas { get; set; }
        public DbSet<SalidasAnticipadas> SalidasAnticipadas { get; set; }
        public DbSet<HorasTrabajadas> HorasTrabajadas { get; set; }


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
            modelBuilder.Entity<ReporteHoras>()
            .HasNoKey();
            modelBuilder.Entity<Vacaciones>().HasData(
                new Vacaciones { VacacionesId = 1, Descripcion = "Vacaciones diciembre", Fecha_Inicio = new DateTime(2022, 5, 5), Fecha_Fin = new DateTime(2010, 7, 7) },
                new Vacaciones { VacacionesId = 2, Descripcion = "Vacaciones Semana Santa", Fecha_Inicio = new DateTime(2022, 4, 5), Fecha_Fin = new DateTime(2022, 8, 8) }

            );
        }
    }
}
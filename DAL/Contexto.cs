using System;
using Microsoft.EntityFrameworkCore;
using NOVAASSIST.Entidades;

namespace NOVAASSIST.DAL
{
    public class Contexto : DbContext
    {
        
        public DbSet<Asistencias> Asistencias { get; set;}

        public DbSet<Empleados> Empleados { get; set;}

        public DbSet<Horarios> Horarios { get; set;}

        public DbSet<Areas> Areas { get; set; }

        public DbSet<Vacaciones> Vacaciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = DATA\DATA.db");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleados>().HasData(
                new Empleados{EmpleadoId=1, Nombre="Luisa", ClaveUsuarios="12345", ClaveAcceso="123", FechaNacimiento= new DateTime(2010,5,5),Cedula="123",
                Genero = "F" , Area="bobo",Telefono="8093",Email="jfasnfn@gmail.com",Vacaciones="Si", Estado=null},

                new Empleados{EmpleadoId=2, Nombre="pedro", ClaveUsuarios="1234", ClaveAcceso="12", FechaNacimiento= new DateTime(2015,5,5),Cedula="1234",
                Genero = "M" , Area="bobo",Telefono="80934",Email="jf@gmail.com",Vacaciones="Si", Estado=null},
                
                new Empleados{EmpleadoId=3, Nombre="maria", ClaveUsuarios="123", ClaveAcceso="124", FechaNacimiento= new DateTime(2017,5,5),Cedula="1235",
                Genero = "F" , Area="bobo",Telefono="80939",Email="mm@gmail.com",Vacaciones="Si", Estado=null},
               
                new Empleados{EmpleadoId=4, Nombre="mario", ClaveUsuarios="12", ClaveAcceso="126", FechaNacimiento= new DateTime(2022,5,5),Cedula="1236",
                Genero = "M" , Area="bobo",Telefono="80938",Email="zz@gmail.com",Vacaciones="Si", Estado=null}
               
            );

            modelBuilder.Entity<Areas>().HasData(
                new Areas{AreaId=1, Nombre="Contabilidad", Descripcion="Contable", SueldoPorHora=100, Descuento=10, SueldoTotal=1000, Horario="Noche"  },
                new Areas{AreaId=2, Nombre="Sistemas", Descripcion="Programador", SueldoPorHora=100, Descuento=10, SueldoTotal=1000, Horario="Tarde" }
               
            );

            modelBuilder.Entity<Vacaciones>().HasData(
                new Vacaciones{VacacionesId= 1, Descripcion="Vacaciones diciembre", Fecha_Inicio= new DateTime(2022,5,5), Fecha_Fin=new DateTime(2010,7,7)},
                new Vacaciones{ VacacionesId= 2, Descripcion="Vacaciones Semana Santa", Fecha_Inicio= new DateTime(2022,4,5), Fecha_Fin=new DateTime(2022,8,8)}
               
            );
            
        }
    }
}
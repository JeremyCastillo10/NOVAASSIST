using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOVAASSIST.Entidades
{
    public class Empleados
    {
        [Key]

        public int EmpleadoId { get; set; }

        public string? Nombre { get; set; }

        public string? ClaveUsuarios { get; set; } // tiene que ser string

        public string? ClaveAcceso { get; set; } // tiene que ser string


        public DateTime FechaNacimiento { get; set; }= DateTime.Now;

        public string? Cedula { get; set; }

        public string? Genero { get; set; }

        public int Area { get; set; } //id area

        public string? Telefono { get; set; }

        public string? Email { get; set; }

        public int Vacaciones { get; set; } // id vacaciones

        public string? Direccion { get; set; }
        
        public double SalarioPorHora { get; set; }

        public Boolean? Estado { get; set; }

        public Boolean? EmpleadoEliminado { get; set; } = false;
        public double HorasTrabajadasMes { get; set; }
        public double HorasExtras { get; set; }

        public string? HoraEntrada { get; set; }
        public string? HoraSalida { get; set; }


    }
}
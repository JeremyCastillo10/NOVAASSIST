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

        //public string ClaveHuella {get; set;} // tiene que generarse con un hash

        public DateTime FechaNacimiento { get; set; }= DateTime.Now;

        public string? Cedula { get; set; }

        public string? Genero { get; set; }

        public int Area { get; set; } //id area
        public string? AreaDescripcion {set; get;}

        public string? Telefono { get; set; }

        public string? Email { get; set; }

        public int Vacaciones { get; set; } // id vacaciones

        public string? VacacionesDescripcion {set; get;}

        public string? Direccion { get; set; }
        
        public int contador { get; set; } = 0;

        public Boolean? Estado { get; set; }

        public Boolean? EmpleadoEliminado { get; set; } = false;

        public override bool Equals(object? obj)
        {
            if(obj==null)
                return false;
                
            if(!(obj is Empleados))
                return false;

            return (this.EmpleadoId == ((Empleados)obj).EmpleadoId)
                    && this.EmpleadoId== ((Empleados)obj).EmpleadoId;
        }
    }
}
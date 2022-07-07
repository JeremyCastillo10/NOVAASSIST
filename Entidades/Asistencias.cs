
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOVAASSIST.Entidades
{
  
    public class Asistencias
    {
        [Key]
        public int AsistenciaId { get; set; }
        
        public string ? Nombre { get; set; }
        public int EmpleadoId { get; set; }
        
         public string ? cedula { get; set; }
        public DateTime Fecha_Entrada { get; set; }

        public DateTime Fecha_Salida { get; set; }

        public Boolean? Estado { get; set; }

        public string? HuellaEmpleado { get; set; }
    }
}
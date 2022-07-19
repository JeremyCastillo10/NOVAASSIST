using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOVAASSIST.Entidades
{
    public class Excepciones
    {
        [Key]

        public int ExcepcionId { get; set; }

        public DateTime Fecha_Creacion { get; set; } = DateTime.Now;

        public string? Nombre { get; set; }

        public string? Descripcion  { get; set; }
        
        public float Descuento { get; set; }

        public Boolean? ExcepcionEliminada { get; set; }  = false;
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOVAASSIST.Entidades
{
  
    public class Horarios
    {
        [Key]
        public int HorarioId { get; set; }
        public string? Descripcion { get; set; }
        public string? Fecha_Entrada { get; set; } 
        /* public string Fecha_Descanso { get; set; }  */
        public string? Fecha_Salida { get; set; } 
        public string? Dias { get; set; }

    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOVAASSIST.Entidades
{
    public class Vacaciones
    {
        [Key]

        public int VacacionesId { get; set; }

        public string? Descripcion { get; set; }

        public DateTime Fecha_Inicio { get; set; } = DateTime.Now;

        public DateTime Fecha_Fin { get; set; } = DateTime.Now;
    }
}
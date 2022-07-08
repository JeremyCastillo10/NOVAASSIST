
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOVAASSIST.Entidades
{
    public class Areas
    {
        [Key]

        public int AreaId { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public float SueldoPorHora { get; set; }
        
        public float Descuento { get; set; }

        public float SueldoTotal { get; set; }

        public string? Horario {get; set; }
    }
}
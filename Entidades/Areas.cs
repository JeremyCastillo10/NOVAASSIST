
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
       
    }
}
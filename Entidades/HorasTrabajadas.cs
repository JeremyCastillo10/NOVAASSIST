using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOVAASSIST.Entidades
{
    public class HorasTrabajadas
    {
        [Key]
        public int HorasTrabajadasId { get; set; }
        public int EmpleadoId { get; set; }  // Relación con la entidad Empleados
        public DateTime Fecha { get; set; }
        public double Horas { get; set; } // Horas trabajadas en ese día
    }
}

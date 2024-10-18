using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOVAASSIST.Entidades
{
    public class Tardanzas
    {
        [Key]
        public int TardanzaId { get; set; }

        public int EmpleadoId { get; set; }

        [ForeignKey("Asistencia")]
        public int AsistenciaId { get; set; } // ID de la asistencia relacionada

        public DateTime Fecha { get; set; }

        public TimeSpan HoraEntradaReal { get; set; }

        // Otras propiedades necesarias para registrar la tardanza

        // Navegación a la asistencia relacionada
        public virtual Asistencias Asistencia { get; set; }

        public TimeSpan TiempoTardanza { get; set; }
    }
}

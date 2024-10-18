using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOVAASSIST.Entidades
{
    public class SalidasAnticipadas
    {
        [Key]
        public int SalidaAnticipadaId { get; set; }

        public int EmpleadoId { get; set; }

        [ForeignKey("Asistencia")]
        public int AsistenciaId { get; set; } // ID de la asistencia relacionada

        public DateTime Fecha { get; set; }

        public TimeSpan HoraSalidaReal { get; set; }

        // Otras propiedades necesarias para registrar la salida anticipada

        // Navegación a la asistencia relacionada
        public virtual Asistencias Asistencia { get; set; }
        public TimeSpan TiempoSalidaAnticipada { get; set; }

    }
}

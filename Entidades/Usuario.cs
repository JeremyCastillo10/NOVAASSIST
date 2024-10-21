using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOVAASSIST.Entidades
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; } // ID del usuario
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public int AreaId { get; set; } // ID del área asignada
        public string Rol { get; set; } // Rol del usuario (ej. Admin, Empleado)
        public virtual Areas Area { get; set; } // Navegación hacia el área


    }
}

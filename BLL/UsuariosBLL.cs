using System.Collections.Generic;
using System.Linq;
using NOVAASSIST.Entidades;
using NOVAASSIST.DAL;

namespace NOVAASSIST.BLL
{
    public static class UsuariosBLL
    {
        public static bool RegistrarUsuario(Usuario usuario)
        {
            using (var contexto = new Contexto())
            {
                if (contexto.Usuarios.Any(u => u.Nombre == usuario.Nombre))
                {
                    return false; // El usuario ya existe
                }

                contexto.Usuarios.Add(usuario); // Agregar nuevo usuario
                contexto.SaveChanges(); // Guardar cambios
                return true; // Registro exitoso
            }
        }

        public static List<Areas> GetAreas()
        {
            using (var contexto = new Contexto())
            {
                return contexto.Areas.ToList(); // Retorna la lista de áreas
            }
        }

        public static List<string> GetRoles()
        {
            return new List<string> { "Admin", "RRHH", "Supervisor" }; // Lista de roles
        }
        public static Usuario ValidarUsuario(string nombreUsuario, string clave)
        {
            Contexto contexto = new Contexto();
            return contexto.Usuarios
                .FirstOrDefault(u => u.Nombre == nombreUsuario && u.Clave == clave);
        }
    }
}

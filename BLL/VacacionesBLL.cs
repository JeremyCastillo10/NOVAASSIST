using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using NOVAASSIST.DAL;
using NOVAASSIST.Entidades;
namespace NOVAASSIST.BLL
{
    public class VacacionesBLL
    {
        public static bool Existe(int vacacionesId)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Vacaciones.Any(l => l.VacacionesId == vacacionesId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }

        public static bool Guardar(Vacaciones vacaciones)
        {
            if (!Existe(vacaciones.VacacionesId))
                return Insertar(vacaciones);
            else
                return Modificar(vacaciones);

        }

        private static bool Insertar(Vacaciones vacaciones)
        {
            Contexto contexto = new Contexto();
            bool paso = false;
            try
            {
                contexto.Vacaciones.Add(vacaciones);
                paso = contexto.SaveChanges() > 0;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        private static bool Modificar(Vacaciones vacaciones)
        {
            Contexto contexto = new Contexto();
            bool paso = false;
            try
            {
                contexto.Entry(vacaciones).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int vacacionesId)
        {
            Contexto contexto = new Contexto();
            bool paso = false;
            try
            {
                var Vacaciones = contexto.Vacaciones.Find(vacacionesId);
                if (Vacaciones != null)
                {
                    contexto.Vacaciones.Remove(Vacaciones);
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static Vacaciones? Buscar(int vacacionesId)
        {
            Contexto contexto = new Contexto();
            Vacaciones? Vacaciones;
            try
            {
                Vacaciones = contexto.Vacaciones.Find(vacacionesId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Vacaciones;
        }

        public static List<Vacaciones> GetList(Expression<Func<Vacaciones, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Vacaciones> lista = new List<Vacaciones>();
            try
            {
                lista = contexto.Vacaciones.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
    }
}
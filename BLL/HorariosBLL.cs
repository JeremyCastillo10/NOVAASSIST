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
    public class HorariosBLL
    {

         public static bool Existe(int horarioId)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Horarios.Any(l => l.HorarioId == horarioId);
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

        public static bool Guardar(Horarios horarios)
        {
            if (!Existe(horarios.HorarioId))
                return Insertar(horarios);
            else
                return Modificar(horarios);

        }

        private static bool Insertar(Horarios horario)
        {
            Contexto contexto = new Contexto();
            bool paso = false;
            try
            {
                contexto.Horarios.Add(horario);
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

        private static bool Modificar(Horarios horario)
        {
            Contexto contexto = new Contexto();
            bool paso = false;
            try
            {
                contexto.Entry(horario).State = EntityState.Modified;
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

        public static bool Eliminar(int horarioId)
        {
            Contexto contexto = new Contexto();
            bool paso = false;
            try
            {
                var Horario = contexto.Horarios.Find(horarioId);
                if (Horario != null)
                {
                    contexto.Horarios.Remove(Horario);
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

        public static Horarios? Buscar(int horarioId)
        {
            Contexto contexto = new Contexto();
            Horarios? Horario;
            try
            {
                Horario = contexto.Horarios.Find(horarioId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Horario;
        }

        public static List<Horarios> GetList(Expression<Func<Horarios, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Horarios> lista = new List<Horarios>();
            try
            {
                lista = contexto.Horarios.Where(criterio).ToList();
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
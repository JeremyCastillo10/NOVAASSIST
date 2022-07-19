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
    public class ExcepcionesBLL
    {
        public static bool Guardar(Excepciones Excepciones)
        {
            if (!Existe(Excepciones.ExcepcionId))
                return Insertar(Excepciones);
            else
                return Modificar(Excepciones);
        }

        public static bool Insertar(Excepciones Excepciones)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.Excepciones.Add(Excepciones);
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

        public static bool Modificar(Excepciones excepciones)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.Entry(excepciones).State = EntityState.Modified;
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

        public static bool Existe(int ExcepcionesId)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Excepciones.Any(l => l.ExcepcionId ==ExcepcionesId);
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

        public static bool Eliminar(int ExcepcionesId)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                var Excepciones = contexto.Excepciones.Find(ExcepcionesId);
                if (Excepciones != null)
                {
                    contexto.Excepciones.Remove(Excepciones);
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

        public static Excepciones? Buscar(int ExcepcionesId)
        {
            Contexto contexto = new Contexto();
           Excepciones?Excepciones;

            try
            {
               Excepciones = contexto.Excepciones.Find(ExcepcionesId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return Excepciones;
        }

        public static List<Excepciones> GetList(Expression<Func<Excepciones, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Excepciones> lista = new List<Excepciones>();

            try
            {
                lista = contexto.Excepciones.Where(criterio).ToList();
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
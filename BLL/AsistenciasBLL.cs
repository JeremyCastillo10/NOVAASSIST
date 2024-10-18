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
    public class AsistenciasBLL
    {

        public static void Insertar(Asistencias asistencia)
        {
            using (var contexto = new Contexto())
            {
                contexto.Asistencias.Add(asistencia); // No se establece ID manualmente
                contexto.SaveChanges(); // Aquí se generará el ID
            }
        }

        public static bool Modificar(Asistencias asistencias)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                //marcar la entidad como modificada para que el contexto sepa como proceder
                contexto.Entry(asistencias).State = EntityState.Modified;
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

        public static Asistencias Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Asistencias? asistencias;

            try
            {
                asistencias = contexto?.Asistencias?.Where(A => A.EmpleadoId == id).SingleOrDefault();

            }
            catch (Exception)
            {
                throw;
            }
            finally { contexto.Dispose(); }

            return asistencias;
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool confirmar = false;

            try
            {
                confirmar = contexto.Asistencias.Any(e => e.AsistenciaId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally { contexto.Dispose(); }

            return confirmar;
        }

        public static List<Asistencias> GetAsistecia()
        {
            List<Asistencias>? lista = new List<Asistencias>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto?.Asistencias?.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally { contexto.Dispose(); }

            return lista;
        }

        public static List<Asistencias> GetList(Expression<Func<Asistencias, bool>> criterio)
        {
            List<Asistencias> lista = new List<Asistencias>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto?.Asistencias?.Where(criterio)?.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally { contexto.Dispose(); }

            return lista;
        }
    }
}
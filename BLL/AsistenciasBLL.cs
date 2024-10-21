using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using NOVAASSIST.DAL;
using NOVAASSIST.Entidades;
using System.Windows;

namespace NOVAASSIST.BLL
{
    public class AsistenciasBLL
    {

        public static void Insertar(Asistencias asistencia)
        {
            using (var contexto = new Contexto())
            {
                // Buscar la última asistencia del empleado en el mismo día
                var asistenciaExistente = contexto.Asistencias
                    .Where(a => a.EmpleadoId == asistencia.EmpleadoId &&
                                a.Fecha_Entrada.Date == DateTime.Now.Date)
                    .OrderByDescending(a => a.Fecha_Entrada)
                    .FirstOrDefault();

                // Si ya existe una entrada para el día
                if (asistenciaExistente != null)
                {
                    if (asistenciaExistente.Estado == true)
                    {
                        // Modificar la asistencia existente: actualizar Fecha_Salida y cambiar el estado a false
                        asistenciaExistente.Fecha_Salida = DateTime.Now; // Asignar la fecha actual
                        asistenciaExistente.Estado = false; // Cambiar el estado a false

                        contexto.Entry(asistenciaExistente).State = EntityState.Modified;
                        contexto.SaveChanges(); // Guardar los cambios
                    }
                    else
                    {
                        // Si el estado es false, se crea una nueva asistencia
                        asistencia.Fecha_Entrada = DateTime.Now; // Asignar la fecha actual
                        asistencia.Fecha_Salida = DateTime.MinValue; // Asignar nulo a Fecha_Salida
                        asistencia.Estado = true; // Establecer el estado como true

                        contexto.Asistencias.Add(asistencia); // Agregar nueva asistencia
                        contexto.SaveChanges(); // Guardar cambios en la base de datos
                    }
                }
                else
                {
                    // Si no hay asistencia previa, agregar nueva asistencia
                    asistencia.Fecha_Entrada = DateTime.Now; // Asignar la fecha actual
                    asistencia.Fecha_Salida = DateTime.MinValue; // Asignar nulo a Fecha_Salida
                    asistencia.Estado = true; // Establecer el estado como true

                    contexto.Asistencias.Add(asistencia); // Agregar nueva asistencia
                    contexto.SaveChanges(); // Guardar cambios en la base de datos
                }
            }
        }




        public static TimeSpan CalcularHorasTrabajadas(int empleadoId, DateTime fechaInicio, DateTime fechaFin)
        {
            using (var contexto = new Contexto())
            {
                try
                {
                    var asistencias = contexto.Asistencias
                        .Where(a => a.EmpleadoId == empleadoId &&
                                    a.Fecha_Entrada.Date >= fechaInicio.Date &&
                                    a.Fecha_Entrada.Date <= fechaFin.Date)
                        .ToList();

                    if (asistencias.Count == 0)
                    {
                        MessageBox.Show("No se encontraron asistencias para este empleado en el rango de fechas especificado.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        return TimeSpan.Zero;
                    }

                    TimeSpan totalHoras = TimeSpan.Zero;

                    foreach (var asistencia in asistencias)
                    {
                        if (asistencia.Fecha_Salida > asistencia.Fecha_Entrada)
                        {
                            totalHoras += (asistencia.Fecha_Salida - asistencia.Fecha_Entrada);
                        }
                    }

                    return totalHoras;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al calcular horas trabajadas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return TimeSpan.Zero; // En caso de error, devuelve TimeSpan.Zero
                }
            }
        }
        public static List<Asistencias> GetAsistencias(int empleadoId, DateTime startDate, DateTime endDate)
        {
            using (var contexto = new Contexto())
            {
                return contexto.Asistencias
                    .Where(a => a.EmpleadoId == empleadoId && a.Fecha_Entrada >= startDate && a.Fecha_Entrada < endDate)
                    .ToList();
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
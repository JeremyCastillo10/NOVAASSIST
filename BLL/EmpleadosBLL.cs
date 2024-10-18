using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using NOVAASSIST.DAL;
using NOVAASSIST.Entidades;
using Microsoft.Data.SqlClient;
using System.Windows;

namespace NOVAASSIST.BLL
{
    public class EmpleadosBLL
    {
        public static bool Guardar(Empleados empleados)
        {
            if(!Existe(empleados.EmpleadoId))
                return Insertar(empleados);
            else
                return Modificar(empleados);
        }

        public static bool Insertar(Empleados empleados)
        {
            bool confirmar = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Empleados.Add(empleados);
                confirmar = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally { contexto.Dispose(); }

            return confirmar;
        }

        public static bool Modificar(Empleados empleados)
        {
            bool confirmar = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(empleados).State = EntityState.Modified;
                confirmar = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }

            return confirmar;
        }

        public static bool Eliminar(int id)
        {
            bool confirmar = false;
            Contexto contexto = new Contexto();

            try
            {
                var empleados = contexto.Empleados.Find(id);
                if(empleados != null)
                {
                    contexto.Empleados.Remove(empleados);
                    confirmar = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally { contexto.Dispose(); }

            return confirmar;
        }

        public static Empleados Buscar(string id)
        {
            using (var contexto = new Contexto())
            {
                return contexto.Empleados.FirstOrDefault(A => A.ClaveAcceso == id);
            }
        }
        public static Empleados Buscar(string claveUsuario, string claveAcceso)
        {
            using (var contexto = new Contexto())
            {
                return contexto.Empleados
                    .FirstOrDefault(e => e.ClaveUsuarios == claveUsuario && e.ClaveAcceso == claveAcceso);
            }
        }

        public static Empleados Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Empleados? empleados;

            try
            {
                empleados = contexto.Empleados.Where(e => e.EmpleadoId == id).SingleOrDefault();

            }
            catch (Exception)
            {
                throw;
            }
            finally { contexto.Dispose(); }

            return empleados;
        }

        public static bool Existe(string id, string id2)
        {
            bool confirmars = false;
            Contexto contexto = new Contexto();

            try
            {
                var confirmar = from Empleados in contexto.Empleados
                                where Empleados.ClaveUsuarios == id && Empleados.ClaveAcceso == id2
                                select Empleados;

                if (confirmar.Count() > 0)
                    confirmars = true;
                else
                    confirmars = false;
            }
            catch (Exception)
            {
                throw;
            }
            finally { contexto.Dispose(); }

            return confirmars;
        }

        private static bool Existe(int id)
        {
            bool confirmar = false;
            Contexto contexto = new Contexto();

            try
            {
                confirmar = contexto.Empleados.Any(e => e.EmpleadoId == id);
            }
            catch (Exception)
            {
                throw;
            }

            return confirmar;
        }

        public static List<Empleados> GetEmpleados()
        {
            List<Empleados> lista = new List<Empleados>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Empleados.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally { contexto.Dispose(); }

            return lista;
        }

        public static List<Empleados> GetList(Expression<Func<Empleados, bool>> criterio)
        {
            List<Empleados> lista = new List<Empleados>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Empleados?.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally { contexto.Dispose(); }

            return lista;
        }
       

        public static List<Areas> GetAreas()
        {
            List<Areas> lista = new List<Areas>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Areas.ToList();
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
        public static Dictionary<int, double> ObtenerHorasTrabajadas(DateTime fechaSeleccionada)
        {
            Dictionary<int, double> horasTrabajadasPorEmpleado = new Dictionary<int, double>();

            using (Contexto contexto = new Contexto())
            {
                try
                {
                    // Obtener todas las asistencias del día seleccionado
                    var asistencias = contexto.Asistencias
                        .Where(a => a.Fecha_Entrada.Date == fechaSeleccionada.Date || a.Fecha_Salida.Date == fechaSeleccionada.Date)
                        .ToList();

                    // Agrupar entradas y salidas por empleado
                    var entradasPorEmpleado = asistencias
                        .Where(a => a.Fecha_Entrada.Date == fechaSeleccionada.Date)
                        .GroupBy(a => a.EmpleadoId)
                        .ToDictionary(g => g.Key, g => g.ToList());

                    var salidasPorEmpleado = asistencias
                        .Where(a => a.Fecha_Salida.Date == fechaSeleccionada.Date)
                        .GroupBy(a => a.EmpleadoId)
                        .ToDictionary(g => g.Key, g => g.ToList());

                    // Mensaje de depuración
                    MessageBox.Show($"Entradas encontradas: {entradasPorEmpleado.Sum(e => e.Value.Count)}, Salidas encontradas: {salidasPorEmpleado.Sum(e => e.Value.Count)}");

                    // Calcular horas trabajadas
                    foreach (var entrada in entradasPorEmpleado)
                    {
                        int empleadoId = entrada.Key;
                        var listaEntradas = entrada.Value;

                        foreach (var e in listaEntradas)
                        {
                            // Buscar salida más cercana
                            var salida = salidasPorEmpleado.ContainsKey(empleadoId) ?
                                salidasPorEmpleado[empleadoId].FirstOrDefault(s => s.Fecha_Salida > e.Fecha_Entrada) : null;

                            if (salida != null)
                            {
                                double horasTrabajadas = (salida.Fecha_Salida - e.Fecha_Entrada).TotalHours;

                                // Mensaje de depuración
                                MessageBox.Show($"EmpleadoId: {empleadoId}, Horas Trabajadas: {horasTrabajadas}");

                                // Acumula las horas por empleado
                                if (horasTrabajadasPorEmpleado.ContainsKey(empleadoId))
                                {
                                    horasTrabajadasPorEmpleado[empleadoId] += horasTrabajadas;
                                }
                                else
                                {
                                    horasTrabajadasPorEmpleado[empleadoId] = horasTrabajadas;
                                }
                            }
                            else
                            {
                                MessageBox.Show($"No se encontró una salida válida para EmpleadoId {empleadoId} en la fecha {fechaSeleccionada.ToShortDateString()}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    MessageBox.Show($"Error: {ex.Message}");
                    throw;
                }
            }

            return horasTrabajadasPorEmpleado;
        }

        public static List<Vacaciones> GetVacaciones()
        {
            List<Vacaciones> lista = new List<Vacaciones>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Vacaciones.ToList();
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
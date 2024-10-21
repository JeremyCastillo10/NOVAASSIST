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
        public static List<Empleados> GetEmpleadosPorArea(int areaId)
        {
            using (var context = new Contexto())
            {
                return context.Empleados.Where(e => e.Area == areaId).ToList();
            }
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
        public static Dictionary<int, double> ObtenerHorasTrabajadas(DateTime fecha)
        {
            Dictionary<int, double> horasTrabajadasPorEmpleado = new Dictionary<int, double>();
            Contexto contexto = new Contexto();

            try
            {
                var asistencias = contexto.Asistencias.ToList();

                foreach (var asistencia in asistencias)
                {
                    double horasTrabajadas = (asistencia.Fecha_Salida - asistencia.Fecha_Entrada).TotalHours;

                    if (horasTrabajadasPorEmpleado.ContainsKey(asistencia.EmpleadoId))
                    {
                        horasTrabajadasPorEmpleado[asistencia.EmpleadoId] += horasTrabajadas;
                    }
                    else
                    {
                        horasTrabajadasPorEmpleado[asistencia.EmpleadoId] = horasTrabajadas;
                    }
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

            return horasTrabajadasPorEmpleado;
        }
        public static Dictionary<int, double> ObtenerHorasTrabajadasPorMes(int mes, int anio)
        {
            Dictionary<int, double> horasTrabajadasPorEmpleado = new Dictionary<int, double>();
            Contexto contexto = new Contexto();

            try
            {
                var asistencias = contexto.Asistencias
                    .Where(a => a.Fecha_Entrada.Month == mes && a.Fecha_Entrada.Year == anio)
                    .ToList();

                foreach (var asistencia in asistencias)
                {
                    if (asistencia.Fecha_Salida != DateTime.MinValue) // Asegurarse de que hay una salida registrada
                    {
                        double horasTrabajadas = (asistencia.Fecha_Salida - asistencia.Fecha_Entrada).TotalHours;

                        if (horasTrabajadasPorEmpleado.ContainsKey(asistencia.EmpleadoId))
                        {
                            horasTrabajadasPorEmpleado[asistencia.EmpleadoId] += horasTrabajadas;
                        }
                        else
                        {
                            horasTrabajadasPorEmpleado[asistencia.EmpleadoId] = horasTrabajadas;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se produjo un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return horasTrabajadasPorEmpleado;
        }
        public static Dictionary<int, double> ObtenerHorasTrabajadasDelMes(DateTime mes)
        {
            Dictionary<int, double> horasTrabajadasPorEmpleado = new Dictionary<int, double>();
            Contexto contexto = new Contexto();

            // Obtener el primer y último día del mes
            DateTime primerDia = new DateTime(mes.Year, mes.Month, 1);
            DateTime ultimoDia = primerDia.AddMonths(1).AddDays(-1);

            try
            {
                var asistencias = contexto.Asistencias
                    .Where(a => a.Fecha_Entrada >= primerDia && a.Fecha_Entrada <= ultimoDia)
                    .ToList();

                // Diccionario para llevar el control de entradas y salidas en un mismo día
                Dictionary<int, DateTime?> entradasDiarias = new Dictionary<int, DateTime?>();

                foreach (var asistencia in asistencias)
                {
                    // Si la entrada es la primera del día
                    if (!entradasDiarias.ContainsKey(asistencia.EmpleadoId))
                    {
                        // Registrar la entrada
                        entradasDiarias[asistencia.EmpleadoId] = asistencia.Fecha_Entrada;
                    }
                    else
                    {
                        // Si ya había una entrada registrada, se considera una salida
                        DateTime? fechaEntrada = entradasDiarias[asistencia.EmpleadoId];
                        if (fechaEntrada.HasValue)
                        {
                            double horasTrabajadas = (asistencia.Fecha_Entrada - fechaEntrada.Value).TotalHours;

                            if (horasTrabajadasPorEmpleado.ContainsKey(asistencia.EmpleadoId))
                            {
                                horasTrabajadasPorEmpleado[asistencia.EmpleadoId] += horasTrabajadas;
                            }
                            else
                            {
                                horasTrabajadasPorEmpleado[asistencia.EmpleadoId] = horasTrabajadas;
                            }

                            // Reiniciar entrada a null para que la próxima asistencia del mismo día sea una nueva entrada
                            entradasDiarias[asistencia.EmpleadoId] = null;
                        }
                    }
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

            return horasTrabajadasPorEmpleado;
        }


       
    }
}
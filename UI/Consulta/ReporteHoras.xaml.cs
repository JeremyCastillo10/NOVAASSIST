using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using NOVAASSIST.BLL;
using NOVAASSIST.Entidades;

namespace NOVAASSIST.UI.Consulta
{
    public partial class HorasTrabajadasR : Window
    {
        public HorasTrabajadasR()
        {
            InitializeComponent();
            // Inicialmente, el DataGrid estará vacío
            TablaTexto.ItemsSource = null;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (FechaPicker.SelectedDate.HasValue)
            {
                DateTime fechaSeleccionada = FechaPicker.SelectedDate.Value;
                CargarEmpleados(fechaSeleccionada); // Carga los empleados con la fecha seleccionada
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fecha.");
            }
        }

        private void CargarEmpleados(DateTime fecha)
        {
            var listado = EmpleadosBLL.GetList(e => e.EmpleadoEliminado == false);

            // Obtener los valores de los TextBox
            string nombreFiltro = NombreTextBox.Text.ToLower();
            string cedulaFiltro = CedulaTextBox.Text.ToLower();

            // Filtrar la lista por nombre y cédula
            var empleadosFiltrados = listado.Where(e =>
                (string.IsNullOrEmpty(nombreFiltro) || e.Nombre.ToLower().Contains(nombreFiltro)) &&
                (string.IsNullOrEmpty(cedulaFiltro) || e.Cedula.ToLower().Contains(cedulaFiltro))
            );

            var horasTrabajadas = EmpleadosBLL.ObtenerHorasTrabajadas(fecha); // Obtener horas trabajadas para la fecha seleccionada

            var reportes = empleadosFiltrados.Select(empleado => new
            {
                empleado.EmpleadoId,
                empleado.Nombre,
                empleado.Cedula,
                empleado.Genero,
                empleado.SalarioPorHora,
                HorasTrabajadas = horasTrabajadas.ContainsKey(empleado.EmpleadoId) ? horasTrabajadas[empleado.EmpleadoId] : 0,
                TotalMes = (horasTrabajadas.ContainsKey(empleado.EmpleadoId) ? horasTrabajadas[empleado.EmpleadoId] : 0) * empleado.SalarioPorHora
            }).ToList();

            // Asigna los reportes al DataGrid
            if (reportes.Any())
            {
                TablaTexto.ItemsSource = reportes; // Asigna la lista anónima al DataGrid
            }
            else
            {
                MessageBox.Show("No se encontraron horas trabajadas para la fecha seleccionada.");
                TablaTexto.ItemsSource = null; // Limpia el DataGrid
            }
        }
    }
}

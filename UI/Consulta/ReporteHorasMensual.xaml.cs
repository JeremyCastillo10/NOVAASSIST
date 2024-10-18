using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NOVAASSIST.BLL; // Asegúrate de incluir la referencia a tu BLL

namespace NOVAASSIST.UI.Consulta
{
    public partial class ReporteHorasMensual : Window
    {
        private readonly AsistenciasBLL horasTrabajadasBLL;

        public ReporteHorasMensual()
        {
            InitializeComponent();
            CargarMeses();
            horasTrabajadasBLL = new AsistenciasBLL(); // Inicializa tu BLL
        }

        private void CargarMeses()
        {
            MesComboBox.ItemsSource = new List<string>
            {
                "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
                "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
            };
        }

        private void GenerarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MesComboBox.SelectedItem == null || !int.TryParse(AnioTextBox.Text, out int anio))
            {
                MessageBox.Show("Por favor, seleccione un mes y escriba un año válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int mes = MesComboBox.SelectedIndex + 1; // ComboBox es cero-indexado

            // Lógica para obtener los datos del reporte
            var horasTrabajadas = ObtenerHorasTrabajadasDelMes(new DateTime(anio, mes, 1));

            // Llenar el DataGrid
            TablaTexto.ItemsSource = horasTrabajadas;
        }

        private List<EmpleadoReporte> ObtenerHorasTrabajadasDelMes(DateTime mes)
        {
            // Llama al método de la BLL para obtener las horas trabajadas
            var horasTrabajadasPorEmpleado = EmpleadosBLL.ObtenerHorasTrabajadas(mes); // Este método debe devolver un Dictionary<int, double>
            var empleados = EmpleadosBLL.GetEmpleados(); // Asumiendo que hay un método para obtener todos los empleados

            // Crea una lista para almacenar los reportes
            List<EmpleadoReporte> reportes = new List<EmpleadoReporte>();

            // Itera sobre los empleados y llena la lista de reportes
            foreach (var empleado in empleados)
            {
                // Intenta obtener las horas trabajadas del empleado
                double horasTrabajadas = horasTrabajadasPorEmpleado.ContainsKey(empleado.EmpleadoId)
                    ? horasTrabajadasPorEmpleado[empleado.EmpleadoId]
                    : 0;

                // Calcula el total mensual
                double totalMensual = horasTrabajadas * empleado.SalarioPorHora;

                // Crea un objeto de reporte y lo agrega a la lista
                reportes.Add(new EmpleadoReporte
                {
                    Nombre = empleado.Nombre,
                    Cedula = empleado.Cedula,
                    HorasTrabajadasMes = horasTrabajadas,
                    TotalMes = totalMensual
                });
            }

            return reportes; // Devuelve la lista de reportes
        }
    }

    public class EmpleadoReporte
    {
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public double HorasTrabajadasMes { get; set; }
        public double TotalMes { get; set; }
    }
}

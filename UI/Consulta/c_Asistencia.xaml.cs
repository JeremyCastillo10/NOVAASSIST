using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using NOVAASSIST.BLL;
using NOVAASSIST.Entidades;

namespace NOVAASSIST.UI.Consulta
{
    /// <summary>
    /// Interaction logic for c_Asistencia.xaml
    /// </summary>
    public partial class c_Asistencia : Window
    {
        public c_Asistencia()
        {
            InitializeComponent();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var lista = new List<Asistencias>();
            var empleado = new List<Empleados>();

            if (IdTextBox.Text != "" || NombreTextBox.Text != "" || CedulaTextBox.Text != "" || AreaTextBox.Text != "")
            {
                empleado = EmpleadosBLL.GetList(e => e.EmpleadoId.ToString() == IdTextBox.Text || e.Nombre == NombreTextBox.Text || e.Cedula == CedulaTextBox.Text || e.Area == AreaTextBox.Text);
                lista = AsistenciasBLL.GetList(e => (e.EmpleadoId.ToString() == IdTextBox.Text || e.EmpleadoId == empleado.First().EmpleadoId) || (DesdeDate.DisplayDate >= e.Fecha_Entrada && HastaDate.DisplayDate <= e.Fecha_Entrada));
            }
            else
            {
                lista = AsistenciasBLL.GetList(l => true);
            }
            AsistenciaDataGrid.ItemsSource = null;
            AsistenciaDataGrid.ItemsSource = lista;
            
        }
    }
}
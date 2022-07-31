using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using NOVAASSIST.BLL;
using NOVAASSIST.UI.Registros;
using NOVAASSIST.Entidades;

namespace NOVAASSIST.UI.Consulta
{
    /// <summary>
    /// Interaction logic for c_Excepciones.xaml
    /// </summary>
    public partial class c_Excepciones : Window
    {
        public c_Excepciones()
        {
            InitializeComponent();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Filtrados();
        }

        private void Actualizar_GotFocus(object sender, RoutedEventArgs e)
        {
            Filtrados();
        }

        private void Filtrados()
        {

        }

        private void Nuevo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Ver_Click(object sender, RoutedEventArgs e)
        {
            /* Empleados empleados = (Empleados)TablaTexto.SelectedItem;
            r_Excepciones empleadosRegistro = new r_Excepciones(Convert.ToInt32(empleados.EmpleadoId));
            empleadosRegistro.Show(); */
        }
    }
}
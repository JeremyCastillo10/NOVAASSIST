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
            var lista= AsistenciasBLL.GetList(e => true);

            AsistenciaDataGrid.ItemsSource = null;
            AsistenciaDataGrid.ItemsSource = lista;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var lista = new List<Asistencias>();

            if (!string.IsNullOrEmpty(IdTextBox.Text) || !string.IsNullOrEmpty(NombreTextBox.Text) || !string.IsNullOrEmpty(CedulaTextBox.Text) || !string.IsNullOrEmpty(AreaTextBox.Text))
            {
              
                if(!string.IsNullOrEmpty(IdTextBox.Text))
                    lista= AsistenciasBLL.GetList(e => e.AsistenciaId.ToString() == IdTextBox.Text);

                if(!string.IsNullOrEmpty(NombreTextBox.Text))
                {
                    foreach(var empleado in  AsistenciasBLL.GetList(e => e.Nombre.ToLower().Contains(NombreTextBox.Text.ToLower())))
                    {
                        if(!lista.Any(e => e.Equals(empleado)))
                            lista.Add(empleado);
                    }
                }
                if(!string.IsNullOrEmpty(CedulaTextBox.Text))
                {
                    foreach(var empleado in  AsistenciasBLL.GetList(e => e.cedula==CedulaTextBox.Text))
                    {
                        if(!lista.Any(e => e.Equals(empleado)))
                            lista.Add(empleado);
                    }
                }
            }
            else
            {
                lista = AsistenciasBLL.GetList(l => true);
            }

            AsistenciaDataGrid.ItemsSource = null;
            AsistenciaDataGrid.ItemsSource = lista;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            IdTextBox.Text = "";
            NombreTextBox.Text = "";
            CedulaTextBox.Text = "";
            DesdeDate.Text = "";
            DesdeDate.SelectedDate = null;
            HastaDate.Text = "";
            HastaDate.SelectedDate = null;
            TipoTextBox.Text = "";
            AreaTextBox.Text = "";
        }
    }
}
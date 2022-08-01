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
            Filtrados();
        }
        
        /* private void Actualizar_GotFocus(object sender, RoutedEventArgs e)
        {
            Filtrados();
        } */

        private void Filtrados()
        {
            var lista = new List<Asistencias>();

            if (!string.IsNullOrEmpty(IdTextBox.Text) || !string.IsNullOrEmpty(NombreTextBox.Text) || !string.IsNullOrEmpty(CedulaTextBox.Text) || !string.IsNullOrEmpty(AreaTextBox.Text))
            {
                if(!string.IsNullOrEmpty(IdTextBox.Text))
                    lista = AsistenciasBLL.GetList(e => e.AsistenciaId.ToString() == IdTextBox.Text);

                else if(!string.IsNullOrEmpty(NombreTextBox.Text) && !string.IsNullOrEmpty(CedulaTextBox.Text) && (DesdeDate.SelectedDate != null || HastaDate.SelectedDate != null))
                    lista = AsistenciasBLL.GetList(e => e.Nombre.ToLower().Contains(NombreTextBox.Text.ToLower()) && e.cedula.Contains(CedulaTextBox.Text) && ((e.Fecha_Entrada.Date >= DesdeDate.SelectedDate && e.Fecha_Salida.Date <= HastaDate.SelectedDate) || (e.Fecha_Salida.Date >= DesdeDate.SelectedDate && e.Fecha_Salida.Date <= HastaDate.SelectedDate)));

                else if(!string.IsNullOrEmpty(NombreTextBox.Text) && !string.IsNullOrEmpty(CedulaTextBox.Text))
                    lista = AsistenciasBLL.GetList(e => e.Nombre.ToLower().Contains(NombreTextBox.Text.ToLower()) && e.cedula.Contains(CedulaTextBox.Text));

                else if(!string.IsNullOrEmpty(NombreTextBox.Text) && (DesdeDate.SelectedDate != null || HastaDate.SelectedDate != null))
                    lista = AsistenciasBLL.GetList(e => e.Nombre.ToLower().Contains(NombreTextBox.Text.ToLower()) && ((e.Fecha_Entrada.Date >= DesdeDate.SelectedDate && e.Fecha_Salida.Date <= HastaDate.SelectedDate) || (e.Fecha_Salida.Date >= DesdeDate.SelectedDate && e.Fecha_Salida.Date <= HastaDate.SelectedDate)));

                else if((DesdeDate.SelectedDate != null || HastaDate.SelectedDate != null) && !string.IsNullOrEmpty(CedulaTextBox.Text))
                    lista = AsistenciasBLL.GetList(e => ((e.Fecha_Entrada.Date >= DesdeDate.SelectedDate && e.Fecha_Salida.Date <= HastaDate.SelectedDate) || (e.Fecha_Salida.Date >= DesdeDate.SelectedDate && e.Fecha_Salida.Date <= HastaDate.SelectedDate)) && e.cedula.Contains(CedulaTextBox.Text));

                else if(!string.IsNullOrEmpty(NombreTextBox.Text))
                    lista = AsistenciasBLL.GetList(e => e.Nombre.ToLower().Contains(NombreTextBox.Text.ToLower()));

                else if(!string.IsNullOrEmpty(CedulaTextBox.Text))
                    lista = AsistenciasBLL.GetList(e => e.cedula.Contains(CedulaTextBox.Text));

                else if(DesdeDate.SelectedDate != null || HastaDate.SelectedDate != null)
                    lista = AsistenciasBLL.GetList(e => (e.Fecha_Entrada.Date >= DesdeDate.SelectedDate && e.Fecha_Salida.Date <= HastaDate.SelectedDate) || (e.Fecha_Salida.Date >= DesdeDate.SelectedDate && e.Fecha_Salida.Date <= HastaDate.SelectedDate));
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
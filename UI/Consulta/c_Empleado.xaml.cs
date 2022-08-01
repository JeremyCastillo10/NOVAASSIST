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
using NOVAASSIST.UI.Registros;
using NOVAASSIST.BLL;
using NOVAASSIST.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace NOVAASSIST.UI.Consulta
{
    /// <summary>
    /// Interaction logic for rConsultarEmpleado.xaml
    /// </summary>
    public partial class c_Empleado : Window
    {
        public c_Empleado()
        {
            InitializeComponent();
            var listado = EmpleadosBLL.GetList(e => true && e.EmpleadoEliminado == false);

            TablaTexto.ItemsSource = null;
            TablaTexto.ItemsSource = listado;
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
            var listado = new List<Empleados>();
            
            if (!string.IsNullOrEmpty(Idtexbo.Text) || !string.IsNullOrEmpty(nombretexbo.Text) || !string.IsNullOrEmpty(GeneroTextBox.Text) || !string.IsNullOrEmpty(emailtexbo.Text) || !string.IsNullOrEmpty(cedulatexbo.Text) || !string.IsNullOrEmpty(telefonotexbo.Text) || desdetexbo.SelectedDate != null || hastatexbo.SelectedDate != null)
            {
                if (desdetexbo.SelectedDate != null || hastatexbo.SelectedDate != null)
                    listado = EmpleadosBLL.GetList(c => (c.FechaNacimiento.Date >= desdetexbo.SelectedDate && c.FechaNacimiento.Date <= hastatexbo.SelectedDate) && c.EmpleadoEliminado == false);
                if (!string.IsNullOrEmpty(Idtexbo.Text))
                {
                    foreach (var empleado in EmpleadosBLL.GetList(e => e.EmpleadoId.ToString() == Idtexbo.Text && e.EmpleadoEliminado == false))
                    {
                        if (!listado.Any(e => e.Equals(empleado)))
                            listado.Add(empleado);
                    }
                }
                if (!string.IsNullOrEmpty(nombretexbo.Text))
                {
                    foreach (var empleado in EmpleadosBLL.GetList(e => e.Nombre.ToLower().Contains(nombretexbo.Text.ToLower()) && e.EmpleadoEliminado == false))
                    {
                         if (!listado.Any(e => e.Equals(empleado)))
                            listado.Add(empleado);
                    }
                }
                if (!string.IsNullOrEmpty(emailtexbo.Text))
                {
                    foreach (var empleado in EmpleadosBLL.GetList(e => e.Email.ToLower().Contains(emailtexbo.Text.ToLower()) && e.EmpleadoEliminado == false))
                    {
                        if (!listado.Any(e => e.Equals(empleado)))
                            listado.Add(empleado);
                    }
                }
                if (!string.IsNullOrEmpty(GeneroTextBox.Text))
                {
                    foreach (var empleado in EmpleadosBLL.GetList(e => e.Genero.ToLower() == GeneroTextBox.Text.ToLower() && e.EmpleadoEliminado == false))
                    {
                        if (!listado.Any(e => e.Equals(empleado)))
                            listado.Add(empleado);
                    }
                }
                if (!string.IsNullOrEmpty(telefonotexbo.Text))
                {
                    foreach (var empleado in EmpleadosBLL.GetList(e => e.Telefono == telefonotexbo.Text && e.EmpleadoEliminado == false))
                    {
                        if (!listado.Any(e => e.Equals(empleado)))
                            listado.Add(empleado);
                    }
                }
                if (!string.IsNullOrEmpty(cedulatexbo.Text))
                {
                    foreach (var empleado in EmpleadosBLL.GetList(e => e.Cedula == cedulatexbo.Text && e.EmpleadoEliminado == false))
                    {
                        if (!listado.Any(e => e.Equals(empleado)))
                            listado.Add(empleado);
                    }
                }
            }
            else
            {
                listado = EmpleadosBLL.GetList(e => true && e.EmpleadoEliminado == false);
            }

            TablaTexto.ItemsSource = null;
            TablaTexto.ItemsSource = listado;
        }

        private void Nuevo_Click(object sender, RoutedEventArgs e)
        {
            Idtexbo.Text = "";
            nombretexbo.Text = "";
            cedulatexbo.Text = "";
            emailtexbo.Text = "";
            GeneroTextBox.Text = "";
            telefonotexbo.Text = "";
            desdetexbo.Text = "";
            desdetexbo.SelectedDate = null;
            hastatexbo.Text = "";
            hastatexbo.SelectedDate = null;
        }

        private void Ver_Click(object sender, RoutedEventArgs e)
        {
            Empleados empleados = (Empleados)TablaTexto.SelectedItem;            
            rEmpleados empleadosRegistro = new rEmpleados(Convert.ToInt32(empleados.EmpleadoId));
            empleadosRegistro.Show();
        }

        private void Idtexbo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void nombretexbo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cedulatexbo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void emailtexbo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cargotexbo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void telefonotexbo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TablaTexto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
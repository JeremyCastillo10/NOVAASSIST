using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using NOVAASSIST.BLL;
using NOVAASSIST.Entidades;
using NOVAASSIST.UI.Registros;

namespace NOVAASSIST.UI.Consulta
{
    public partial class c_Empleado : Window
    {
        public c_Empleado()
        {
            InitializeComponent();
            CargarEmpleados();
        }

        private void CargarEmpleados()
        {
            var listado = EmpleadosBLL.GetList(e => e.EmpleadoEliminado == false);
            TablaTexto.ItemsSource = null;
            TablaTexto.ItemsSource = listado;
        }


        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Filtrados();
        }

        private void Filtrados()
        {
            var listado = new List<Empleados>();

            // Lógica de filtrado
            if (!string.IsNullOrEmpty(Idtexbo.Text) || !string.IsNullOrEmpty(nombretexbo.Text) ||
                !string.IsNullOrEmpty(GeneroTextBox.Text) || !string.IsNullOrEmpty(emailtexbo.Text) ||
                !string.IsNullOrEmpty(cedulatexbo.Text) || !string.IsNullOrEmpty(telefonotexbo.Text) ||
                desdetexbo.SelectedDate != null || hastatexbo.SelectedDate != null)
            {
                listado = EmpleadosBLL.GetList(e => e.EmpleadoEliminado == false);

                // Filtrar por otros criterios
                if (!string.IsNullOrEmpty(Idtexbo.Text))
                {
                    listado = listado.FindAll(e => e.EmpleadoId.ToString() == Idtexbo.Text);
                }
                if (!string.IsNullOrEmpty(nombretexbo.Text))
                {
                    listado = listado.FindAll(e => e.Nombre.ToLower().Contains(nombretexbo.Text.ToLower()));
                }
                if (!string.IsNullOrEmpty(emailtexbo.Text))
                {
                    listado = listado.FindAll(e => e.Email.ToLower().Contains(emailtexbo.Text.ToLower()));
                }
                if (!string.IsNullOrEmpty(GeneroTextBox.Text))
                {
                    listado = listado.FindAll(e => e.Genero.ToLower() == GeneroTextBox.Text.ToLower());
                }
                if (!string.IsNullOrEmpty(telefonotexbo.Text))
                {
                    listado = listado.FindAll(e => e.Telefono == telefonotexbo.Text);
                }
                if (!string.IsNullOrEmpty(cedulatexbo.Text))
                {
                    listado = listado.FindAll(e => e.Cedula == cedulatexbo.Text);
                }
            }
            else
            {
                listado = EmpleadosBLL.GetList(e => e.EmpleadoEliminado == false);
            }


            TablaTexto.ItemsSource = null;
            TablaTexto.ItemsSource = listado;
        }

        private void Ver_Click(object sender, RoutedEventArgs e)
        {
            Empleados empleados = (Empleados)TablaTexto.SelectedItem;
            rEmpleados empleadosRegistro = new rEmpleados(Convert.ToInt32(empleados.EmpleadoId));
            empleadosRegistro.Show();
        }

        private void Nuevo_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para limpiar los campos
            Idtexbo.Text = "";
            nombretexbo.Text = "";
            cedulatexbo.Text = "";
            emailtexbo.Text = "";
            GeneroTextBox.Text = "";
            telefonotexbo.Text = "";
            desdetexbo.SelectedDate = null;
            hastatexbo.SelectedDate = null;
        }

        private void Idtexbo_TextChanged(object sender, TextChangedEventArgs e) { }
        private void nombretexbo_TextChanged(object sender, TextChangedEventArgs e) { }
        private void cedulatexbo_TextChanged(object sender, TextChangedEventArgs e) { }
        private void emailtexbo_TextChanged(object sender, TextChangedEventArgs e) { }
        private void telefonotexbo_TextChanged(object sender, TextChangedEventArgs e) { }
        private void TablaTexto_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
    }
}

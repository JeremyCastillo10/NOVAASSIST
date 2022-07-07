using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using NOVAASSIST.BLL;
using NOVAASSIST.Entidades;


namespace NOVAASSIST.UI.Registros
{
    public partial class rEmpleados : Window
    {
        private Empleados empleados = new Empleados();

        public rEmpleados()
        {
            InitializeComponent();
            this.DataContext = empleados;

            AreaTextBox.ItemsSource = EmpleadosBLL.GetAreas();
            AreaTextBox.SelectedValuePath = "AreaId";
            AreaTextBox.DisplayMemberPath = "Nombre";

            VacacionesTextBox.ItemsSource = EmpleadosBLL.GetVacaciones();
            VacacionesTextBox.SelectedValuePath = "VacacionesId";
            VacacionesTextBox.DisplayMemberPath = "Descripcion";
        }

        public rEmpleados(int id)
        {
            InitializeComponent();

            var encontro = EmpleadosBLL.Buscar(id);

            if(encontro != null)
            {
                empleados = encontro;
                IdTextBox.IsEnabled = false;
                 Cargar();
            }

           // this.DataContext = EmpleadosBLL.Buscar(id);
        }

        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = this.empleados;
            CedulaTextBox.Text = empleados.Cedula;
            
        }

        private bool Validar()
        {
            bool valido = true;
            string mensaje = "Tiene que llenar ";

            if (string.IsNullOrWhiteSpace(NombreTextBox.Text) && string.IsNullOrWhiteSpace(AreaTextBox.Text) && string.IsNullOrWhiteSpace(GeneroTextBox.Text) && string.IsNullOrWhiteSpace(EmailTextBox.Text) && string.IsNullOrWhiteSpace(CedulaTextBox.Text) &&
            string.IsNullOrWhiteSpace(TelefonoTextBox.Text) && string.IsNullOrWhiteSpace(UsuarioTextBox.Text) && string.IsNullOrWhiteSpace(ClaveTextBox.Text))
            {
                valido = false;
                MessageBox.Show("Tiene que llenar todo los campos", "Validacion", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(NombreTextBox.Text))
                {
                    valido = false;
                    NombreTextBox.Focus();
                    mensaje += ", Nombre";
                }
                if (string.IsNullOrWhiteSpace(AreaTextBox.Text))
                {
                    valido = false;
                    AreaTextBox.Focus();
                    mensaje += ", Area";
                }
                if (string.IsNullOrWhiteSpace(GeneroTextBox.Text))
                {
                    valido = false;
                    GeneroTextBox.Focus();
                    mensaje += ", Genero";
                }
                if (string.IsNullOrWhiteSpace(EmailTextBox.Text) && ValidarEmail(EmailTextBox.Text) == false)
                {
                    valido = false;
                    EmailTextBox.Focus();
                    mensaje += ", Email";
                }

                if (string.IsNullOrWhiteSpace(CedulaTextBox.Text))
                {
                    valido = false;
                    CedulaTextBox.Focus();
                    mensaje += ", Cedula";
                }
                else
                {
                    if (Regex.IsMatch(CedulaTextBox.Text, @"^[0-9]+$"))
                    {
                        if (CedulaTextBox.Text.Length != 11)
                        {
                            MessageBox.Show("La cedula solo tiene 11 digito", "Validacion", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("La cedula solo admite numero", "Validacion", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                if (string.IsNullOrWhiteSpace(TelefonoTextBox.Text))
                {
                    valido = false;
                    TelefonoTextBox.Focus();
                    mensaje += ", Telefono";
                }
                else
                {
                    if (Regex.IsMatch(TelefonoTextBox.Text, @"^[0-9]+$"))
                    {
                        if (TelefonoTextBox.Text.Length != 10)
                        {
                            MessageBox.Show("El telefono solo tiene 10 digito", "Validacion", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El telefono solo admite numero", "Validacion", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                if (string.IsNullOrWhiteSpace(UsuarioTextBox.Text) || (UsuarioTextBox.Text.Length != 6) && (UsuarioTextBox.Text.Length != 7) && (UsuarioTextBox.Text.Length != 8))
                {
                    valido = false;
                    UsuarioTextBox.Focus();
                    mensaje += ", Usuiario";
                }
                if (string.IsNullOrWhiteSpace(ClaveTextBox.Text) || (ClaveTextBox.Text.Length != 6) && (ClaveTextBox.Text.Length != 7) && (ClaveTextBox.Text.Length != 8))
                {
                    valido = false;
                    ClaveTextBox.Focus();
                    mensaje += ", Clave";
                }
                if (FechaTextBox == null)
                {
                    valido = false;
                    FechaTextBox.Focus();
                    mensaje += ", Fecha";
                }

                MessageBox.Show(mensaje, "Validacion", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return valido;
        }

        private void Limpiar()
        {
            this.empleados = new Empleados();
            this.DataContext = empleados;
            CedulaTextBox.Text = "";
            GeneroTextBox.Text = "";
        }

        private void Telefono_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]").IsMatch(e.Text);
        }

        public static bool ValidarEmail(string comprobarEmail)
        {
            string emailFormato;
            emailFormato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(comprobarEmail, emailFormato))
            {
                if (Regex.Replace(comprobarEmail, emailFormato, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            IdTextBox.IsEnabled = true;
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            empleados.Cedula = CedulaTextBox.Text;
            empleados.Genero = GeneroTextBox.Text;

            bool pasa = false;
            if (!Validar())
                return;

            pasa = EmpleadosBLL.Insertar(empleados);

            if (pasa)
            {
                MessageBox.Show("empleado guardado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
            else
                MessageBox.Show("No se pudo Guardar el empleadoo", "fallo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {

            var encontro = EmpleadosBLL.Buscar(Convert.ToInt32(empleados.EmpleadoId));

            if(encontro != null)
            {
                empleados = encontro;

                if(empleados.EmpleadoEliminado == false)
                {
                    empleados.EmpleadoEliminado = true;

                    if(EmpleadosBLL.Modificar(empleados))
                    {
                        MessageBox.Show("Empleado eliminado", "Exito",
                            MessageBoxButton.OK);
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("No se fue posible eliminar el empleado", "Fallo",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Empleado ya esta en estado eliminado", "Exito",
                            MessageBoxButton.OK);

                }
                
            }
            else
            {
                MessageBox.Show("Empleado no existe");
            }






            /*
            if (EmpleadosBLL.Eliminar(Convert.ToInt32(IdTextBox.Text)))
            {
                MessageBox.Show("Se puedo guardar el empleado", "Exito",
                    MessageBoxButton.OK);
                Limpiar();
            }
            else
            {
                MessageBox.Show("No se fue posible eliminar empleado", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            */
        }
    }
}
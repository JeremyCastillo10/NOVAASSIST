using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NOVAASSIST.BLL;
using NOVAASSIST.Entidades;
using NOVAASSIST.UI.Consulta;

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
            CargarHoras();
        }

        private void CargarHoras()
        {
            for (int i = 0; i < 24; i++)
            {
                string hourString = $"{i:D2}:00";
                HoraEntradaComboBox.Items.Add(new ComboBoxItem { Content = hourString });
                HoraSalidaComboBox.Items.Add(new ComboBoxItem { Content = hourString });
            }
        }


        public rEmpleados(int id)
        {
            InitializeComponent();

            var encontro = EmpleadosBLL.Buscar(id);
            if (encontro != null)
            {
                empleados = encontro;

                AreaTextBox.ItemsSource = EmpleadosBLL.GetAreas();
                AreaTextBox.SelectedValuePath = "AreaId";
                AreaTextBox.DisplayMemberPath = "Nombre";

                VacacionesTextBox.ItemsSource = EmpleadosBLL.GetVacaciones();
                VacacionesTextBox.SelectedValuePath = "VacacionesId";
                VacacionesTextBox.DisplayMemberPath = "Descripcion";
                Cargar();
            }
        }

        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = this.empleados;
            CedulaTextBox.Text = empleados.Cedula;
        }

        private bool Validar()
        {
            List<string> errores = new List<string>();

            if (string.IsNullOrWhiteSpace(NombreTextBox.Text))
                errores.Add("Nombre es requerido.");

            if (string.IsNullOrWhiteSpace(AreaTextBox.Text))
                errores.Add("Area es requerido.");

            if (string.IsNullOrWhiteSpace(GeneroTextBox.Text))
                errores.Add("Genero es requerido.");

            if (string.IsNullOrWhiteSpace(CedulaTextBox.Text))
                errores.Add("Cedula es requerida.");

            if (string.IsNullOrWhiteSpace(TelefonoTextBox.Text))
                errores.Add("Telefono es requerido.");

            if (string.IsNullOrWhiteSpace(EmailTextBox.Text) || !Regex.IsMatch(EmailTextBox.Text, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
                errores.Add("Email inválido.");

            if (errores.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, errores), "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void Limpiar()
        {
            this.empleados = new Empleados();
            this.DataContext = empleados;

            CedulaTextBox.Text = "";
            GeneroTextBox.Text = "";
            TelefonoTextBox.Text = "";
            EmailTextBox.Text = "";
            NombreTextBox.Text = "";
            SalarioTextBox.Text = "";
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            // Verificar la selección de horas
            var horaEntradaSeleccionada = HoraEntradaComboBox.SelectedItem as ComboBoxItem;
            var horaSalidaSeleccionada = HoraSalidaComboBox.SelectedItem as ComboBoxItem;

            if (horaEntradaSeleccionada == null || horaSalidaSeleccionada == null)
            {
                MessageBox.Show("Por favor, selecciona tanto la hora de entrada como la de salida.");
                return;
            }
            if (!Validar())
                return;

            empleados.Cedula = CedulaTextBox.Text;
            empleados.Genero = GeneroTextBox.Text;
            empleados.Telefono = TelefonoTextBox.Text;
            empleados.Email = EmailTextBox.Text;
            empleados.Nombre = NombreTextBox.Text;
            empleados.HoraEntrada = horaEntradaSeleccionada.Content.ToString();
            empleados.HoraSalida = horaSalidaSeleccionada.Content.ToString();
            if (double.TryParse(SalarioTextBox.Text, out double salario))
            {
                empleados.SalarioPorHora = salario;
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un valor numérico válido para el salario.");
                return;
            }
            // Generar un usuario aleatorio y asignarlo
            empleados.ClaveUsuarios = GenerarUsuarioAleatorio();
            empleados.ClaveAcceso = ClaveTextBox.Text;

            foreach (var item in EmpleadosBLL.GetAreas())
            {
                if (item.AreaId == empleados.Area)
                    empleados.Area = item.AreaId;
            }

            foreach (var item in EmpleadosBLL.GetVacaciones())
            {
                if (item.VacacionesId == empleados.Vacaciones)
                    empleados.Vacaciones = item.VacacionesId;
            }


            if (EmpleadosBLL.Guardar(empleados))
            {
                MessageBox.Show("Empleado guardado. Usuario: " + empleados.ClaveUsuarios, "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
            else
                MessageBox.Show("No se pudo guardar el empleado", "Fallo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var consulta = new c_Empleado();
            consulta.Show();
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementa la lógica de eliminación aquí.
        }

        private void Telefono_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private string GenerarUsuarioAleatorio()
        {
            Random random = new Random();
            return random.Next(1000, 9999).ToString(); // Genera un número entre 8 dígitos
        }
    }
}

using System.Windows;
using NOVAASSIST.Entidades;
using NOVAASSIST.UI.Consulta;
using NOVAASSIST.UI.Registros;
using NOVAASSIST.UI;

namespace NOVAASSIST
{
    public partial class MainWindow : Window
    {
        private string rolUsuario;

        public MainWindow(string rol)
        {
            InitializeComponent();
            rolUsuario = rol;
            ConfigurarMenu();
        }

        private void ConfigurarMenu()
        {
            // Configuración según el rol del usuario
            if (rolUsuario == "Administrador")
            {
                // Administrador: tiene acceso completo
                RegistrosMenu.Visibility = Visibility.Visible;
                ConsultasMenu.Visibility = Visibility.Visible;
                ReportesMenu.Visibility = Visibility.Visible;
            }
            else if (rolUsuario == "Supervisor")
            {
                // Supervisor: acceso limitado
                RegistrosMenu.Visibility = Visibility.Collapsed; // No puede registrar
                ConsultasMenu.Visibility = Visibility.Collapsed;
                ReportesMenu.Visibility = Visibility.Visible;
            }
            else if (rolUsuario == "RRHH")
            {
                // RRHH: acceso a registros de empleados y consultas
                RegistrosMenu.Visibility = Visibility.Visible; // Puede registrar
                ConsultasMenu.Visibility = Visibility.Visible;
                ReportesMenu.Visibility = Visibility.Visible; // No puede ver reportes
            }
            else
            {
                // Si el rol no es reconocido
                MessageBox.Show("Rol de usuario no reconocido", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // Implementar la lógica para regresar o cerrar la ventana
            this.Close();
        }

        private void RegistroEmpleados_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para abrir la ventana de registro de empleados
            rEmpleados m = new rEmpleados();
            m.Show();
        }

        private void RegistroUsuarios_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para abrir la ventana de registro de usuarios
            Registro m = new Registro();
            m.Show();
        }

        private void ConsultaAsistencia_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para abrir la ventana de consulta de asistencia
            c_Asistencia m = new c_Asistencia();
            m.Show();
        }

        private void ConsultaEmpleados_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para abrir la ventana de consulta de empleados
            c_Empleado m = new c_Empleado();
            m.Show();
        }

        private void ReportesEmpleados_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para abrir la ventana de reportes de empleados
            HorasTrabajadasR horasTrabajadasWindow = new HorasTrabajadasR();
            horasTrabajadasWindow.ShowDialog();
        }
    }
}

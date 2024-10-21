using System;
using System.Windows;
using NOVAASSIST.BLL;
using NOVAASSIST.Entidades;

namespace NOVAASSIST.UI
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = NombreUsuarioTextBox.Text;
            string password = ClavePasswordBox.Password;

            // Aquí se llama al método de la BLL para validar el usuario
            var usuario = UsuariosBLL.ValidarUsuario(username, password);

            if (usuario != null)
            {
                // Aquí puedes abrir la ventana principal basada en el rol del usuario
                MainWindow mainWindow = new MainWindow(usuario.Rol);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Error de Login", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

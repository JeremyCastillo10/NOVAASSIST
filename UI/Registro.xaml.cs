using System;
using System.Collections.Generic;
using System.Windows;
using NOVAASSIST.BLL;
using NOVAASSIST.Entidades;

namespace NOVAASSIST.UI
{
    public partial class Registro : Window
    {
        private readonly List<string> rolesDisponibles = new List<string>
        {
            "Administrador",
            "Supervisor",
            "RRHH"
        };

        public Registro()
        {
            InitializeComponent();
            CargarAreas();
            CargarRoles();
            GenerarNombreAleatorio();
        }

        private void CargarAreas()
        {
            try
            {
                var areas = UsuariosBLL.GetAreas(); // Asegúrate de que este método retorna una lista de áreas
                AreaComboBox.ItemsSource = areas;

                // Establece qué propiedad mostrar
                AreaComboBox.DisplayMemberPath = "Nombre"; // Cambia 'Nombre' por la propiedad que deseas mostrar
                AreaComboBox.SelectedValuePath = "AreaId"; // Para obtener el ID del área seleccionada
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar áreas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CargarRoles()
        {
            RolComboBox.ItemsSource = rolesDisponibles;
        }

        private void GenerarNombreAleatorio()
        {
            var random = new Random();
            var letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var numeros = "0123456789";

            var nombre = new char[3];
            for (int i = 0; i < 3; i++)
            {
                nombre[i] = letras[random.Next(letras.Length)];
            }

            var num1 = numeros[random.Next(numeros.Length)];
            var num2 = numeros[random.Next(numeros.Length)];
            var nombreGenerado = $"{new string(nombre)}{num1}{num2}";
            NombreUsuarioTextBlock.Text = nombreGenerado;

            MessageBox.Show($"El nombre de usuario generado es: {nombreGenerado}", "Nombre de Usuario", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RegistrarButton_Click(object sender, RoutedEventArgs e)
        {
            if (AreaComboBox.SelectedItem is Areas areaSeleccionada)
            {
                var nuevoUsuario = new Usuario
                {
                    Nombre = NombreUsuarioTextBlock.Text,
                    Clave = ClavePasswordBox.Password,
                    AreaId = areaSeleccionada.AreaId,
                    Rol = RolComboBox.SelectedItem?.ToString()
                };

                if (UsuariosBLL.RegistrarUsuario(nuevoUsuario))
                {
                    MessageBox.Show("Usuario registrado exitosamente.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("El nombre de usuario ya existe.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un área.");
            }
        }
    }
}

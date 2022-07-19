using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

using System.Media;
using System.Threading;
using NOVAASSIST.BLL;
using NOVAASSIST.Entidades;
using NOVAASSIST.UI.Consulta;

namespace NOVAASSIST.UI.Registros
{
    public partial class r_Excepciones : Window
    {
        private Excepciones Excepciones = new Excepciones();

        public r_Excepciones()
        {
            InitializeComponent();
            this.DataContext = Excepciones;
        }

        public r_Excepciones(int id)
        {
            InitializeComponent();
            this.DataContext = Excepciones;
        }
        private void Limpiar()
        {
            this.Excepciones = new Excepciones();
            this.DataContext = Excepciones;
        }
        private bool Validar()
        {
            bool valido = true;
            if(string.IsNullOrWhiteSpace(Excepciones.Descripcion) && string.IsNullOrWhiteSpace(Excepciones.Nombre))
            {
                valido = false;
                MessageBox.Show("Tiene que llenar todo los campo para poder guardar", "Validacion", MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
            {
                if(string.IsNullOrWhiteSpace(Excepciones.Descripcion))
                {
                    valido = false;
                    descripciontext.Focus();
                    MessageBox.Show("Indique la descripcion de la excepcion", "Validacion", MessageBoxButton.OK,MessageBoxImage.Error);
                }
                if(string.IsNullOrWhiteSpace(Excepciones.Nombre))
                {
                    valido = false;
                    nombretext.Focus();
                    MessageBox.Show("Indique el nombre de la excepcion", "Validacion", MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }

            return valido;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        
        public void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            c_Excepciones m = new c_Excepciones();
            m.Show();            
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool confirmar = false;

            if(!string.IsNullOrEmpty(nombretext.Text)){
            
            confirmar = ExcepcionesBLL.Guardar(Excepciones);

            if (confirmar)
            {
                MessageBox.Show("Se pudo guardar", "Exito",
                    MessageBoxButton.OK);
                Limpiar();
            }
            else
            {
                MessageBox.Show("¡¡No se puedo guardar!!", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            }else{
                MessageBox.Show("Debe ingresar un nombre ", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (ExcepcionesBLL.Eliminar(Excepciones.ExcepcionId))
            {
                Limpiar();
                MessageBox.Show("eliminado con éxito", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No se pudo eliminar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
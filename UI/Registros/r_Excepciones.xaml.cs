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
        private Excepciones excepciones = new Excepciones();

        public r_Excepciones()
        {
            InitializeComponent();
            this.DataContext = excepciones;
        }

        public r_Excepciones(int id)
        {
            InitializeComponent();
            this.DataContext = excepciones;
        }

        private void Limpiar()
        {
            this.excepciones = new Excepciones();
            this.DataContext = excepciones;
        }

        private bool Validar()
        {
            bool valido = true;
            if(string.IsNullOrWhiteSpace(excepciones.Descripcion) && string.IsNullOrWhiteSpace(excepciones.Nombre))
            {
                valido = false;
                MessageBox.Show("Tiene que llenar todo los campo para poder guardar", "Validacion", MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
            {
                if(string.IsNullOrWhiteSpace(excepciones.Descripcion))
                {
                    valido = false;
                    descripciontext.Focus();
                    MessageBox.Show("Indique la descripcion de la excepcion", "Validacion", MessageBoxButton.OK,MessageBoxImage.Error);
                }
                if(string.IsNullOrWhiteSpace(excepciones.Nombre))
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
        


        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool confirmar = false;

            if(!string.IsNullOrEmpty(nombretext.Text)){
            
            confirmar = ExcepcionesBLL.Guardar(excepciones);

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
            var encontro = ExcepcionesBLL.Buscar(Convert.ToInt32(excepciones.ExcepcionId));

            if(encontro != null)
            {
                excepciones = encontro;

                if(excepciones.ExcepcionEliminada == false)
                {
                    excepciones.ExcepcionEliminada = true;

                    if(ExcepcionesBLL.Modificar(excepciones))
                    {
                        MessageBox.Show("Excepción eliminada", "Exito",
                            MessageBoxButton.OK);
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("No se fue posible eliminar el excepción", "Fallo",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Excepción ya eliminada fue restaurado", "Exito",
                            MessageBoxButton.OK);
                    excepciones.ExcepcionEliminada = false;
                    ExcepcionesBLL.Modificar(excepciones);
                    Limpiar();
                }
            }
            else
            {
                MessageBox.Show("La excepción no existe");
            }
        }
    }
}
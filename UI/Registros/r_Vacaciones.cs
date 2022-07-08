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
using System.Windows.Threading;

using System.Media;
using System.Threading;
using NOVAASSIST.BLL;
using NOVAASSIST.Entidades;

namespace NOVAASSIST.UI.Registros
{
    public partial class r_Vacaciones : Window
    {
        private Vacaciones vacaciones = new Vacaciones();
        public r_Vacaciones()
        {
            InitializeComponent();
            this.DataContext = vacaciones;
        }

        private void Limpiar()
        {
            this.vacaciones = new Vacaciones();
            this.DataContext = vacaciones;
        }
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();

        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool confirmar = false;

            if(!string.IsNullOrEmpty(descriciontext.Text)){
            
            confirmar = VacacionesBLL.Guardar(vacaciones);

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
                MessageBox.Show("Debe ingresar una descripcion ", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                
            }

        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (VacacionesBLL.Eliminar(vacaciones.VacacionesId))
            {
                Limpiar();
                MessageBox.Show("eliminado con éxito", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No se pudo eliminar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
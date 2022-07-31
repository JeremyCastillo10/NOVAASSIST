using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using NOVAASSIST.BLL;
using NOVAASSIST.Entidades;
using NOVAASSIST.UI.Registros;

namespace NOVAASSIST.UI.Consulta
{
    /// <summary>
    /// Interaction logic for c_Excepciones.xaml
    /// </summary>
    public partial class c_Excepciones : Window
    {
    
         public c_Excepciones()
        {
            InitializeComponent();
            var listado = ExcepcionesBLL.GetList(e => true && e.ExcepcionEliminada == false);

            TablaTexto.ItemsSource = null;
            TablaTexto.ItemsSource = listado;
        }


        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Filtrados();
        }

        private void Actualizar_GotFocus(object sender, RoutedEventArgs e)
        {
            Filtrados();
        }

        private void Filtrados()
        {

        }
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            IdTextBox.Text = "";
            DescripcionTextBox.Text = "";
        }

        private void Ver_Click(object sender, RoutedEventArgs e)
        {
            Excepciones horarios = (Excepciones)TablaTexto.SelectedItem;            
            r_Excepciones registroHorarios = new r_Excepciones(Convert.ToInt32(horarios.ExcepcionId));
            registroHorarios.Show();
        }
    }
}
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
using NOVAASSIST.UI.Registros;
using NOVAASSIST.BLL;
using NOVAASSIST.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace NOVAASSIST.UI.Consulta
{
    /// <summary>
    /// Interaction logic for rConsultarEmpleado.xaml
    /// </summary>
    public partial class c_Horarios : Window
    {
        public c_Horarios()
        {
            InitializeComponent();
            var listado = HorariosBLL.GetList(e => true && e.HorarioEliminado == false);

            TablaTexto.ItemsSource = null;
            TablaTexto.ItemsSource = listado;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Horarios>();

            if (!string.IsNullOrEmpty(IdTextBox.Text) || !string.IsNullOrEmpty(DescripcionTextBox.Text))
            {
                if (!string.IsNullOrEmpty(IdTextBox.Text))
                {
                    foreach (var horarios1 in HorariosBLL.GetList(e => e.HorarioId.ToString() == IdTextBox.Text && e.HorarioEliminado == false))
                    {
                        if (!listado.Any(e => e.Equals(horarios1)))
                            listado.Add(horarios1);
                    }
                }
                if (!string.IsNullOrEmpty(DescripcionTextBox.Text))
                {
                    foreach (var horarios in HorariosBLL.GetList(e => e.Descripcion.ToLower().Contains(DescripcionTextBox.Text.ToLower()) && e.HorarioEliminado == false))
                    {
                        if (!listado.Any(e => e.Equals(horarios)))
                            listado.Add(horarios);
                    }
                }
            }
            else
            {
                listado = HorariosBLL.GetList(e => true && e.HorarioEliminado == false);
            }

            TablaTexto.ItemsSource = null;
            TablaTexto.ItemsSource = listado;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            IdTextBox.Text = "";
            DescripcionTextBox.Text = "";
        }

        private void Ver_Click(object sender, RoutedEventArgs e)
        {
            Horarios horarios = (Horarios)TablaTexto.SelectedItem;            
            r_Horarios registroHorarios = new r_Horarios(Convert.ToInt32(horarios.HorarioId));
            registroHorarios.Show();
        }
    }
}
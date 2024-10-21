using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

using System.Media;
using System.Threading;
using NOVAASSIST.BLL;
using NOVAASSIST.Entidades;
using NOVAASSIST.UI.Registros;
using NOVAASSIST.UI.Consulta;
using NOVAASSIST.UI;

namespace NOVAASSIST
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void RegistroAsistencia_Click(object sender, RoutedEventArgs e)
        {
            r_Horarios m = new r_Horarios();
            m.Show();
        }

        private void RegistroEmpleados_Click(object sender, RoutedEventArgs e)
        {
            rEmpleados m = new rEmpleados ();
            m.Show();
        }

        private void RegistroVacaciones_Click(object sender, RoutedEventArgs e)
        {
            r_Vacaciones m = new r_Vacaciones ();
            m.Show();
        }

        private void RegistroExcepciones_Click(object sender, RoutedEventArgs e)
        {
            r_Excepciones m = new r_Excepciones ();
            m.Show();
        }

        private void ConsultaAsistencia_Click(object sender, RoutedEventArgs e)
        {
            c_Asistencia m = new c_Asistencia();
            m.Show();
        }

        private void ConsultaEmpleados_Click(object sender, RoutedEventArgs e)
        {
            c_Empleado m = new c_Empleado();
            m.Show();
        }

        private void ConsultaHorario_Click(object sender, RoutedEventArgs e)
        {
            c_Horarios m = new c_Horarios();
            m.Show();
        }
        private void ConsultaExcepciones_Click(object sender, RoutedEventArgs e)
        {
            c_Excepciones m = new c_Excepciones();
            m.Show();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            r_Asistencia m = new r_Asistencia();
            m.Show();
            this.Close();
        }

        private void ReportesEmpleados_Click(object sender, RoutedEventArgs e)
        {
            HorasTrabajadasR horasTrabajadasWindow = new HorasTrabajadasR();
            horasTrabajadasWindow.ShowDialog();
        }
        private void ReportesHorasMes_Click(object sender, RoutedEventArgs e)
        {
            ReporteHorasMensual horasTrabajadasWindow = new ReporteHorasMensual();
            horasTrabajadasWindow.ShowDialog();
        }
    }
}

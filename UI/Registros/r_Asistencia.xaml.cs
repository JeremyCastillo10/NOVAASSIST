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
    /// <summary>
    /// Interaction logic for r_Asistencia.xaml
    /// </summary>
    public partial class r_Asistencia : Window
    {
        public int contado_bloqueador { set; get; } = 0;

        public int ver { set; get; } = 0;

        public string  m { set; get; } = null;

        public string  p { set; get; } = null;

        public Asistencias asistencias = new Asistencias();

        public Empleados empleados = new Empleados();

        public AsistenciasBLL asistenciasBLL = new AsistenciasBLL();
        public r_Asistencia()
        {
            InitializeComponent();
            EmpezarTiempo(); 
            Pantalla.Focus();
             
            Pantalla.Visibility = Visibility.Collapsed;
        }

        public void EmpezarTiempo()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Tickevent;
            timer.Start();
        }
        public void Tickevent(object sender, EventArgs e)
        {
            Hora.Text = DateTime.Now.ToString(@"hh\:mm\:ss") + "   " + (DateTime.Now.ToString("d/M/yyyy"));  
        }

        private void Button1(object sender, RoutedEventArgs e)
        {
            Sonido();
            Pantalla.Password += "1";
            Pantallausuario.Text += "1";
        }

        private void Button2(object sender, RoutedEventArgs e)
        {
            Sonido();
            Pantalla.Password += "2"; 
            Pantallausuario.Text += "2";
        }

        private void Button3(object sender, RoutedEventArgs e)
        {
            Sonido();
            Pantalla.Password += "3";
            Pantallausuario.Text += "3";
        }

        private void Button4(object sender, RoutedEventArgs e)
        {
            Sonido();
            Pantalla.Password += "4";
            Pantallausuario.Text += "4";
        }
        private void Button5(object sender, RoutedEventArgs e)
        {
            Sonido();
            Pantalla.Password += "5"; 
            Pantallausuario.Text += "5";
        }
        private void Button6(object sender, RoutedEventArgs e)
        {
            Sonido();
            Pantalla.Password += "6";
            Pantallausuario.Text += "6"; 
        }
        private void Button7(object sender, RoutedEventArgs e)
        {
            Sonido();
            Pantalla.Password += "7";
            Pantallausuario.Text += "7"; 
        }
        private void Button8(object sender, RoutedEventArgs e)
        {
            Sonido();
            Pantalla.Password += "8";
            Pantallausuario.Text += "8"; 
        }
        private void Button9(object sender, RoutedEventArgs e)
        {
            Sonido();
            Pantalla.Password += "9";
            Pantallausuario.Text += "9";
        }
        private void Button0(object sender, RoutedEventArgs e)
        {
            Sonido();
            Pantalla.Password += "0";
            Pantallausuario.Text += "0";
        }
        private void ButtonX(object sender, RoutedEventArgs e)
        {
            Eliminar();
        }
        private void ButtonOK(object sender, RoutedEventArgs e)
        {
         
            if (string.IsNullOrEmpty(Pantalla.Password))
            {
                Sonido2();
            }
            else
            {
                if(ver == 0)
                {
                    m = Pantalla.Password;
                    Eliminar();
                    ver += 1;
                  
                    Pantallausuario.Visibility = Visibility.Collapsed;
                    Pantalla.Visibility = Visibility.Visible;
                }
                else
                {
                    p = Pantalla.Password;
                    Eliminar();
                    ver = 0;
                }
               
                if(m != null && p != null)
                {
                    if (EmpleadosBLL.Existe(m, p))
                    {
                        Eliminar();
                        Pantalla.Password = p;

                        insetar_asistencia();
                        Pantallausuario.Visibility = Visibility.Visible;;                       
                        Pantalla.Visibility = Visibility.Collapsed;
                        p = null;
                        m = null;
                        contado_bloqueador = 0;
                    }
                    else
                    {
                        p = null;
                        m = null;
                        contado_bloqueador = contado_bloqueador + 1;
                        Bloqueo(contado_bloqueador);

                        Sonido2();
                        Eliminar();
                        Pantallausuario.Visibility = Visibility.Visible;;                       
                        Pantalla.Visibility = Visibility.Collapsed;
                    }
                }
            }
            asistencias = new Asistencias();
            empleados = new Empleados();
        }
        public void Bloqueo(int contado_bloqueador)
        {
            if (contado_bloqueador == 3)
            {
                botonOK.IsEnabled = false;
            }
        }
        public void Eliminar()
        {
            Pantalla.Clear();
            Pantallausuario.Clear();
        }
        public void Sonido()
        {
            using (var sp = new SoundPlayer(@"Sonido/B.wav"))
            {
                sp.Play();
            }
        }
        public void Sonido2()
        {
            using (var sp = new SoundPlayer(@"Sonido/A.wav"))
            {
                sp.Play();
            }
        }
        public void Sonido3()
        {
            using (var sp = new SoundPlayer(@"Sonido/C.wav"))
            {
                sp.Play();
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Login m = new Login();
            m.Show();
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        public void insetar_asistencia()
        {
            Empleados encontrado = EmpleadosBLL.Buscar(Pantalla.Password);

            if (encontrado != null)
            {
                this.empleados = encontrado;

                if (Pantalla.Password.Length <= 8)
                {
                    Sonido3();
                    
                    if (encontrado.Estado != null)
                    {
                        if (encontrado.Estado == true)
                        {
                            asistencias.Nombre=encontrado.Nombre;
                            asistencias.cedula=encontrado.Cedula;
                            asistencias.Estado = false;
                            empleados.Estado=false;
                            asistencias.Fecha_Salida = DateTime.Now;
                            EmpleadosBLL.Modificar(empleados);
                        }
                        else
                        if (encontrado.Estado == false)
                        {
                            asistencias.Nombre=encontrado.Nombre;
                            asistencias.cedula=encontrado.Cedula;
                            asistencias.Estado = true;
                            empleados.Estado=true;
                            asistencias.Fecha_Entrada= DateTime.Now;
                            EmpleadosBLL.Modificar(empleados);
                        }
                    }
                    else
                    {
                        asistencias.Nombre=encontrado.Nombre;
                        asistencias.cedula=encontrado.Cedula;
                        asistencias.Fecha_Entrada= DateTime.Now;
                        asistencias.Estado = true;
                        empleados.Estado=true;
                        EmpleadosBLL.Modificar(empleados);
                    }
                    asistencias.EmpleadoId = empleados.EmpleadoId;
                    asistencias.HuellaEmpleado = "das";
                  
                    AsistenciasBLL.Insertar(asistencias);
                   
                    Eliminar();
                }
                else
                {
                    MessageBox.Show("Ha sucedido un error, la clave ingresada es incorrecta", "Corrección",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    Sonido2();
                    Eliminar();
                } 
            }
            else
            {
                Pantalla.Password = "bobo"; 
            }
        }
    }
}

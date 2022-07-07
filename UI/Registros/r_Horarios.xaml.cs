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
    /// Interaction logic for r_Horarios.xaml
    /// </summary>
    public partial class r_Horarios : Window
    {
        private Horarios horario = new Horarios();
       

        private string Dias;

        public r_Horarios()
        {
            InitializeComponent();
            this.DataContext = horario;
             obtnerHoras();
        }
        public void obtnerHoras()
        {
            for(int i = 0; i<24; i++){
                if(i<10){
                
                    EntradaCombox.Items.Add("0"+i+":00");
                    SalidaCombox.Items.Add("0"+i+":00");
                }else{
                
                    EntradaCombox.Items.Add(""+i+":00");
                    
                    
                    SalidaCombox.Items.Add(""+i+":00");
                }   
            }

        }
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = this.horario;
        }
        private bool Validar()
        {
           bool valido = true;
           if(string.IsNullOrWhiteSpace(horario.Descripcion) && string.IsNullOrWhiteSpace(horario.Fecha_Entrada)&& string.IsNullOrWhiteSpace(horario.Fecha_Salida)
           &&string.IsNullOrWhiteSpace(horario.Dias))
           {

                valido = false;
                MessageBox.Show("Tiene que llenar todo los campo para poder guardar", "Validacion", MessageBoxButton.OK,MessageBoxImage.Error);
            
           }else{
            if(string.IsNullOrWhiteSpace(horario.Descripcion))
            {
                valido = false;
                DescripcionTextBox.Focus();
                MessageBox.Show("Indique la descripcion del horario", "Validacion", MessageBoxButton.OK,MessageBoxImage.Error);
            }

            if(string.IsNullOrWhiteSpace(horario.Fecha_Entrada))
            {
                valido = false;
                EntradaCombox.Focus();
                MessageBox.Show("Indique las horas de entrada del horario", "Validacion", MessageBoxButton.OK,MessageBoxImage.Error);
            }

            if(string.IsNullOrWhiteSpace(horario.Fecha_Salida))
            {
                valido = false;
                SalidaCombox.Focus();
                MessageBox.Show("Indique las horas de salida del horario", "Validacion", MessageBoxButton.OK,MessageBoxImage.Error);
            }
             if(string.IsNullOrWhiteSpace(horario.Dias))
            {
                valido = false;
                SalidaCombox.Focus();
                MessageBox.Show("Debe Indicar El dia", "Validacion", MessageBoxButton.OK,MessageBoxImage.Error);
            }
           }

            return valido;
        }

        private void Limpiar()
        {
            horario.Descripcion = " ";
            EntradaCombox = null;
            SalidaCombox = null;
            horario.Dias = " ";
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            this.horario=new Horarios();
            this.DataContext= new Horarios();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            horario.Fecha_Entrada=EntradaCombox.Text;
            horario.Fecha_Salida=SalidaCombox.Text;

            bool pasa = false;
                if(!Validar())
                return;

                pasa = HorariosBLL.Guardar(horario);

            if(pasa)
            {
                 MessageBox.Show("Horario guardado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                 Limpiar();
            }
                 
            else
               MessageBox.Show("No se pudo Guardar el Horario", "fallo", MessageBoxButton.OK, MessageBoxImage.Information);
            
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {

              var encontro = HorariosBLL.Buscar(Convert.ToInt32(horario.HorarioId));

            if(encontro != null)
            {
                horario = encontro;

                if(horario.HorarioEliminado == false)
                {
                    horario.HorarioEliminado = true;

                    if(HorariosBLL.Guardar(horario))
                    {
                        MessageBox.Show("Horario eliminado", "Exito",
                            MessageBoxButton.OK);
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("No se fue posible eliminar el horario", "Fallo",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Horario ya esta en estado eliminado", "Exito",
                            MessageBoxButton.OK);

                }
                
            }
            else
            {
                MessageBox.Show("Empleado no existe");
            }

            /*
            if (HorariosBLL.Eliminar(horario.HorarioId))
            {
                Limpiar();
                MessageBox.Show("Horario eliminado con éxito", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No se pudo eliminar el Horario", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);

            */
        }
          private void SelectAll_Checked(object sender, RoutedEventArgs e)
        {
            Option1CheckBox.IsChecked = Option2CheckBox.IsChecked = Option3CheckBox.IsChecked = Option4CheckBox.IsChecked = Option5CheckBox.IsChecked = Option6CheckBox.IsChecked = Option7CheckBox.IsChecked = true;
        }

        private void SelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            Option1CheckBox.IsChecked = Option2CheckBox.IsChecked = Option3CheckBox.IsChecked = Option4CheckBox.IsChecked = Option4CheckBox.IsChecked = Option5CheckBox.IsChecked = Option6CheckBox.IsChecked = Option7CheckBox.IsChecked = false;
        }

        private void SelectAll_Indeterminate(object sender, RoutedEventArgs e)
        {
            // If the SelectAll box is checked (all options are selected), 
            // clicking the box will change it to its indeterminate state.
            // Instead, we want to uncheck all the boxes,
            // so we do this programatically. The indeterminate state should
            // only be set programatically, not by the user.

            if (Option1CheckBox.IsChecked == true &&
                Option2CheckBox.IsChecked == true &&
                Option3CheckBox.IsChecked == true &&
                Option4CheckBox.IsChecked == true &&
                Option5CheckBox.IsChecked == true &&
                Option6CheckBox.IsChecked == true &&
                Option7CheckBox.IsChecked == true )
               
            {
                // This will cause SelectAll_Unchecked to be executed, so
                // we don't need to uncheck the other boxes here.
                 horario.Dias= "Lunes, Martes, Miercoes,Jueves,Viernes,Sabado,Domingo";
                OptionsAllCheckBox.IsChecked = false;
            }else{
 

                if (Option1CheckBox.IsChecked == true)
                 Dias+= " Lunes ";
                horario.Dias =Dias ;
                if (Option2CheckBox.IsChecked == true)
                 Dias+= " Martes ";
                horario.Dias =Dias;
                 if (Option3CheckBox.IsChecked == true)
                horario.Dias += " Miercoles ";
                if (Option4CheckBox.IsChecked == true)
                horario.Dias += " Jueves ";
                if (Option5CheckBox.IsChecked == true)
                horario.Dias += " Viernes ";
                if (Option6CheckBox.IsChecked == true)
                horario.Dias += " Sabado ";
                if (Option7CheckBox.IsChecked == true)
                horario.Dias += " Domingo ";
            }
        }

        private void SetCheckedState()
        {
            // Controls are null the first time this is called, so we just 
            // need to perform a null check on any one of the controls.
            if (Option1CheckBox != null)
            {
                if (Option1CheckBox.IsChecked == true &&
                    Option2CheckBox.IsChecked == true &&
                    Option3CheckBox.IsChecked == true &&
                    Option4CheckBox.IsChecked == true &&
                    Option5CheckBox.IsChecked == true &&
                    Option6CheckBox.IsChecked == true &&
                    Option7CheckBox.IsChecked == true)
                {
                    OptionsAllCheckBox.IsChecked = true;
                }
                else if (Option1CheckBox.IsChecked == false &&
                    Option2CheckBox.IsChecked == false &&
                    Option3CheckBox.IsChecked == false &&
                    Option4CheckBox.IsChecked == false &&
                    Option5CheckBox.IsChecked == false &&
                    Option6CheckBox.IsChecked == false &&
                    Option7CheckBox.IsChecked == false)
                {
                    OptionsAllCheckBox.IsChecked = false;
                }
                else
                {
                    // Set third state (indeterminate) by setting IsChecked to null.
                    OptionsAllCheckBox.IsChecked = null;
                }
            }
        }

        private void Option_Checked(object sender, RoutedEventArgs e)
        {
            SetCheckedState();
        }

        private void Option_Unchecked(object sender, RoutedEventArgs e)
        {
            SetCheckedState();
        }

     
    }
}
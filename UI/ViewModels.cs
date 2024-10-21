using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using NOVAASSIST.BLL;
using NOVAASSIST.Entidades;

public class HorasTrabajadasViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Asistencias> HorasTrabajadas { get; set; } = new ObservableCollection<Asistencias>();
    public ObservableCollection<string> TipoConsultaOptions { get; set; } = new ObservableCollection<string> { "Diario", "Semanal", "Mensual" };
    public ObservableCollection<Empleados> EmpleadosList { get; set; } = new ObservableCollection<Empleados>();
    public Empleados SelectedEmpleado { get; set; }
    public DateTime FechaSeleccionada { get; set; } = DateTime.Today;
    public string TipoConsulta { get; set; }

    private double totalHorasTrabajadas;
    public double TotalHorasTrabajadas
    {
        get => totalHorasTrabajadas;
        set
        {
            totalHorasTrabajadas = value;
            OnPropertyChanged(nameof(TotalHorasTrabajadas));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public HorasTrabajadasViewModel()
    {
        LoadEmpleados(); // Cargar empleados en el constructor
    }

    private void LoadEmpleados()
    {
        var empleados = EmpleadosBLL.GetEmpleados(); // Método para obtener la lista de empleados
        foreach (var empleado in empleados)
        {
            EmpleadosList.Add(empleado);
        }
    }

    public void BuscarHorasTrabajadas()
    {
        try
        {
            if (SelectedEmpleado == null)
            {
                MessageBox.Show("Por favor, seleccione un empleado.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime fechaInicio, fechaFin;

            switch (TipoConsulta)
            {
                case "Diario":
                    fechaInicio = FechaSeleccionada.Date;
                    fechaFin = fechaInicio.AddDays(1);
                    break;

                case "Semanal":
                    fechaInicio = FechaSeleccionada.StartOfWeek();
                    fechaFin = fechaInicio.AddDays(7);
                    break;

                case "Mensual":
                    fechaInicio = new DateTime(FechaSeleccionada.Year, FechaSeleccionada.Month, 1);
                    fechaFin = fechaInicio.AddMonths(1);
                    break;

                default:
                    MessageBox.Show("Tipo de consulta no válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
            }

            var asistencias = AsistenciasBLL.GetAsistencias(SelectedEmpleado.EmpleadoId, fechaInicio, fechaFin);
            HorasTrabajadas.Clear();

            foreach (var asistencia in asistencias)
            {
                HorasTrabajadas.Add(asistencia);
            }

            // Calcular total de horas trabajadas
            TotalHorasTrabajadas = HorasTrabajadas.Sum(a => a.HorasTrabajadas); // Asegúrate de que HorasTrabajadas sea de tipo double

            MessageBox.Show($"Se encontraron {HorasTrabajadas.Count} registros.", "Resultado", MessageBoxButton.OK);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al buscar horas trabajadas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

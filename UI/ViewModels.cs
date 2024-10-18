using NOVAASSIST.BLL;
using NOVAASSIST.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOVAASSIST.UI
{
    public class HorasTrabajadasViewModel : INotifyPropertyChanged
    {
        private List<Empleados> _empleadosList;
        private Empleados _selectedEmpleado;

        public List<Empleados> EmpleadosList
        {
            get { return _empleadosList; }
            set { _empleadosList = value; OnPropertyChanged(nameof(EmpleadosList)); }
        }

        public Empleados SelectedEmpleado
        {
            get { return _selectedEmpleado; }
            set { _selectedEmpleado = value; OnPropertyChanged(nameof(SelectedEmpleado)); }
        }

        public HorasTrabajadasViewModel()
        {
            EmpleadosList = EmpleadosBLL.GetEmpleados();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

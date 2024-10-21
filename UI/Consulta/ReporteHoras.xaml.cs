using System.Windows;

namespace NOVAASSIST.UI
{
    public partial class HorasTrabajadasR : Window
    {
        private HorasTrabajadasViewModel viewModel;

        public HorasTrabajadasR()
        {
            InitializeComponent();
            viewModel = new HorasTrabajadasViewModel();
            DataContext = viewModel; // Asegúrate de que esto esté presente
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.BuscarHorasTrabajadas();
        }
    }
}

using MVVMWindowExample.ViewModels;
using System.Windows;

namespace MVVMWindowExample.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new MainWindowViewModel();
            this.DataContext = vm;
        }
    }
}

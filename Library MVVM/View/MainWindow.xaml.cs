using Library_MVVM.ViewModel;
using System.Windows;

namespace Library_MVVM
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel();
        }
    }
}

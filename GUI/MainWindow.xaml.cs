using System.Windows;

using ViewModel;
using Serializers;
using Microsoft.Win32;

namespace GUI
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new VMViewModel(new GUIFileSelector(), new XmlSerializer());
        }
    }
}

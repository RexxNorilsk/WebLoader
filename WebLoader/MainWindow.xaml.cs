using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Linq;

namespace WebLoader
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<ILoadLink> _loadLinks = new ObservableCollection<ILoadLink>();
        private Loader _currentLoader;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            _loadLinks.Add(new LoadLinkForm("",0));
            listLoads.ItemsSource = _loadLinks;
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            if(listLoads.Items.Count > 0)
                _loadLinks.RemoveAt(listLoads.Items.Count-1);
        }

        private void buttonLoad_Click(object sender, RoutedEventArgs e)
        {
            if(_currentLoader == null)
                _currentLoader = new Loader(_loadLinks, (t) => MessageBox.Show(t));
            else if(_currentLoader.IsFinal())
                _currentLoader = new Loader(_loadLinks, (t) => MessageBox.Show(t));
        }
    }
}

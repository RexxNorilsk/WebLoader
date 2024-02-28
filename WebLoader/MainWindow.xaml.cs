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

namespace WebLoader
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<LoadLink> LoadLinks = new ObservableCollection<LoadLink>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            LoadLinks.Add(new LoadLink("",0));
            listLoads.ItemsSource = LoadLinks;
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            if(listLoads.Items.Count > 0)
                LoadLinks.RemoveAt(listLoads.Items.Count-1);
        }

        private void buttonLoad_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

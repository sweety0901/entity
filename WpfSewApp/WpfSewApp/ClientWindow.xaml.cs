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
using System.Windows.Shapes;

namespace WpfSewApp
{
    /// <summary>
    /// Логика взаимодействия для СlientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
        }
        
        private void BExit_Click(object sender, RoutedEventArgs e)
        {
            Windows.ShowWindow(this, new MainWindow());
        }

        private void BTextile_Click(object sender, RoutedEventArgs e)
        {
            Windows.ShowWindow(this, new TextileListWindow());
        }
    }
}

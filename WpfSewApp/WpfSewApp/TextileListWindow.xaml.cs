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
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfSewApp
{
    /// <summary>
    /// Логика взаимодействия для TextileListWindow.xaml
    /// </summary>
    public partial class TextileListWindow : Window
    {
        public TextileListWindow()
        {
            InitializeComponent();
        }               

        private void DataGridOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dynamic a = grid.SelectedItem;
                Windows.ShowWindow(this, new TextileEditWindow(Convert.ToInt32(a.ID)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            grid.ItemsSource = Entity.Entities.Table.ToList();
            grid.Columns[3].Visibility = Visibility.Collapsed;

        }

        private void BNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Windows.ShowWindow(this, new TextileEditWindow(Convert.ToInt32(0)));
            }
            catch (Exception)
            {
                MessageBox.Show("Не выбрана запись!");
            }
        }

        private void BPrint_Click(object sender, RoutedEventArgs e)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\soft\Desktop\1.csv"))
            {
                foreach (var item in Entity.Entities.Table.ToList())
                {
                    file.WriteLine(item.ID + ";" + item.name + ";" + item.cost + ";");
                }
            }
        }
    }
}

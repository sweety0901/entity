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
using System.IO;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace WpfSewApp
{
    /// <summary>
    /// Логика взаимодействия для TextileEditWindow.xaml
    /// </summary>
    public partial class TextileEditWindow : Window
    {
        int item;
        byte[] buffer;

        public TextileEditWindow(int item)
        {
            this.item = item;
            InitializeComponent();

            if (item != 0)
            {
                Table table = Entity.Entities.Table.Where(i => i.ID == item).SingleOrDefault();
                ID.Text = table.ID.ToString();
                name.Text = table.name;
                cost.Text = table.cost.ToString();

                try
                {
                    image.Source = GetBitmapSource(table.image);
                }
                catch (Exception)
                {
                    image.Source = null;
                }
            }

            SizeToContent = SizeToContent.WidthAndHeight;

            ID.IsReadOnly = true;
        }
              
        
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (item == 0)
                {
                    Entity.Entities.Table.Add(new Table()
                    {
                        name = name.Text,
                        cost = Convert.ToInt32(cost.Text),
                        image = buffer                        
                    });                    
                }
                else
                {
                    Table table = Entity.Entities.Table.Where(i => i.ID == item).FirstOrDefault();
                    table.name = name.Text;
                    table.cost = Convert.ToInt32(cost.Text);
                    table.image = buffer;                    
                }

                Entity.Entities.SaveChanges();

                Windows.ShowWindow(this, new TextileListWindow());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Windows.ShowWindow(this, new TextileListWindow());
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            if(fileDialog.ShowDialog()==true)
            {
                FileStream fileStream = new FileStream(fileDialog.FileName, FileMode.Open, FileAccess.Read);
                buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, (int)(fileStream.Length));
                fileStream.Close();

                image.Source = GetBitmapSource(buffer);
            }
        }

        private static BitmapSource GetBitmapSource(byte[] buf)
        {
            if (buf != null)
            {
                BitmapImage bitmap = new BitmapImage();
                var stream = new MemoryStream(buf);
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.EndInit();
                return bitmap as BitmapSource;
            }
            else
            {
                return null;
            }
        }
    }
}

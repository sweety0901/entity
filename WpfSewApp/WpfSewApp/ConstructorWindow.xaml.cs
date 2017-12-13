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
    /// Логика взаимодействия для ConstructorWindow.xaml
    /// </summary>
    public partial class ConstructorWindow : Window
    {
        public ConstructorWindow()
        {
            InitializeComponent();

            canvas.Height = 500;
            canvas.Width = 500;
            SizeToContent = SizeToContent.WidthAndHeight;
        }

        Rectangle thisRec;

        int Reci = 0;   
        bool drop = false;
        bool resize = false;

        private void Rec_Down(object sender, RoutedEventArgs e)
        {
            try
            {
                thisRec.Stroke = null;
            }
            catch (Exception)
            {

            }
            finally
            {
                thisRec = (Rectangle)(sender);
                thisRec.Stroke = new SolidColorBrush(Colors.Black);
                drop = true;
            }
        }
        

        private void Rec_MouseMove(object sender, MouseEventArgs e)
        {
            if (drop)
            {
                double centrex = thisRec.Width / 2;
                double centrey = thisRec.Height / 2;

                double cx = e.GetPosition(canvas).X;
                double cy = e.GetPosition(canvas).Y;
                
                thisRec.Margin = new Thickness((cx- centrex), (cy-centrey), 0, 0);
            }

            


            label1.Content = e.GetPosition(canvas).X + "*" + e.GetPosition(canvas).Y;
            label2.Content = e.GetPosition(thisRec).X + "*" + e.GetPosition(thisRec).Y;
        }

        private void Rec_Up(object sender, RoutedEventArgs e)
        {
            drop = false;
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string filename="";
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".jpg"
            };

            if (dialog.ShowDialog() == true)
            {
                filename = dialog.FileName;
            }
            Rectangle rectangle = new Rectangle()
            {
                Width = 150,
                Height = 150,
                Margin = new Thickness(10, 10, 0, 0),
                Uid = "rec" + (++Reci),
                RadiusX = 10,
                Fill = new ImageBrush(new BitmapImage(new Uri(filename))),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            
            rectangle.MouseDown += Rec_Down;
            rectangle.MouseUp += Rec_Up;
            rectangle.MouseMove += Rec_MouseMove;
            canvas.Children.Add(rectangle);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Без названия.png";
            if (dialog.ShowDialog() == true)
            {
                RenderTargetBitmap render = new RenderTargetBitmap((int)(canvas.Width), (int)(canvas.Height), 96, 96, PixelFormats.Pbgra32);
                render.Render(canvas);

                using (var file = System.IO.File.Open(dialog.FileName, System.IO.FileMode.OpenOrCreate))
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(render));
                    encoder.Save(file);
                }
            }
        }
        
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            resize = false;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                resize = true;
            }

            if(e.Key==Key.D && e.Key == Key.LeftCtrl)
            {
                thisRec.Stroke = null;
            }
        }

        private void canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {
                if (resize)
                {
                    thisRec.Width = thisRec.Width += (e.Delta / 12);
                    thisRec.Height = thisRec.Height += (e.Delta / 12);
                }
                else
                {
                    Rectangle image = canvas.Children.OfType<Rectangle>().FirstOrDefault(x => x.Uid == thisRec.Uid);
                    RotateTransform rotate = thisRec.RenderTransform as RotateTransform;
                    try
                    {
                        image.RenderTransform = new RotateTransform((int)(rotate.Angle += (e.Delta / 12)), image.Width / 2, image.Height / 2);
                    }
                    catch (Exception)
                    {
                        image.RenderTransform = new RotateTransform((e.Delta / 12), image.Width / 2, image.Height / 2);
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}




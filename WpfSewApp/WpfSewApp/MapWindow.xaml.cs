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
    /// Логика взаимодействия для MapWindow.xaml
    /// </summary>
    public partial class MapWindow : Window
    {
        double
            minX, // стартовая точка х
            minY, // стартовая точка y

            maxX, // ?
            maxY, // ?

            thisX, // текущая точка х
            thisY; // текущая точка У

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RenderTargetBitmap render = new RenderTargetBitmap(100, 100, 96d, 96d, PixelFormats.Pbgra32);
            render.Render(canvas);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(render));

            byte[] buf;
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                encoder.Save(stream);
                buf = stream.ToArray();
                stream.Close();
            }

            Entity.Entities.Table.Add(new Table
            {
                name = "A",
                cost = 3,
                image = buf
            });
            Entity.Entities.SaveChanges();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFile = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "Без названия.png"
            };

            if (saveFile.ShowDialog() == true)
            {
                MessageBox.Show((canvas.Width).ToString() + (canvas.Height).ToString());

                RenderTargetBitmap render = new RenderTargetBitmap(pixelWidth: (int)(canvas.RenderSize.Width), pixelHeight: (int)(canvas.RenderSize.Width), dpiX: 96d, dpiY: 96d, pixelFormat: PixelFormats.Pbgra32);
                render.Render(canvas);

                using (var filename = System.IO.File.Open(saveFile.FileName, System.IO.FileMode.OpenOrCreate))
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(render));
                    encoder.Save(filename);
                }
            }
        }

        public MapWindow()
        {
            InitializeComponent();

            string thisitem = "ПЦ-001.745.14";
            string order = "29";

            try
            {
                var fabric = from p in Entity.Entities.Изделия_
                             join pf in Entity.Entities.ТканиИзделия on p.Артикул equals pf.Изделиe
                             join f in Entity.Entities.СкладТкани on pf.Ткань equals f.Ткань
                             where p.Артикул == thisitem.ToString()
                             select new
                             {
                                 x = (double)f.Ширина,
                                 y = (double)f.Длина
                             };

                LPrice.Content = fabric.FirstOrDefault().x + " на " + fabric.FirstOrDefault().y;

                canvas.Height = fabric.FirstOrDefault().x;
                canvas.Width = fabric.FirstOrDefault().y;

                SizeToContent = SizeToContent.WidthAndHeight;

                //стартвая позиция в ноль по canvas              
                minX = 0;
                minY = 0;

                //текущая точка тоже в ноль
                thisX = minX;
                thisY = minY;

                // предельная точка
                maxY = canvas.Height;
                maxX = canvas.Width;

                bool newcolumnflag = true;

                var product = from p in Entity.Entities.Изделия_
                              join po in Entity.Entities.ЗаказанныеИзделия on p.Артикул equals po.Изделие
                              where p.Артикул == thisitem.ToString() && po.Заказ.ToString() == order
                              orderby p.Ширина, p.Длина
                              select new
                              {
                                  x = p.Ширина,
                                  y = p.Длина,
                                  count = po.Количество
                              };
                int newcolumncount = 0;
                foreach (var item in product.ToList())
                {
                    for (int i = 0; i < item.count + 9; i++)
                    {
                        // распределение наибольшего У, и наименьшего X
                        double y = Convert.ToDouble(item.x) > Convert.ToDouble(item.y) ? Convert.ToDouble(item.x) : Convert.ToDouble(item.y);
                        double x = Convert.ToDouble(item.x) < Convert.ToDouble(item.y) ? Convert.ToDouble(item.x) : Convert.ToDouble(item.y);


                        //условие на новую колонку, если новый элемент не входит в столбец по Y, то перенос на новый
                        if (thisY + y > maxY && (thisX + x) <= maxX)
                        {
                            thisY = minY;
                            thisX += x;
                            newcolumnflag = false;
                        }
                        //условие на максимум изделий
                        if (thisY + y > maxY || (thisX + x) > maxX) return;

                        //новый прямоугольник с заданными размерами и отступом по текущей точке (х,у)
                        Rectangle rectangle = new Rectangle()
                        {
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                            Uid = "rec" + i.ToString(),
                            Fill = new SolidColorBrush(Color.FromRgb(224, 169, 175)),
                            Stroke = new SolidColorBrush(Colors.Black),
                            Margin = new Thickness(thisX, thisY, 0, 0),
                            Height = y,
                            Width = x
                        };
                        canvas.Children.Add(element: rectangle);

                        //условие на отступ в колонке, если помещается - помещаем, нет - выходим.
                        if (thisY + y <= maxY && thisX + x <= maxX )
                        {
                            thisY += y;
                            if (newcolumnflag) { newcolumncount++; }
                            
                        }
                        else
                        {
                            return;
                        }

                        LPrice.Content = "Размер раскроя: " + (thisX + x) + "x" + (thisY) + ". Списано ткани: " + (maxY-newcolumncount*y) + "x" + (thisX + x) + ". Доступно для раскроя: " + (i + 1) + " изделий.";
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}

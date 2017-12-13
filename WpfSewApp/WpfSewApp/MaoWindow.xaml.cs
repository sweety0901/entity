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
    /// Логика взаимодействия для MaoWindow.xaml
    /// </summary>
    public partial class MaoWindow : Window
    {

        double
            minX, // стартовая точка х
            minY, // стартовая точка y

            maxX, // ?
            maxY, // ?

            thisX, // текущая точка х
            thisY; // текущая точка У
        public MaoWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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

                grid.Height = (bord.Height = fabric.FirstOrDefault().x) + bord.Margin.Top + bord.Margin.Bottom;
                grid.Width = (bord.Width = fabric.FirstOrDefault().y) + bord.Margin.Left + bord.Margin.Right;

                SizeToContent = SizeToContent.WidthAndHeight;

                minX = bord.Margin.Left;
                minY = bord.Margin.Top;

                // ?
                maxX = grid.Width;
                maxY = grid.Height;

                //текущая точка тоже в ноль
                thisX = minX;
                thisY = minY;
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

                //grid.Children.Add(element: new Rectangle()
                //{
                //    HorizontalAlignment = HorizontalAlignment.Left,
                //    VerticalAlignment = VerticalAlignment.Top,
                //    Uid = "rec0",
                //    Fill = new SolidColorBrush(Color.FromRgb((byte)((new Random()).Next(0, 255)), (byte)((new Random()).Next(0, 255)), (byte)((new Random()).Next(0, 255)))),
                //    Stroke = new SolidColorBrush(Colors.Black),
                //    Margin = new Thickness(thisX, thisY, 0, 0),
                //    Height = Convert.ToDouble(product.FirstOrDefault().x),
                //    Width = Convert.ToDouble(product.FirstOrDefault().y)
                //});

                foreach (var item in product.ToList())
                {
                    for (int i = 0; i < item.count+1; i++)
                    {
                        double y = Convert.ToDouble(item.x) > Convert.ToDouble(item.y) ? Convert.ToDouble(item.x) : Convert.ToDouble(item.y);
                        double x = Convert.ToDouble(item.x) < Convert.ToDouble(item.y) ? Convert.ToDouble(item.x) : Convert.ToDouble(item.y);

                        if (thisY + x <= maxY && thisY + y > maxY)
                        {
                            double b = x;
                            x = y;
                            y = b;
                        }                       

                       
                        

                        Rectangle rectangle = new Rectangle()
                        {
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                            Uid = "rec" + i.ToString(),
                            Fill = new SolidColorBrush(Color.FromRgb((byte)((new Random()).Next(0, 255)), (byte)((new Random()).Next(0, 255)), (byte)((new Random()).Next(0, 255)))),
                            Stroke = new SolidColorBrush(Colors.Black),
                            Margin = new Thickness(thisX, thisY, 0, 0),
                            Height = y,
                            Width = x
                        };
                        grid.Children.Add(element: rectangle);
                        {
                            if (maxY - thisY >=  y)
                            {
                                thisY += y;
                            }
                            else
                            {
                                if ( maxY - thisY >=x)
                                {
                                    thisY += x;
                                }
                                else
                                {
                                    thisY = minY;
                                    thisX += x;
                                }
                            }
                        }
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


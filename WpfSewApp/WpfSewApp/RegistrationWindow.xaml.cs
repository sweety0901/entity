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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void BRegistration_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TLogin.Text == "" || TPassword.Text == "" || TName.Text == "" || TName.Text == "")
                {
                    throw new Exception("не все поля заполнены!");
                }

                if(TPassword.Text != TPasswordRep.Text)
                {
                    throw new Exception("пароль не потдвержден!");
                }

                MessageBox.Show(TLogin.Text);

                Пользователь пользователь = new Пользователь()
                {
                    Логин = TLogin.Text,
                    Пароль = TPassword.Text,
                    Роль = "Заказчик",
                    Наименование = TName.Text
                };

                Entity.Entities.Пользователь.Add(пользователь);
                
                Windows.ShowWindow(this, new MainWindow());
            }
            catch(Exception ex)
            {
                MessageBox.Show("Не удалось зарегистрировать пользователя: " + ex.Message );
            }
            finally
            {
                Entity.Entities.SaveChanges();
            }

            
        }
    }
}

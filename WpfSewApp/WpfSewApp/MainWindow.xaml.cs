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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfSewApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BRegistration_Click(object sender, RoutedEventArgs e) => Windows.ShowWindow(this, new RegistrationWindow());

        private void BLogin_Click(object sender, RoutedEventArgs e)
        {
            var user = Entity.Entities.Пользователь.Where(u => u.Логин == TLogin.Text && u.Пароль == TPassword.Text).First();

            if (user != null)
            {
                Entity.User = user;
                switch (Entity.User.Роль)
                {
                    case "Заказчик":
                        Windows.ShowWindow(this, new ClientWindow());
                        break;
                    case "Кладовщик":
                        MessageBox.Show("");
                        break;
                    case "Менеджер":
                        Windows.ShowWindow(this, new ManagerWindow());
                        break;
                    case "Руководитель":
                        MessageBox.Show("");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace marah_practic
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private static string connStr = "Host=Localhost;Username=postgres;Password=1204;Database=wss";

        private static NpgsqlConnection conn = new NpgsqlConnection(connStr);

        User user1 = new User();

        public Login()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
            InitializeComponent();
            DebugPage.Visibility = Visibility.Hidden;
        }

        class User
        {
            public int id;
            public string email;
            public string password;
            public string role;
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Left = this.Left;
            mainWindow.Top = this.Top;
            mainWindow.Show();
            this.Close();
        }

        private void cancel_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Left = this.Left;
            mainWindow.Top = this.Top;
            mainWindow.Show();
            this.Close();
        }

        private void login_button_Click(object sender, RoutedEventArgs e)
        {

            var cmd = new NpgsqlCommand($"select u.id, email, password, r.name from users u " +
                $"join roles r " +
                $"on r.id = u.role_id " +
                $"where u.email = '{user_email.Text}'", conn);
            var reader = cmd.ExecuteReader();
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user1.id = reader.GetInt32(0);
                        user1.email = reader.GetString(1);
                        user1.password = reader.GetString(2);
                        user1.role = reader.GetString(3);
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка ввода email", "Ошибка");
                }
            }
            reader.Close();

            if (user_password.Text == user1.password)
            {
                if (user1.role == "Runner")
                {
                    menurunner menurunner = new menurunner(user1.id);
                    menurunner.Left = this.Left;
                    menurunner.Top = this.Top;
                    menurunner.Show();
                    this.Close();
                }
                else if (user1.role == "Coordinator")
                {
                    menukoordinator menukoordinator = new menukoordinator(user1.id);
                    menukoordinator.Left = this.Left;
                    menukoordinator.Top = this.Top;
                    menukoordinator.Show();
                    this.Close();
                }
                else if (user1.role == "Administrator")
                {
                    adminmenu adminmenu = new adminmenu();
                    adminmenu.Left = this.Left;
                    adminmenu.Top = this.Top;
                    adminmenu.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Ошибка ввода password", "Ошибка");
            }
        }

        private void runner_button_Click(object sender, RoutedEventArgs e)
        {
            menurunner menurunner = new menurunner(user1.id);
            menurunner.Top = this.Top;
            menurunner.Left = this.Left;
            menurunner.Show();
            this.Close();
        }

        private void coordinator_button_Click(object sender, RoutedEventArgs e)
        {
            menukoordinator menukoordinator = new menukoordinator(user1.id);
            menukoordinator.Top = this.Top;
            menukoordinator.Left = this.Left;  
            menukoordinator.Show();
            this.Close();
        }

        private void admin_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void closeDebug_button_Click(object sender, RoutedEventArgs e)
        {
            DebugPage.Visibility = Visibility.Hidden;
        }
    }
}

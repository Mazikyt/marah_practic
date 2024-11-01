using System.Data;
using System.Windows;
using Npgsql;

namespace marah_practic;

/// <summary>
///     Логика взаимодействия для Login.xaml
/// </summary>
public partial class Login : Window
{
    private static readonly string connStr = "Host=84.21.173.156:5435;Username=postgres;Password=DBDfJN3Vz9;Database=wss";

    private static readonly NpgsqlConnection conn = new(connStr);

    private readonly User user1 = new();

    public Login()
    {
        if (conn.State == ConnectionState.Closed) conn.Open();
        InitializeComponent();
        DebugPage.Visibility = Visibility.Hidden;
    }

    private void back_button_Click(object sender, RoutedEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Left = Left;
        mainWindow.Top = Top;
        mainWindow.Show();
        Close();
    }

    private void cancel_button_Click(object sender, RoutedEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Left = Left;
        mainWindow.Top = Top;
        mainWindow.Show();
        Close();
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
                while (reader.Read())
                {
                    user1.id = reader.GetInt32(0);
                    user1.email = reader.GetString(1);
                    user1.password = reader.GetString(2);
                    user1.role = reader.GetString(3);
                }
            else
                MessageBox.Show("Ошибка ввода email", "Ошибка");
        }
        reader.Close();

        if (user_password.Text == user1.password)
        {
            if (user1.role == "Runner")
            {
                var menurunner = new menurunner(user1.id);
                menurunner.Left = Left;
                menurunner.Top = Top;
                menurunner.Show();
                Close();
            }
            else if (user1.role == "Coordinator")
            {
                var menukoordinator = new menukoordinator(user1.id);
                menukoordinator.Left = Left;
                menukoordinator.Top = Top;
                menukoordinator.Show();
                Close();
            }
            else if (user1.role == "Administrator")
            {
                var adminmenu = new adminmenu();
                adminmenu.Left = Left;
                adminmenu.Top = Top;
                adminmenu.Show();
                Close();
            }
        }
        else
        {
            MessageBox.Show("Ошибка ввода password", "Ошибка");
        }
    }

    private void runner_button_Click(object sender, RoutedEventArgs e)
    {
        var menurunner = new menurunner(user1.id);
        menurunner.Top = Top;
        menurunner.Left = Left;
        menurunner.Show();
        Close();
    }

    private void coordinator_button_Click(object sender, RoutedEventArgs e)
    {
        var menukoordinator = new menukoordinator(user1.id);
        menukoordinator.Top = Top;
        menukoordinator.Left = Left;
        menukoordinator.Show();
        Close();
    }

    private void admin_button_Click(object sender, RoutedEventArgs e)
    {
    }

    private void closeDebug_button_Click(object sender, RoutedEventArgs e)
    {
        DebugPage.Visibility = Visibility.Hidden;
    }

    private class User
    {
        public string email;
        public int id;
        public string password;
        public string role;
    }
}
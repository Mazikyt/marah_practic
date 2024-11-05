using System.Data;
using System.Windows;
using Npgsql;

namespace marah_practic;

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
    // Проверка на пустые поля
    if (string.IsNullOrWhiteSpace(user_email.Text) || string.IsNullOrWhiteSpace(user_password.Text))
    {
        MessageBox.Show("Пожалуйста, заполните все поля", "Ошибка");
        return;
    }
    
    var cmd = new NpgsqlCommand("SELECT u.id, email, password, r.name FROM users u " +
                                "JOIN roles r ON r.id = u.role_id " +
                                "WHERE u.email = @Email", conn);
    cmd.Parameters.AddWithValue("Email", user_email.Text);
    try {
        var reader = cmd.ExecuteReader();
        if (reader.HasRows) {
            while (reader.Read()) {
                user1.id = reader.GetInt32(0);
                user1.email = reader.GetString(1);
                user1.password = reader.GetString(2);
                user1.role = reader.GetString(3);
            }
            reader.Close();
            if (user_password.Text == user1.password) {
                OpenUserMenu(user1.role);
            }
            else {
                MessageBox.Show("Неверный пароль", "Ошибка");
            }
        }
        else {
            MessageBox.Show("Пользователь с таким email не найден", "Ошибка");
        }
    }
    catch (Exception ex) {
        MessageBox.Show($"Ошибка при подключении к базе данных: {ex.Message}", "Ошибка");
    }
    finally {
        cmd.Dispose();
    }
}

private void OpenUserMenu(string role)
{
    Window menuWindow;
    switch (role)
    {
        case "Runner":
            menuWindow = new menurunner(user1.id);
            break;
        case "Coordinator":
            menuWindow = new menukoordinator(user1.id);
            break;
        case "Administrator":
            menuWindow = new adminmenu();
            break;
        default:
            MessageBox.Show("Неизвестная роль пользователя", "Ошибка");
            return;
    }
    
    menuWindow.Left = Left;
    menuWindow.Top = Top;
    menuWindow.Show();
    Close();
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
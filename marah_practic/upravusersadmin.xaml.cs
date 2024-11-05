using System.Data;
using System.Windows;
using Npgsql;

namespace marah_practic;

public partial class upravusersadmin : Window
{
    private static readonly string connStr = "Host=84.21.173.156:5435;Username=postgres;Password=DBDfJN3Vz9;Database=wss";

    private static readonly NpgsqlConnection conn = new(connStr);

    public upravusersadmin()
    {
        if (conn.State == ConnectionState.Closed) conn.Open();
        InitializeComponent();

        var refresh_comboBox = new List<string>();
        refresh_comboBox.Add("Фамилия");
        refresh_comboBox.Add("Имя");
        refresh_comboBox.Add("Email");
        refresh_comboBox.Add("Роль");

        sort_comboBox.ItemsSource = refresh_comboBox;

        var roleRefresh_comboBox = new List<string>();
        roleRefresh_comboBox.Add("Админ");
        roleRefresh_comboBox.Add("Координатор");
        roleRefresh_comboBox.Add("Бегун");
        roleRefresh_comboBox.Add("Сотрудники");
        roleRefresh_comboBox.Add("Волонтер");

        role_comboBox.ItemsSource = roleRefresh_comboBox;
    }

    private void back_button_Click(object sender, RoutedEventArgs e)
    {
        var adminmenu = new adminmenu();
        adminmenu.Left = Left;
        adminmenu.Top = Top;
        adminmenu.Show();
        Close();
    }

    private void logout_button_Click(object sender, RoutedEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Left = Left;
        mainWindow.Top = Top;
        mainWindow.Show();
        Close();
    }

    private void refresh_button_Click(object sender, RoutedEventArgs e)
    {
        var usersCount = 0;

        var select_string = @"select u.last_name, u.first_name, COALESCE(u.email, '-'), r.name from users u
                                      join roles r ON r.id = u.role_id ";

        if (role_comboBox.Text != "")
        {
            if (role_comboBox.Text == "Админ")
                select_string = select_string + "where u.role_id = 1 ";
            else if (role_comboBox.Text == "Координатор")
                select_string = select_string + "where u.role_id = 2 ";
            else if (role_comboBox.Text == "Бегун")
                select_string = select_string + "where u.role_id = 3 ";
            else if (role_comboBox.Text == "Сотрудник")
                select_string = select_string + "where u.role_id = 4 ";
            else if (role_comboBox.Text == "Волонтер") select_string = select_string + "where u.role_id = 5 ";
        }

        if (sort_comboBox.Text != "")
        {
            if (sort_comboBox.Text == "Фамилия")
                select_string = select_string + "order by u.last_name";
            else if (sort_comboBox.Text == "Имя")
                select_string = select_string + "order by u.first_name";
            else if (sort_comboBox.Text == "Email")
                select_string = select_string + "order by 3";
            else if (sort_comboBox.Text == "Роль") select_string = select_string + "order by 4";
        }

        var users = new List<User>();

        var cmd = new NpgsqlCommand(select_string, conn);
        var reader = cmd.ExecuteReader();
        if (reader.HasRows)
            while (reader.Read())
            {
                users.Add(new User(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                usersCount++;
            }

        reader.Close();

        usersCount_text.Text = usersCount.ToString();

        var ListView = users.Select(x => new
        {
            x.first_name,
            x.last_name,
            x.email,
            x.role
        }).ToList();

        users_ListView.ItemsSource = ListView;
    }

    private void addUser_button_Click(object sender, RoutedEventArgs e)
    {
        var insertnewuser = new insertnewuser();
        insertnewuser.Left = Left;
        insertnewuser.Top = Top;
        insertnewuser.Show();
        Close();
    }

    private class User
    {
        public readonly string email;
        public readonly string first_name;
        public readonly string last_name;
        public readonly string role;

        public User(string last_name, string first_name, string email, string role)
        {
            this.last_name = last_name;
            this.first_name = first_name;
            this.email = email;
            this.role = role;
        }
    }
}
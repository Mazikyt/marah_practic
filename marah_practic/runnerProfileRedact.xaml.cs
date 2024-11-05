using System.Data;
using System.Globalization;
using System.Windows;
using Npgsql;

namespace marah_practic;

public partial class runnerProfileRedact : Window
{
    private static int cur_user_id;

    private static readonly string connStr = "Host=84.21.173.156:5435;Username=postgres;Password=DBDfJN3Vz9;Database=wss";

    private static readonly NpgsqlConnection conn = new(connStr);

    private readonly User user1 = new();

    public runnerProfileRedact(int user_id)
    {
        if (conn.State == ConnectionState.Closed) conn.Open();
        InitializeComponent();
        cur_user_id = user_id;

        var countriesListName = new List<string>();
        var countriesListId = new List<int>();

        var cmd = new NpgsqlCommand("select * from countries", conn);
        var reader = cmd.ExecuteReader();
        if (reader.HasRows)
            while (reader.Read())
            {
                countriesListId.Add(reader.GetInt32(0));
                countriesListName.Add(reader.GetString(1));
            }

        reader.Close();

        countryComboBox.ItemsSource = countriesListName;

        var gendersListName = new List<string>();
        var gendersListId = new List<int>();

        cmd = new NpgsqlCommand("select * from genders", conn);
        reader = cmd.ExecuteReader();
        if (reader.HasRows)
            while (reader.Read())
            {
                gendersListId.Add(reader.GetInt32(0));
                gendersListName.Add(reader.GetString(1));
            }

        reader.Close();
        genderComboBox.ItemsSource = gendersListName;

        cmd = new NpgsqlCommand(
            $"select id, first_name, last_name, email, password, birthday::varchar, country_id, gender_id from users where id = {cur_user_id}",
            conn);
        reader = cmd.ExecuteReader();
        if (reader.HasRows)
            while (reader.Read())
                user1 = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),
                    reader.GetString(4), reader.GetString(5), reader.GetInt32(6), reader.GetInt32(7));
        reader.Close();

        userEmailText.Text = user1.email;
        userFirstName.Text = user1.first_name;
        userLastName.Text = user1.last_name;
        userBirthDay.Text = user1.birthday;
        genderComboBox.SelectedIndex = gendersListId.IndexOf(user1.gender_id);
        countryComboBox.SelectedIndex = countriesListId.IndexOf(user1.country_id);
    }

    private void save_button_Click(object sender, RoutedEventArgs e)
    {
        var cmd = new NpgsqlCommand(
            $"update users set first_name = '{userFirstName.Text}', last_name = '{userLastName.Text}', birthday = '{DateFormat(userBirthDay.Text)}'::timestamp, country_id = {countryComboBox.SelectedIndex + 1}, gender_id = {genderComboBox.SelectedIndex + 1} where id = {cur_user_id}",
            conn);
        cmd.ExecuteNonQuery();

        if (userPassword.Text.Trim() != " " && userRePassword.Text == userPassword.Text)
        {
            cmd = new NpgsqlCommand($"update users set password = '{userPassword.Text}' where id = {cur_user_id}",
                conn);
            cmd.ExecuteNonQuery();
        }

        var menurunner = new menurunner(cur_user_id);
        menurunner.Left = Left;
        menurunner.Top = Top;
        menurunner.Show();
        Close();
    }

    private void back_button_Click(object sender, RoutedEventArgs e)
    {
        var menurunner = new menurunner(cur_user_id);
        menurunner.Left = Left;
        menurunner.Top = Top;
        menurunner.Show();
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

    private string DateFormat(string dateString)
    {
        var formattedDate = "";

        DateTime parsedDate;
        if (DateTime.TryParseExact(dateString, "dd.MM.yyyy", null, DateTimeStyles.None, out parsedDate))
            formattedDate = parsedDate.ToString("yyyy-MM-dd");

        return formattedDate;
    }

    private class User
    {
        public readonly string birthday;
        public readonly int country_id;
        public readonly string email;
        public readonly string first_name;
        public readonly int gender_id;
        public int id;
        public readonly string last_name;
        public string password;

        public User(int id, string first_name, string last_name, string email, string password, string birthday,
            int country_id, int gender_id)
        {
            this.id = id;
            this.first_name = first_name;
            this.last_name = last_name;
            this.email = email;
            this.password = password;
            this.birthday = birthday;
            this.country_id = country_id;
            this.gender_id = gender_id;
        }

        public User()
        {
        }
    }

    private class Country
    {
        public int id;
        public string name;

        public Country(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }

    private class Gender
    {
        public int id;
        public string name;

        public Gender(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
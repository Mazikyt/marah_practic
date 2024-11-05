using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using Npgsql;

namespace marah_practic;

public partial class newrunner : Window
{
    private static int cur_user_id;

    private static readonly string connStr = "Host=84.21.173.156:5435;Username=postgres;Password=DBDfJN3Vz9;Database=wss";

    private static readonly NpgsqlConnection conn = new(connStr);

    private User user1 = new();

    public newrunner()
    {
        if (conn.State == ConnectionState.Closed) conn.Open();
        InitializeComponent();

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
            while (reader.Read()) {
                gendersListId.Add(reader.GetInt32(0));
                gendersListName.Add(reader.GetString(1));
            }

        reader.Close();
        genderComboBox.ItemsSource = gendersListName;
    }

    private void back_button_Click(object sender, RoutedEventArgs e) {
        var mainWindow = new MainWindow();
        mainWindow.Left = Left;
        mainWindow.Top = Top;
        mainWindow.Show();
        Close();
    }

   private void registr_button_Click(object sender, RoutedEventArgs e)
        {
            // Проверка email
            if (string.IsNullOrWhiteSpace(userEmail.Text) || !IsValidEmail(userEmail.Text))
            {
                MessageBox.Show("Введите корректный email.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка пароля
            if (string.IsNullOrWhiteSpace(userPassword.Text))
            {
                MessageBox.Show("Пароль не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка повторного ввода пароля
            if (userPassword.Text != userRePassword.Text)
            {
                MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка имени
            if (string.IsNullOrWhiteSpace(userFirstName.Text))
            {
                MessageBox.Show("Введите имя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка фамилии
            if (string.IsNullOrWhiteSpace(userLastName.Text))
            {
                MessageBox.Show("Введите фамилию.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка выбора пола
            if (genderComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите пол.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка даты рождения
            if (!userBirthDay.SelectedDate.HasValue)
            {
                MessageBox.Show("Выберите дату рождения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка выбора страны
            if (countryComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите страну.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Если все проверки пройдены
            MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool IsValidEmail(string email)
        {
            // Регулярное выражение для проверки корректности email
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
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
        public string birthday;
        public int country_id;
        public string email;
        public string first_name;
        public int gender_id;
        public int id;
        public string last_name;
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
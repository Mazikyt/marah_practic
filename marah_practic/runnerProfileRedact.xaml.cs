using Npgsql;
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

namespace marah_practic
{
    /// <summary>
    /// Логика взаимодействия для runnerProfileRedact.xaml
    /// </summary>
    public partial class runnerProfileRedact : Window
    {
        static int cur_user_id;

        User user1 = new User();

        private static string connStr = "Host=84.21.173.156:5435;Username=postgres;Password=DBDfJN3Vz9;Database=wss";

        private static NpgsqlConnection conn = new NpgsqlConnection(connStr);

        public runnerProfileRedact(int user_id)
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
            InitializeComponent();
            cur_user_id = user_id;

            List<string> countriesListName = new List<string>();
            List<int> countriesListId = new List<int>();

            var cmd = new NpgsqlCommand("select * from countries", conn);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    countriesListId.Add(reader.GetInt32(0));
                    countriesListName.Add(reader.GetString(1));

                }
            }
            reader.Close();

            countryComboBox.ItemsSource = countriesListName;

            List<string> gendersListName = new List<string>();
            List<int> gendersListId = new List<int>();

            cmd = new NpgsqlCommand("select * from genders", conn);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    gendersListId.Add(reader.GetInt32(0));
                    gendersListName.Add(reader.GetString(1));
                }
            }
            reader.Close();
            genderComboBox.ItemsSource = gendersListName;

            cmd = new NpgsqlCommand($"select id, first_name, last_name, email, password, birthday::varchar, country_id, gender_id from users where id = {cur_user_id}", conn);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user1 = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetInt32(6), reader.GetInt32(7));
                }
            }
            reader.Close();

            userEmailText.Text = user1.email;
            userFirstName.Text = user1.first_name;
            userLastName.Text = user1.last_name;
            userBirthDay.Text = user1.birthday;
            genderComboBox.SelectedIndex = gendersListId.IndexOf(user1.gender_id);
            countryComboBox.SelectedIndex = countriesListId.IndexOf(user1.country_id);
        }

        class User
        {
            public int id;
            public string first_name;
            public string last_name;
            public string email;
            public string password;
            public string birthday;
            public int country_id;
            public int gender_id;

            public User(int id, string first_name, string last_name, string email, string password, string birthday, int country_id, int gender_id)
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

        class Country
        {
            public int id;
            public string name;

            public Country(int id, string name)
            {
                this.id = id;
                this.name = name;
            }
        }

        class Gender
        {
            public int id;
            public string name;

            public Gender(int id, string name)
            {
                this.id = id;
                this.name = name;
            }
        }

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            var cmd = new NpgsqlCommand($"update users set first_name = '{userFirstName.Text}', last_name = '{userLastName.Text}', birthday = '{DateFormat(userBirthDay.Text)}'::timestamp, country_id = {countryComboBox.SelectedIndex+1}, gender_id = {genderComboBox.SelectedIndex+1} where id = {cur_user_id}", conn);
            cmd.ExecuteNonQuery();

            if (userPassword.Text.Trim() != " " && userRePassword.Text == userPassword.Text)
            {
                cmd = new NpgsqlCommand($"update users set password = '{userPassword.Text}' where id = {cur_user_id}", conn);
                cmd.ExecuteNonQuery();
            }

            menurunner menurunner = new menurunner(cur_user_id);
            menurunner.Left = this.Left;
            menurunner.Top = this.Top;
            menurunner.Show();
            this.Close();
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            menurunner menurunner = new menurunner(cur_user_id);
            menurunner.Left = this.Left;
            menurunner.Top = this.Top;
            menurunner.Show();
            this.Close();
        }

        private void logout_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Left = this.Left;
            mainWindow.Top = this.Top;
            mainWindow.Show();
            this.Close();
        }

        private string DateFormat(string dateString)
        {
            var formattedDate = "";

            DateTime parsedDate;
            if (DateTime.TryParseExact(dateString, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                formattedDate = parsedDate.ToString("yyyy-MM-dd");
            }

            return formattedDate;
        }
    }
}

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
    /// Логика взаимодействия для newrunner.xaml
    /// </summary>
    public partial class newrunner : Window
    {
        static int cur_user_id;

        User user1 = new User();

        private static string connStr = "Host=Localhost;Username=postgres;Password=1204;Database=wss";

        private static NpgsqlConnection conn = new NpgsqlConnection(connStr);

        public newrunner()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
            InitializeComponent();

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

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Left = this.Left;
            mainWindow.Top = this.Top;
            mainWindow.Show();
            this.Close();
        }

        private void registr_button_Click(object sender, RoutedEventArgs e)
        {
            var cmd = new NpgsqlCommand($"insert into users(first_name, last_name, birthday, email, password, country_id, gender_id) values ('{userFirstName.Text}','{userLastName.Text}','{DateFormat(userBirthDay.Text)}'::timestamp, '{userEmail.Text}', '{userPassword.Text}', {countryComboBox.SelectedIndex + 1},{genderComboBox.SelectedIndex + 1})", conn);
            cmd.ExecuteNonQuery();

            cmd = new NpgsqlCommand($"select id from users where email = '{userEmail}'", conn);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cur_user_id = reader.GetInt32(0);
                }
            }

            registrationonma registrationonma = new registrationonma(cur_user_id);
            registrationonma.Left = this.Left;
            registrationonma.Top = this.Top;
            registrationonma.Show();
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

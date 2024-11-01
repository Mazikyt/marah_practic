using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
    /// Логика взаимодействия для upravvolonter.xaml
    /// </summary>
    public partial class upravvolonter : Window
    {
        private static string connStr = "Host=Localhost;Username=postgres;Password=1204;Database=wss";

        private static NpgsqlConnection conn = new NpgsqlConnection(connStr);

        public upravvolonter()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
            InitializeComponent();

            List<string> refresh_comboBox = new List<string>();
            refresh_comboBox.Add("Фамилия");
            refresh_comboBox.Add("Имя");
            refresh_comboBox.Add("Страна");
            refresh_comboBox.Add("Пол");

            sort_comboBox.ItemsSource = refresh_comboBox;

        }

        class Volonter
        {
            public string last_name;
            public string first_name;
            public string country;
            public string gender;

            public Volonter(string last_name, string first_name, string country, string gender)
            {
                this.last_name = last_name;
                this.first_name = first_name;
                this.country = country;
                this.gender = gender;
            }
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            adminmenu adminmenu = new adminmenu();
        }

        private void refresh_button_Click(object sender, RoutedEventArgs e)
        {
            int volontersCount = 0;

            string select_string = "select u.last_name, u.first_name, c.name, g.name from users u " +
                "join countries c ON c.id = u.country_id " +
                "join genders g ON g.id = u.gender_id " +
                "where role_id = 5 ";

            if (sort_comboBox.Text != "")
            {
                if (sort_comboBox.Text == "Фамилия")
                {
                    select_string = select_string + "order by u.last_name";
                }
                else if (sort_comboBox.Text == "Имя")
                {
                    select_string = select_string + "order by u.first_name";
                }    
                else if (sort_comboBox.Text == "Страна")
                {
                    select_string = select_string + "order by 3";
                }
                else if (sort_comboBox.Text == "Пол")
                {
                    select_string = select_string + "order by 4";
                }
            }    

            List<Volonter> volonters = new List<Volonter>();

            var cmd = new NpgsqlCommand(select_string, conn);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while(reader.Read())
                {
                    volonters.Add(new Volonter(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                    volontersCount++;
                }
            }
            reader.Close();

            volonterCount_text.Text = volontersCount.ToString();

            var ListView = volonters.Select( x => new
            {
                x.last_name,
                x.first_name,
                x.country,
                x.gender
            }).ToList();

            volonters_ListView.ItemsSource = ListView;
        }

        private void importVolonters_button_Click(object sender, RoutedEventArgs e)
        {
            importVolonters importVolonters = new importVolonters();
            importVolonters.Left = this.Left;
            importVolonters.Top = this.Top;
            importVolonters.Show();
            this.Close();
        }
    }
}

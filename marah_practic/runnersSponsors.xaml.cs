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
    /// Логика взаимодействия для runnersSponsors.xaml
    /// </summary>
    public partial class runnersSponsors : Window
    {
        static int cur_user_id;

        private static string connStr = "Host=Localhost;Username=postgres;Password=1204;Database=wss";

        private static NpgsqlConnection conn = new NpgsqlConnection(connStr);

        public runnersSponsors(int user1)
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
            InitializeComponent();
            cur_user_id = user1;

            Charity cur_charity = new Charity();

            var cmd = new NpgsqlCommand("select ch.id, ch.name, ch.logo, ch.description from registrations r " +
      "join charities ch on r.charity_id = ch.id " +
      "join users u on r.user_id = u.id " +
      "where u.id = @UserId", conn);
            cmd.Parameters.AddWithValue("@UserId", cur_user_id);

            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cur_charity = new Charity(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                }
            }
            reader.Close();


            charityName.Text = cur_charity.name;
            string logoPath = $"images/{cur_charity.logo}".TrimEnd('/');
            charityLogo.Source = new BitmapImage(new Uri($"pack://application:,,,/{logoPath}", UriKind.Absolute));

            charityDiscription.Text = cur_charity.description;

            List<Sponsor> sponsorsList = new List<Sponsor>();

            float total_amount = 0;

            cmd = new NpgsqlCommand($"select (s.first_name || '_' || s.last_name) as name, s.amount from sponsorships s " +
                $"join registrations r " +
                $"on r.id = s.registration_id " +
                $"where r.user_id = {cur_user_id}", conn);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while(reader.Read())
                {
                    sponsorsList.Add(new Sponsor(reader.GetString(0), reader.GetFloat(1)));
                    total_amount += reader.GetFloat(1);
                }
            }
            reader.Close();

            var ListView = sponsorsList.Select(x => new
            {
                x.name,
                x.amount
            }).ToList();

            total_amount_text.Text = "Всего: " + total_amount.ToString();

            sponsorsListView.ItemsSource = ListView;

        }

        class Charity
        {
            public int id;
            public string name;
            public string logo;
            public string description;

            public Charity(int id, string name, string logo, string description)
            {
                this.id = id;
                this.name = name;
                this.logo = logo;
                this.description = description;
            }

            public Charity()
            {

            }
        }

        class Sponsor
        {
            public string name;
            public float amount;

            public Sponsor(string name, float amount)
            {
                this.name = name;
                this.amount = amount;
            }
        }

        private void logout_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Left = this.Left;
            mainWindow.Top = this.Top;
            mainWindow.Show();
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
    }
}

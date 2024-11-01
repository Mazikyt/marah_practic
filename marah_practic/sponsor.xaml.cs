using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
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
    /// Логика взаимодействия для sponsor.xaml
    /// </summary>
    public partial class sponsor : Window
    {
        private static string connStr = "Host=Localhost;Username=postgres;Password=1204;Database=wss";

        private static NpgsqlConnection conn = new NpgsqlConnection(connStr);

        private int amount = 50;

        List<Charity> charitiesList = new List<Charity>();
        List<string> usersList = new List<string>();
        List<int> usersIdList = new List<int>();
        public sponsor()
        {
            InitializeComponent();
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }

            var cmd = new NpgsqlCommand("select u.id, u.first_name || ' ' || u.last_name || ' - ' || u.id || ' ' || '(' || cu.name || ')' as runner, " +
                "ch.id, ch.name, ch.description, ch.logo from registrations r " +
                "join users u " +
                "on u.id = r.user_id " +
                "join charities ch " +
                "on ch.id = r.charity_id " +
                "join countries cu " +
                "on cu.id = u.country_id", conn);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while(reader.Read())
                {
                    usersIdList.Add(reader.GetInt32(0));
                    usersList.Add(reader.GetString(1));
                    charitiesList.Add(new Charity(reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetString(5)));
                }
            }
            reader.Close();

            runnerInfo.ItemsSource = usersList;
            runnerInfo.SelectedIndex = 0;

            charityName.Text = charitiesList[runnerInfo.SelectedIndex].name;
            charitiesName.Text = charitiesList[runnerInfo.SelectedIndex].name;
            charityDiscription.Text = charitiesList[runnerInfo.SelectedIndex].description;
            charityLogo.Source = new BitmapImage(new Uri($"pack://application:,,,/images/{charitiesList[runnerInfo.SelectedIndex].logo}"));
        }
        class Charity
        {
            public int id;
            public string name;
            public string description;
            public string logo;

            public Charity(int id, string name, string description, string logo)
            {
                this.id = id;
                this.name = name;
                this.description = description;
                this.logo = logo;
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

        private void pay_button_Click(object sender, RoutedEventArgs e)
        {
            int registr_id = 0;

            var cmd = new NpgsqlCommand($"select * from registrations " +
                $"where user_id = {usersIdList[runnerInfo.SelectedIndex]} and charity_id = {charitiesList[runnerInfo.SelectedIndex].id}", conn);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while(reader.Read())
                {
                    registr_id = reader.GetInt32(0);
                }
            }
            reader.Close();

            string name = sponsorName.Text;
            string[] words = name.Split();
            string firstWord = words[0];
            string secondWord = words[1];

            cmd = new NpgsqlCommand($"insert into sponsorships(first_name, last_name, amount, registration_id) values('{firstWord}', '{secondWord}', {amount}, {registr_id})", conn);
            cmd.ExecuteNonQuery();

            thanksSponsor thanksSponsor = new thanksSponsor(runnerInfo.Text, charitiesName.Text, amount);
            thanksSponsor.Left = this.Left;
            thanksSponsor.Top = this.Top;
            thanksSponsor.Show();
            this.Close();
        }

        private void closeCharityInfo_button_Click(object sender, RoutedEventArgs e)
        {
            charityInfo.Visibility = Visibility.Hidden;
        }

        private void openCharityInfo_button_Click(object sender, RoutedEventArgs e)
        {
            charityInfo.Visibility = Visibility.Visible;
        }

        private void runnerInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            charityName.Text = charitiesList[runnerInfo.SelectedIndex].name;
            charitiesName.Text = charitiesList[runnerInfo.SelectedIndex].name;
            charityDiscription.Text = charitiesList[runnerInfo.SelectedIndex].description;
            charityLogo.Source = new BitmapImage(new Uri($"pack://application:,,,/images/{charitiesList[runnerInfo.SelectedIndex].logo}"));
        }

        private void minusAmount_Click(object sender, RoutedEventArgs e)
        {
            amount -= 5;
            amountText.Text = "$" + amount.ToString();
            amountTextBox.Text = amount.ToString();
        }

        private void plusAmount_Click(object sender, RoutedEventArgs e)
        {
            amount += 5;
            amountText.Text = "$" + amount.ToString();
            amountTextBox.Text = amount.ToString();
        }
    }
}

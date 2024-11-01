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
    /// Логика взаимодействия для registrationonma.xaml
    /// </summary>
    public partial class registrationonma : Window
    {
        static int cur_user_id;

        private static string connStr = "Host=Localhost;Username=postgres;Password=1204;Database=wss";

        private static NpgsqlConnection conn = new NpgsqlConnection(connStr);

        List<Charity> charityList = new List<Charity>();

        public registrationonma(int user1)
        {
            InitializeComponent();

            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }

            cur_user_id = user1;


            List<string> charityListName = new List<string>();

            var cmd = new NpgsqlCommand($"select * from charities", conn);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    charityList.Add(new Charity(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                    charityListName.Add(reader.GetString(1));
                }
            }

            charityComboBox.ItemsSource = charityListName;
            charityComboBox.SelectedIndex = 0;

            charityName.Text = charityList[charityComboBox.SelectedIndex].name;
            charityDiscription.Text = charityList[charityComboBox.SelectedIndex].description;
            charityLogo.Source = new BitmapImage(new Uri($"pack://application:,,,/images/{charityList[charityComboBox.SelectedIndex].logo}"));
        }

        static int amount;

        static bool varsA = true;
        static bool varsB = false;
        static bool varsC = false;
        static bool fullMarathon = false;
        static bool halfMarathon = false;
        static bool miniMarathon = false;

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

        private void total_amount()
        {

            int total = 0;

            if (varsA == true)
            {
                total += 0;
            }
            else if (varsB == true)
            {
                total += 20;
            }
            else if (varsC == true)
            {
                total += 45;
            }

            if (fullMarathon == true)
            {
                total += 145;
            }
            if (halfMarathon == true)
            {
                total += 75;
            }
            if (miniMarathon == true)
            {
                total += 20;
            }

            amount = total;

            if (totalAmount != null)
            {
                totalAmount.Text = "$" + amount.ToString();
            }
        }

        private void registr_button_Click(object sender, RoutedEventArgs e)
        {
            //додумать как заполнять

            endregistration endregistration = new endregistration();
            endregistration.Top = this.Top;
            endregistration.Left = this.Left;
            endregistration.Show();
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

        private void cancel_button_Click(object sender, RoutedEventArgs e)
        {
            menurunner menurunner = new menurunner(0);
            menurunner.Top = this.Top;
            menurunner.Left = this.Left;
            menurunner.Show();
            this.Close();
        }

        private void varA_Checked(object sender, RoutedEventArgs e)
        {
            varsA = true;
            varsB = false;
            varsC = false;
            total_amount();
        }

        private void varB_Checked(object sender, RoutedEventArgs e)
        {
            varsA = false;
            varsB = true;
            varsC = false;
            total_amount();
        }

        private void varC_Checked(object sender, RoutedEventArgs e)
        {
            varsA = false;
            varsB = false;
            varsC = true;
            total_amount();
        }

        private void fullMar_Checked(object sender, RoutedEventArgs e)
        {
            fullMarathon = !fullMarathon;
            total_amount();
        }

        private void halfMar_Checked(object sender, RoutedEventArgs e)
        {
            halfMarathon = !halfMarathon;
            total_amount();
        }

        private void miniMar_Checked(object sender, RoutedEventArgs e)
        {
            miniMarathon = !miniMarathon;
            total_amount();
        }

        private void closeCharityInfo_button_Click(object sender, RoutedEventArgs e)
        {
            charityInfo.Visibility = Visibility.Hidden;
        }

        private void openCharityInfo_button_Click(object sender, RoutedEventArgs e)
        {
            charityInfo.Visibility = Visibility.Visible;
        }

        private void charityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            charityName.Text = charityList[charityComboBox.SelectedIndex].name;
            charityDiscription.Text = charityList[charityComboBox.SelectedIndex].description;
            charityLogo.Source = new BitmapImage(new Uri($"pack://application:,,,/images/{charityList[charityComboBox.SelectedIndex].logo}"));
        }
    }
}

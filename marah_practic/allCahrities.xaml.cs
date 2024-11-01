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
    /// Логика взаимодействия для allCahrities.xaml
    /// </summary>
    public partial class allCahrities : Window
    {
        private static string connStr = "Host=Localhost;Username=postgres;Password=1204;Database=wss";

        private static NpgsqlConnection conn = new NpgsqlConnection(connStr);

        public allCahrities()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
            InitializeComponent();

            List<Charity> charities = new List<Charity>();

            var cmd = new NpgsqlCommand("select logo, name, description from charities", conn);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while(reader.Read())
                {
                    charities.Add(new Charity(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
                }
            }

            var ListView = charities.Select(x => new
            {
                x.logo,
                x.name,
                x.description
            }).ToList();

            charities_ListView.ItemsSource = ListView;
        }

        class Charity
        {
            public Image logo;
            public string name;
            public string description;

            public Charity(string logoPath, string name, string description)
            {
                this.logo = new Image();
                this.logo.Source = new BitmapImage(new Uri($"pack://application:,,,/images/{logoPath}"));
                this.name = name;
                this.description = description;
            }
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            DopInfo dopInfo = new DopInfo();
            dopInfo.Left = this.Left;
            dopInfo.Top = this.Top;
            dopInfo.Show();
            this.Close();
        }
    }
}

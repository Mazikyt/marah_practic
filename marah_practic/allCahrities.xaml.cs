using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Npgsql;

namespace marah_practic;

/// <summary>
///     Логика взаимодействия для allCahrities.xaml
/// </summary>
public partial class allCahrities : Window
{
    private static readonly string connStr = "Host=84.21.173.156:5435;Username=postgres;Password=DBDfJN3Vz9;Database=wss";

    private static readonly NpgsqlConnection conn = new(connStr);

    public allCahrities()
    {
        if (conn.State == ConnectionState.Closed) conn.Open();
        InitializeComponent();

        var charities = new List<Charity>();

        var cmd = new NpgsqlCommand("select logo, name, description from charities", conn);
        var reader = cmd.ExecuteReader();
        if (reader.HasRows)
            while (reader.Read())
                charities.Add(new Charity(reader.GetString(0), reader.GetString(1), reader.GetString(2)));

        var ListView = charities.Select(x => new
        {
            x.logo,
            x.name,
            x.description
        }).ToList();

        charities_ListView.ItemsSource = ListView;
    }

    private void back_button_Click(object sender, RoutedEventArgs e)
    {
        var dopInfo = new DopInfo();
        dopInfo.Left = Left;
        dopInfo.Top = Top;
        dopInfo.Show();
        Close();
    }

    private class Charity
    {
        public readonly string description;
        public readonly Image logo;
        public readonly string name;

        public Charity(string logoPath, string name, string description)
        {
            logo = new Image();
            logo.Source = new BitmapImage(new Uri($"pack://application:,,,/images/{logoPath}"));
            this.name = name;
            this.description = description;
        }
    }
}
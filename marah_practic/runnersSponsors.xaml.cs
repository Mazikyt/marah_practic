using System.Data;
using System.Windows;
using System.Windows.Media.Imaging;
using Npgsql;

namespace marah_practic;

/// <summary>
///     Логика взаимодействия для runnersSponsors.xaml
/// </summary>
public partial class runnersSponsors : Window
{
    private static int cur_user_id;

    private static readonly string connStr = "Host=84.21.173.156:5435;Username=postgres;Password=DBDfJN3Vz9;Database=wss";

    private static readonly NpgsqlConnection conn = new(connStr);

    public runnersSponsors(int user1)
    {
        if (conn.State == ConnectionState.Closed) conn.Open();
        InitializeComponent();
        cur_user_id = user1;

        var cur_charity = new Charity();

        var cmd = new NpgsqlCommand("select ch.id, ch.name, ch.logo, ch.description from registrations r " +
                                    "join charities ch on r.charity_id = ch.id " +
                                    "join users u on r.user_id = u.id " +
                                    "where u.id = @UserId", conn);
        cmd.Parameters.AddWithValue("@UserId", cur_user_id);

        var reader = cmd.ExecuteReader();
        if (reader.HasRows)
            while (reader.Read())
                cur_charity = new Charity(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                    reader.GetString(3));
        reader.Close();


        charityName.Text = cur_charity.name;
        var logoPath = $"images/{cur_charity.logo}".TrimEnd('/');
        charityLogo.Source = new BitmapImage(new Uri($"pack://application:,,,/{logoPath}", UriKind.Absolute));

        charityDiscription.Text = cur_charity.description;

        var sponsorsList = new List<Sponsor>();

        float total_amount = 0;

        cmd = new NpgsqlCommand($"select (s.first_name || '_' || s.last_name) as name, s.amount from sponsorships s " +
                                $"join registrations r " +
                                $"on r.id = s.registration_id " +
                                $"where r.user_id = {cur_user_id}", conn);
        reader = cmd.ExecuteReader();
        if (reader.HasRows)
            while (reader.Read())
            {
                sponsorsList.Add(new Sponsor(reader.GetString(0), reader.GetFloat(1)));
                total_amount += reader.GetFloat(1);
            }

        reader.Close();

        var ListView = sponsorsList.Select(x => new
        {
            x.name,
            x.amount
        }).ToList();

        total_amount_text.Text = "Всего: " + total_amount;

        sponsorsListView.ItemsSource = ListView;
    }

    private void logout_button_Click(object sender, RoutedEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Left = Left;
        mainWindow.Top = Top;
        mainWindow.Show();
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

    private class Charity
    {
        public readonly string description;
        public int id;
        public readonly string logo;
        public readonly string name;

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

    private class Sponsor
    {
        public readonly float amount;
        public readonly string name;

        public Sponsor(string name, float amount)
        {
            this.name = name;
            this.amount = amount;
        }
    }
}
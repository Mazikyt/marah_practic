using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Npgsql;

namespace marah_practic;

/// <summary>
///     Логика взаимодействия для sponsor.xaml
/// </summary>
public partial class sponsor : Window
{
    private static readonly string connStr = "Host=84.21.173.156:5435;Username=postgres;Password=DBDfJN3Vz9;Database=wss";

    private static readonly NpgsqlConnection conn = new(connStr);

    private int amount = 50;

    private readonly List<Charity> charitiesList = new();
    private readonly List<int> usersIdList = new();
    private readonly List<string> usersList = new();

    public sponsor()
    {
        InitializeComponent();
        if (conn.State == ConnectionState.Closed) conn.Open();

        var cmd = new NpgsqlCommand(
            "select u.id, u.first_name || ' ' || u.last_name || ' - ' || u.id || ' ' || '(' || cu.name || ')' as runner, " +
            "ch.id, ch.name, ch.description, ch.logo from registrations r " +
            "join users u " +
            "on u.id = r.user_id " +
            "join charities ch " +
            "on ch.id = r.charity_id " +
            "join countries cu " +
            "on cu.id = u.country_id", conn);
        var reader = cmd.ExecuteReader();
        if (reader.HasRows)
            while (reader.Read())
            {
                usersIdList.Add(reader.GetInt32(0));
                usersList.Add(reader.GetString(1));
                charitiesList.Add(new Charity(reader.GetInt32(2), reader.GetString(3), reader.GetString(4),
                    reader.GetString(5)));
            }

        reader.Close();

        runnerInfo.ItemsSource = usersList;
        runnerInfo.SelectedIndex = 0;

        charityName.Text = charitiesList[runnerInfo.SelectedIndex].name;
        charitiesName.Text = charitiesList[runnerInfo.SelectedIndex].name;
        charityDiscription.Text = charitiesList[runnerInfo.SelectedIndex].description;
        charityLogo.Source =
            new BitmapImage(new Uri($"pack://application:,,,/images/{charitiesList[runnerInfo.SelectedIndex].logo}"));
    }

    private void back_button_Click(object sender, RoutedEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Left = Left;
        mainWindow.Top = Top;
        mainWindow.Show();
        Close();
    }

    private void pay_button_Click(object sender, RoutedEventArgs e)
    {
        var registr_id = 0;

        var cmd = new NpgsqlCommand($"select * from registrations " +
                                    $"where user_id = {usersIdList[runnerInfo.SelectedIndex]} and charity_id = {charitiesList[runnerInfo.SelectedIndex].id}",
            conn);
        var reader = cmd.ExecuteReader();
        if (reader.HasRows)
            while (reader.Read())
                registr_id = reader.GetInt32(0);
        reader.Close();

        var name = sponsorName.Text;
        var words = name.Split();
        var firstWord = words[0];
        var secondWord = words[1];

        cmd = new NpgsqlCommand(
            $"insert into sponsorships(first_name, last_name, amount, registration_id) values('{firstWord}', '{secondWord}', {amount}, {registr_id})",
            conn);
        cmd.ExecuteNonQuery();

        var thanksSponsor = new thanksSponsor(runnerInfo.Text, charitiesName.Text, amount);
        thanksSponsor.Left = Left;
        thanksSponsor.Top = Top;
        thanksSponsor.Show();
        Close();
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
        charityLogo.Source =
            new BitmapImage(new Uri($"pack://application:,,,/images/{charitiesList[runnerInfo.SelectedIndex].logo}"));
    }

    private void minusAmount_Click(object sender, RoutedEventArgs e)
    {
        amount -= 5;
        amountText.Text = "$" + amount;
        amountTextBox.Text = amount.ToString();
    }

    private void plusAmount_Click(object sender, RoutedEventArgs e)
    {
        amount += 5;
        amountText.Text = "$" + amount;
        amountTextBox.Text = amount.ToString();
    }

    private class Charity
    {
        public readonly string description;
        public readonly int id;
        public readonly string logo;
        public readonly string name;

        public Charity(int id, string name, string description, string logo)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.logo = logo;
        }
    }
}
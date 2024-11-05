using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Npgsql;

namespace marah_practic;

public partial class registrationonma
{
    private static int cur_user_id;

    private static readonly string connStr = "Host=84.21.173.156:5435;Username=postgres;Password=DBDfJN3Vz9;Database=wss";

    private static readonly NpgsqlConnection conn = new(connStr);

    private static int amount;

    private static bool varsA = true;
    private static bool varsB;
    private static bool varsC;
    private static bool fullMarathon;
    private static bool halfMarathon;
    private static bool miniMarathon;

    private readonly List<Charity> charityList = new();

    public registrationonma(int user1)
    {
        InitializeComponent();

        if (conn.State == ConnectionState.Closed) conn.Open();

        cur_user_id = user1;


        var charityListName = new List<string>();

        var cmd = new NpgsqlCommand("select * from charities", conn);
        var reader = cmd.ExecuteReader();
        if (reader.HasRows)
            while (reader.Read())
            {
                charityList.Add(new Charity(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                    reader.GetString(3)));
                charityListName.Add(reader.GetString(1));
            }

        charityComboBox.ItemsSource = charityListName;
        charityComboBox.SelectedIndex = 0;

        charityName.Text = charityList[charityComboBox.SelectedIndex].name;
        charityDiscription.Text = charityList[charityComboBox.SelectedIndex].description;
        charityLogo.Source =
            new BitmapImage(
                new Uri($"pack://application:,,,/images/{charityList[charityComboBox.SelectedIndex].logo}"));
        
    }

    private void total_amount()
    {
        var total = 0;

        if (varsA)
            total += 0;
        else if (varsB)
            total += 20;
        else if (varsC) total += 45;

        if (fullMarathon) total += 145;
        if (halfMarathon) total += 75;
        if (miniMarathon) total += 20;

        amount = total;

        if (totalAmount != null) totalAmount.Text = "$" + amount;
    }

    private void registr_button_Click(object sender, RoutedEventArgs e)
    {
        //додумать как заполнять

        var endregistration = new endregistration();
        endregistration.Top = Top;
        endregistration.Left = Left;
        endregistration.Show();
        Close();
    }

    private void logout_button_Click(object sender, RoutedEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Left = Left;
        mainWindow.Top = Top;
        mainWindow.Show();
        Close();
    }

    private void cancel_button_Click(object sender, RoutedEventArgs e)
    {
        var menurunner = new menurunner(0);
        menurunner.Top = Top;
        menurunner.Left = Left;
        menurunner.Show();
        Close();
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
        charityLogo.Source =
            new BitmapImage(
                new Uri($"pack://application:,,,/images/{charityList[charityComboBox.SelectedIndex].logo}"));
    }

    private class Charity
    {
        public readonly string description;
        public int id;
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
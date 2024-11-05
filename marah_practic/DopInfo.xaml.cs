using System.Windows;

namespace marah_practic;

public partial class DopInfo : Window
{
    public DopInfo()
    {
        InitializeComponent();
    }

    private void logout_button_Click(object sender, RoutedEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Left = Left;
        mainWindow.Top = Top;
        mainWindow.Show();
        Close();
    }

    private void aboutMarathon_button_Click(object sender, RoutedEventArgs e)
    {
        var aboutMarathon = new aboutMarathon();
        aboutMarathon.Left = Left;
        aboutMarathon.Top = Top;
        aboutMarathon.Show();
        Close();
    }

    private void howLongMarathon_Click(object sender, RoutedEventArgs e)
    {
        var howLongMarathon = new howLongMarathon();
        howLongMarathon.Top = Top;
        howLongMarathon.Left = Left;
        howLongMarathon.Show();
        Close();
    }

    private void prevResults_button_Click(object sender, RoutedEventArgs e)
    {
        var prevResults = new prevResults();
        prevResults.Left = Left;
        prevResults.Top = Top;
        prevResults.Show();
        Close();
    }

    private void allCharities_button_Click(object sender, RoutedEventArgs e)
    {
        var allCahrities = new allCahrities();
        allCahrities.Left = Left;
        allCahrities.Top = Top;
        allCahrities.Show();
        Close();
    }
}
using System.Windows;

namespace marah_practic;

/// <summary>
///     Логика взаимодействия для menurunner.xaml
/// </summary>
public partial class menurunner : Window
{
    private static int cur_user_id;

    public menurunner(int user_id)
    {
        InitializeComponent();
        ContactPage.Visibility = Visibility.Hidden;
        cur_user_id = user_id;
    }

    private void registrOnMa_button_Click(object sender, RoutedEventArgs e)
    {
        var registrationonma = new registrationonma(cur_user_id);
        registrationonma.Left = Left;
        registrationonma.Top = Top;
        registrationonma.Show();
        Close();
    }

    private void myResults_button_Click(object sender, RoutedEventArgs e)
    {
        var reziltrunner = new reziltrunner();
        reziltrunner.Left = Left;
        reziltrunner.Top = Top;
        reziltrunner.Show();
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

    private void runnerProfileRedact_button_Click(object sender, RoutedEventArgs e)
    {
        var runnerProfileRedact = new runnerProfileRedact(cur_user_id);
        runnerProfileRedact.Left = Left;
        runnerProfileRedact.Top = Top;
        runnerProfileRedact.Show();
        Close();
    }

    private void Contacts_button_Click(object sender, RoutedEventArgs e)
    {
        ContactPage.Visibility = Visibility.Visible;
    }

    private void CloseContactPage_Click(object sender, RoutedEventArgs e)
    {
        ContactPage.Visibility = Visibility.Hidden;
    }


    private void runnersSponsors_button_Click(object sender, RoutedEventArgs e)
    {
        var runnersSponsors = new runnersSponsors(cur_user_id);
        runnersSponsors.Left = Left;
        runnersSponsors.Top = Top;
        runnersSponsors.Show();
        Close();
    }
}
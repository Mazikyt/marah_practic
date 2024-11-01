using System.Windows;

namespace marah_practic;

/// <summary>
///     Логика взаимодействия для menukoordinator.xaml
/// </summary>
public partial class menukoordinator : Window
{
    private static int cur_user_id;

    public menukoordinator(int user_id)
    {
        InitializeComponent();
        cur_user_id = user_id;
    }

    private void back_button_Click(object sender, RoutedEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Left = Left;
        mainWindow.Top = Top;
        mainWindow.Show();
        Close();
    }

    private void runnersUprav_button_Click(object sender, RoutedEventArgs e)
    {
        var runneruprav = new runneruprav();
        runneruprav.Left = Left;
        runneruprav.Top = Top;
        runneruprav.Show();
        Close();
    }
}
using System.Windows;

namespace marah_practic;

/// <summary>
///     Логика взаимодействия для endregistration.xaml
/// </summary>
public partial class endregistration : Window
{
    public endregistration()
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

    private void back_button_Click(object sender, RoutedEventArgs e)
    {
        var menurunner = new menurunner(0);
        menurunner.Top = Top;
        menurunner.Left = Left;
        menurunner.Show();
        Close();
    }
}
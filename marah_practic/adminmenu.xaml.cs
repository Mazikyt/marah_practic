using System.Windows;

namespace marah_practic;

/// <summary>
///     Логика взаимодействия для adminmenu.xaml
/// </summary>
public partial class adminmenu : Window
{
    private int cur_user_id;

    public adminmenu()
    {
        InitializeComponent();
    }

    private void back_button_Click(object sender, RoutedEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Left = Left;
        mainWindow.Top = Top;
        mainWindow.Show();
        Close();
    }

    private void volonteuprav_button_Click(object sender, RoutedEventArgs e)
    {
        var upravvolonter = new upravvolonter();
        upravvolonter.Left = Left;
        upravvolonter.Top = Top;
        upravvolonter.Show();
        Close();
    }

    private void user_adminButton_Click(object sender, RoutedEventArgs e)
    {
        var upravusersadmin = new upravusersadmin();
        upravusersadmin.Left = Left;
        upravusersadmin.Top = Top;
        upravusersadmin.Show();
        Close();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
    }
}
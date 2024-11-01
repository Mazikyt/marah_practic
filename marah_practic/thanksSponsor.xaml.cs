using System.Windows;

namespace marah_practic;

/// <summary>
///     Логика взаимодействия для thanksSponsor.xaml
/// </summary>
public partial class thanksSponsor : Window
{
    public thanksSponsor(string runnerInfo, string charityName, int amount)
    {
        InitializeComponent();
        runnerInfo_text.Text = runnerInfo;
        charityName_text.Text = charityName;
        amount_text.Text = "$" + amount;
    }

    private void back_button_Click(object sender, RoutedEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Left = Left;
        mainWindow.Top = Top;
        mainWindow.Show();
        Close();
    }
}
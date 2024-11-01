using System.Windows;
using System.Windows.Threading;

namespace marah_practic;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly DateTime dateOfStart = new(2024, 11, 02, 6, 0, 0);
    private readonly DispatcherTimer timer; // Объявляем таймер


    public MainWindow()
    {
        InitializeComponent();
        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1); // Устанавливаем интервал в 1 секунду
        timer.Tick += timer_Tick; // Привязываем событие Tick к функции timer_Tick
        timer.Start(); // Запускаем таймер
    }

    private void timer_Tick(object sender, EventArgs e)
    {
        var different = dateOfStart.Subtract(DateTime.Now);
        timerLabel.Text =
            $"{different.Days} дней {different.Hours} часов и {different.Minutes} минут до старта марафона!";
    }

    private void login_button_Click(object sender, RoutedEventArgs e)
    {
        var login = new Login();
        login.Left = Left;
        login.Top = Top;
        login.Show();
        Close();
    }

    private void wannaBeRunner_button_Click(object sender, RoutedEventArgs e)
    {
        var iwantrunner = new iwantrunner();
        iwantrunner.Left = Left;
        iwantrunner.Top = Top;
        iwantrunner.Show();
        Close();
    }

    private void iwantbesponsor_Click(object sender, RoutedEventArgs e)
    {
        var sponsor = new sponsor();
        sponsor.Left = Left;
        sponsor.Top = Top;
        sponsor.Show();
        Close();
    }

    private void DopInfo_button_Click(object sender, RoutedEventArgs e)
    {
        var dopInfo = new DopInfo();
        dopInfo.Left = Left;
        dopInfo.Top = Top;
        dopInfo.Show();
        Close();
    }
}
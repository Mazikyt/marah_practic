using System.Windows;
using System.Windows.Threading;

namespace marah_practic;

/// <summary>
///     Логика взаимодействия для iwantrunner.xaml
/// </summary>
public partial class iwantrunner : Window
{
    private readonly DateTime dateOfStart = new(2024, 11, 02, 6, 0, 0);
    private readonly DispatcherTimer timer; // Объявляем таймер

    public iwantrunner()
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

    private void back_button_Click(object sender, RoutedEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Left = Left;
        mainWindow.Top = Top;
        mainWindow.Show();
        Close();
    }

    private void newRunner_button_Click(object sender, RoutedEventArgs e)
    {
        var newrunner = new newrunner();
        newrunner.Left = Left;
        newrunner.Top = Top;
        newrunner.Show();
        Close();
    }

    private void oldRunner_button_Click(object sender, RoutedEventArgs e)
    {
        var login = new Login();
        login.Left = Left;
        login.Top = Top;
        login.Show();
        Close();
    }

    private void login_button_Click(object sender, RoutedEventArgs e)
    {
        var login = new Login();
        login.Left = Left;
        login.Top = Top;
        login.Show();
        Close();
    }
}
using System.Windows;
using System.Windows.Threading;

namespace marah_practic;

public partial class reziltrunner : Window
{
    private readonly DateTime dateOfStart = new(2024, 11, 02, 18, 0, 0);
    private readonly DispatcherTimer timer; // Объявляем таймер

    public reziltrunner()
    {
        InitializeComponent();
        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1); // Устанавливаем интервал в 1 секунду
        timer.Tick += timer_Tick; // Привязываем событие Tick к функции timer_Tick
        timer.Start(); // Запускаем таймер
    }

    private void timer_Tick(object sender, EventArgs e) {
        var different = dateOfStart.Subtract(DateTime.Now);
        if (different.TotalSeconds <= 0) {
            timer.Stop();
            timerLabel.Text = "Марафон начался!";
        }
        else {
            timerLabel.Text =
                $"{different.Days} дней {different.Hours} часов и {different.Minutes} минут до старта марафона!";
        }
    }

    private void back_button_Click(object sender, RoutedEventArgs e)
    {
        var menurunner = new menurunner(0);
        menurunner.Top = Top;
        menurunner.Left = Left;
        menurunner.Show();
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
}
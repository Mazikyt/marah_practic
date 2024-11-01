using System.Windows;
using System.Windows.Threading;

namespace marah_practic;

/// <summary>
///     Логика взаимодействия для addedit.xaml
/// </summary>
public partial class addedit : Window
{
    private readonly DateTime dateOfStart = new(2024, 11, 02, 6, 0, 0);
    private readonly DispatcherTimer timer; // Объявляем таймер

    public addedit()
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace marah_practic
{
    /// <summary>
    /// Логика взаимодействия для addedit.xaml
    /// </summary>
    public partial class addedit : Window
    {
        public addedit()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Устанавливаем интервал в 1 секунду
            timer.Tick += timer_Tick; // Привязываем событие Tick к функции timer_Tick
            timer.Start(); // Запускаем таймер
        }

        private DateTime dateOfStart = new DateTime(2024, 11, 02, 6, 0, 0);
        private DispatcherTimer timer; // Объявляем таймер

        private void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan different = dateOfStart.Subtract(DateTime.Now);
            timerLabel.Text = $"{different.Days} дней {different.Hours} часов и {different.Minutes} минут до старта марафона!";
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

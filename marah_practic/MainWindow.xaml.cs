using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace marah_practic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        public MainWindow()
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
        private void login_button_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Left = this.Left;
            login.Top = this.Top;
            login.Show();
            this.Close();
        }

        private void wannaBeRunner_button_Click(object sender, RoutedEventArgs e)
        {
            iwantrunner iwantrunner = new iwantrunner();
            iwantrunner.Left = this.Left;
            iwantrunner.Top = this.Top;
            iwantrunner.Show();
            this.Close();
        }

        private void iwantbesponsor_Click(object sender, RoutedEventArgs e)
        {
            sponsor sponsor = new sponsor();
            sponsor.Left = this.Left;
            sponsor.Top = this.Top;
            sponsor.Show();
            this.Close();
        }

        private void DopInfo_button_Click(object sender, RoutedEventArgs e)
        {
            DopInfo dopInfo = new DopInfo();
            dopInfo.Left = this.Left;
            dopInfo.Top = this.Top;
            dopInfo.Show();
            this.Close();
        }
    }
}
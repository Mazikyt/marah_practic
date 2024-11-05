using System.Windows;
using System.Windows.Threading;

namespace marah_practic
{
    public partial class addedit
    {
        private readonly DateTime dateOfStart = new(2024, 11, 02, 18, 0, 0);
        private readonly DispatcherTimer timer; 

        public addedit()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); 
            timer.Tick += timer_Tick; 
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            var different = dateOfStart.Subtract(DateTime.Now);
            if (different.TotalSeconds <= 0)
            {
                timer.Stop();
                timerLabel.Text = "Марафон начался!";
            }
            else
            {
                timerLabel.Text = $"{different.Days} дней {different.Hours} часов и {different.Minutes} минут до старта марафона!";
            }
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            // Логика для кнопки "Назад"
        }

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                // Логика сохранения данных
                MessageBox.Show("Данные сохранены успешно!");
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool ValidateFields()
        {
            // Проверка на обязательные поля
            return !string.IsNullOrWhiteSpace(nameTextBox.Text) && !string.IsNullOrWhiteSpace(descriptionTextBox.Text);
        }
    }
}
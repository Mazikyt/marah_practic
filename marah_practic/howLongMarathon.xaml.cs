using System.Windows;
using System.Windows.Media.Imaging;

namespace marah_practic;

public partial class howLongMarathon : Window
{
    public howLongMarathon()
    {
        InitializeComponent();
    }

    private void back_button_Click(object sender, RoutedEventArgs e)
    {
        var dopInfo = new DopInfo();
        dopInfo.Left = Left;
        dopInfo.Top = Top;
        dopInfo.Show();
        Close();
    }

    private void f1_button_Click(object sender, RoutedEventArgs e)
    {
        bigImage_image.Source = new BitmapImage(new Uri("pack://application:,,,/images/f1-car.jpg"));
        bigText_text.Text = "Формула 1";
        bigAbout_text.Text = "Скорость формулы 1 = 354 км/ч";
    }

    private void slug_button_Click(object sender, RoutedEventArgs e)
    {
        bigImage_image.Source = new BitmapImage(new Uri("pack://application:,,,/images/slug.jpg"));
        bigText_text.Text = "Слизняк";
        bigAbout_text.Text = "Скорость слизняка = 0.01 км/ч";
    }

    private void horse_button_Click(object sender, RoutedEventArgs e)
    {
        bigImage_image.Source = new BitmapImage(new Uri("pack://application:,,,/images/horse.png"));
        bigText_text.Text = "Лошадь";
        bigAbout_text.Text = "Скорость лошади = 15 км/ч";
    }

    private void sloth_button_Click(object sender, RoutedEventArgs e)
    {
        bigImage_image.Source = new BitmapImage(new Uri("pack://application:,,,/images/sloth.jpg"));
        bigText_text.Text = "Ленивец";
        bigAbout_text.Text = "Скорость ленивца = 0.12 км/ч";
    }

    private void capybara_button_Click(object sender, RoutedEventArgs e)
    {
        bigImage_image.Source = new BitmapImage(new Uri("pack://application:,,,/images/capybara.jpg"));
        bigText_text.Text = "Капибара";
        bigAbout_text.Text = "Скорость капибары = 35 км/ч";
    }

    private void jaguar_button_Click(object sender, RoutedEventArgs e)
    {
        bigImage_image.Source = new BitmapImage(new Uri("pack://application:,,,/images/jaguar.jpg"));
        bigText_text.Text = "Ягуар";
        bigAbout_text.Text = "Скорость ягуара = 80 км/ч";
    }

    private void worm_button_Click(object sender, RoutedEventArgs e)
    {
        bigImage_image.Source = new BitmapImage(new Uri("pack://application:,,,/images/worm.jpg"));
        bigText_text.Text = "Червь";
        bigAbout_text.Text = "Скорость червя = 0.03 км/ч";
    }

    private void bus_button_Click(object sender, RoutedEventArgs e)
    {
        bigImage_image.Source = new BitmapImage(new Uri("pack://application:,,,/images/bus.jpg"));
        bigText_text.Text = "Автобус";
        bigAbout_text.Text = "Длина автобуса = 10 метров";
    }

    private void pair_button_Click(object sender, RoutedEventArgs e)
    {
        bigImage_image.Source = new BitmapImage(new Uri("pack://application:,,,/images/pair-of-havaianas.jpg"));
        bigText_text.Text = "Гавайские тапки";
        bigAbout_text.Text = "Длина тапок = 0.245 метра";
    }

    private void aibus_button_Click(object sender, RoutedEventArgs e)
    {
        bigImage_image.Source = new BitmapImage(new Uri("pack://application:,,,/images/airbus-a380.jpg"));
        bigText_text.Text = "Самолет А380";
        bigAbout_text.Text = "Длина самолета = 73 метра";
    }

    private void footballField_button_Click(object sender, RoutedEventArgs e)
    {
        bigImage_image.Source = new BitmapImage(new Uri("pack://application:,,,/images/football-field.jpg"));
        bigText_text.Text = "Футбольное поле";
        bigAbout_text.Text = "Длина футбольного поля = 205 метров";
    }

    private void ronaldinho_button_Click(object sender, RoutedEventArgs e)
    {
        bigImage_image.Source = new BitmapImage(new Uri("pack://application:,,,/images/ronaldinho.jpg"));
        bigText_text.Text = "Рональдинио";
        bigAbout_text.Text = "Высота Рональдинио = 1.81 метра";
    }
}
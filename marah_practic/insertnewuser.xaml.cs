﻿using System.Windows;
using System.Windows.Threading;

namespace marah_practic;

public partial class insertnewuser : Window
{
    private readonly DateTime dateOfStart = new(2024, 11, 02, 18, 0, 0);
    private readonly DispatcherTimer timer; 

    public insertnewuser()
    {
        InitializeComponent();
        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1); 
        timer.Tick += timer_Tick; 
        timer.Start(); 
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

    private void back_button_Click(object sender, RoutedEventArgs e) { }
}
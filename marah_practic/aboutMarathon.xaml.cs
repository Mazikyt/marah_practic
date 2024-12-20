﻿using System.Windows;

namespace marah_practic;

public partial class aboutMarathon {
    public aboutMarathon() { InitializeComponent(); }
    private void back_button_Click(object sender, RoutedEventArgs e) {
        var dopInfo = new DopInfo();
        dopInfo.Top = Top;
        dopInfo.Left = Left;
        dopInfo.Show();
        Close();
    }
    private void interactMap_button_Click(object sender, RoutedEventArgs e) {
        var interactMap = new InteractMap();
        interactMap.Top = Top;
        interactMap.Left = Left;
        interactMap.Show();
        Close();
    }
}
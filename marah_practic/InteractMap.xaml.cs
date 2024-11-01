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

namespace marah_practic
{
    /// <summary>
    /// Логика взаимодействия для InteractMap.xaml
    /// </summary>
    public partial class InteractMap : Window
    {
        public InteractMap()
        {
            InitializeComponent();
            hideAllGroups();
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            aboutMarathon aboutMarathon = new aboutMarathon();
            aboutMarathon.Top = this.Top;
            aboutMarathon.Left = this.Left;
            aboutMarathon.Show();
            this.Close();
        }

        public void hideAllGroups()
        {
            check1_groupBox.Visibility = Visibility.Hidden;
            check2_groupBox.Visibility = Visibility.Hidden;
            check3_groupBox.Visibility = Visibility.Hidden;
            check4_groupBox.Visibility = Visibility.Hidden;
            check5_groupBox.Visibility = Visibility.Hidden;
            check6_groupBox.Visibility = Visibility.Hidden;
            check7_groupBox.Visibility = Visibility.Hidden;
            check8_groupBox.Visibility = Visibility.Hidden;
        }

        private void check1_button_Click(object sender, RoutedEventArgs e)
        {
            hideAllGroups();
            check1_groupBox.Visibility = Visibility.Visible;
        }

        private void check2_button_Click(object sender, RoutedEventArgs e)
        {
            hideAllGroups();
            check2_groupBox.Visibility = Visibility.Visible;
        }

        private void check3_button_Click(object sender, RoutedEventArgs e)
        {
            hideAllGroups();
            check3_groupBox.Visibility = Visibility.Visible;
        }

        private void check4_button_Click(object sender, RoutedEventArgs e)
        {
            hideAllGroups();
            check4_groupBox.Visibility = Visibility.Visible;
        }

        private void check5_button_Click(object sender, RoutedEventArgs e)
        {
            hideAllGroups();
            check5_groupBox.Visibility = Visibility.Visible;
        }

        private void check6_button_Click(object sender, RoutedEventArgs e)
        {
            hideAllGroups();
            check6_groupBox.Visibility = Visibility.Visible;
        }

        private void check7_button_Click(object sender, RoutedEventArgs e)
        {
            hideAllGroups();
            check7_groupBox.Visibility = Visibility.Visible;
        }

        private void check8_button_Click(object sender, RoutedEventArgs e)
        {
            hideAllGroups();
            check8_groupBox.Visibility = Visibility.Visible;
        }

        private void hide_button_Click(object sender, RoutedEventArgs e)
        {
            hideAllGroups();
        }
    }
}

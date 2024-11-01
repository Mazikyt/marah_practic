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
    /// Логика взаимодействия для DopInfo.xaml
    /// </summary>
    public partial class DopInfo : Window
    {
        public DopInfo()
        {
            InitializeComponent();
        }

        private void logout_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Left = this.Left;
            mainWindow.Top = this.Top;
            mainWindow.Show();
            this.Close();
        }

        private void aboutMarathon_button_Click(object sender, RoutedEventArgs e)
        {
            aboutMarathon aboutMarathon = new aboutMarathon();
            aboutMarathon.Left = this.Left;
            aboutMarathon.Top = this.Top;
            aboutMarathon.Show();
            this.Close();
        }

        private void howLongMarathon_Click(object sender, RoutedEventArgs e)
        {
            howLongMarathon howLongMarathon = new howLongMarathon();
            howLongMarathon.Top = this.Top;
            howLongMarathon.Left = this.Left;
            howLongMarathon.Show();
            this.Close();
        }

        private void prevResults_button_Click(object sender, RoutedEventArgs e)
        {
            prevResults prevResults = new prevResults();
            prevResults.Left = this.Left;
            prevResults.Top = this.Top;
            prevResults.Show();
            this.Close();
        }

        private void allCharities_button_Click(object sender, RoutedEventArgs e)
        {
            allCahrities allCahrities = new allCahrities();
            allCahrities.Left = this.Left;
            allCahrities.Top = this.Top;
            allCahrities.Show();
            this.Close();
        }
    }
}

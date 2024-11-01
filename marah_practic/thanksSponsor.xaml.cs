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
    /// Логика взаимодействия для thanksSponsor.xaml
    /// </summary>
    public partial class thanksSponsor : Window
    {
        public thanksSponsor(string runnerInfo, string charityName, int amount)
        {
            InitializeComponent();
            runnerInfo_text.Text = runnerInfo;
            charityName_text.Text= charityName;
            amount_text.Text = "$" + amount.ToString();
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Left = this.Left;
            mainWindow.Top = this.Top;
            mainWindow.Show();
            this.Close();
        }
    }
}

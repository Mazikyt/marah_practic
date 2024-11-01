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
    /// Логика взаимодействия для menurunner.xaml
    /// </summary>
    public partial class menurunner : Window
    {
        static int cur_user_id;

        public menurunner(int user_id)
        {
            InitializeComponent();
            ContactPage.Visibility = Visibility.Hidden;
            cur_user_id = user_id;
        }

        private void registrOnMa_button_Click(object sender, RoutedEventArgs e)
        {
            registrationonma registrationonma = new registrationonma(cur_user_id);
            registrationonma.Left = this.Left;
            registrationonma.Top = this.Top;
            registrationonma.Show();
            this.Close();
        }

        private void myResults_button_Click(object sender, RoutedEventArgs e)
        {
            reziltrunner reziltrunner = new reziltrunner();
            reziltrunner.Left = this.Left;
            reziltrunner.Top = this.Top;
            reziltrunner.Show();
            this.Close();
        }

        private void logout_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Left = this.Left;
            mainWindow.Top = this.Top;
            mainWindow.Show();
            this.Close();
        }

        private void runnerProfileRedact_button_Click(object sender, RoutedEventArgs e)
        {
            runnerProfileRedact runnerProfileRedact = new runnerProfileRedact(cur_user_id);
            runnerProfileRedact.Left = this.Left;
            runnerProfileRedact.Top = this.Top;
            runnerProfileRedact.Show();
            this.Close();
        }
        private void Contacts_button_Click(object sender, RoutedEventArgs e)
        {
            ContactPage.Visibility = Visibility.Visible;
        }

        private void CloseContactPage_Click(object sender, RoutedEventArgs e)
        {
            ContactPage.Visibility = Visibility.Hidden;
        }


        private void runnersSponsors_button_Click(object sender, RoutedEventArgs e)
        {
            runnersSponsors runnersSponsors = new runnersSponsors(cur_user_id);
            runnersSponsors.Left = this.Left;
            runnersSponsors.Top = this.Top;
            runnersSponsors.Show();
            this.Close();
        }
    }
}

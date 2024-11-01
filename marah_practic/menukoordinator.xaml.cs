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
    /// Логика взаимодействия для menukoordinator.xaml
    /// </summary>
    public partial class menukoordinator : Window
    {
        static int cur_user_id;

        public menukoordinator(int user_id)
        {
            InitializeComponent();
            cur_user_id = user_id;
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Left = this.Left;
            mainWindow.Top = this.Top;
            mainWindow.Show();
            this.Close();
        }

        private void runnersUprav_button_Click(object sender, RoutedEventArgs e)
        {
            runneruprav runneruprav = new runneruprav();
            runneruprav.Left = this.Left;
            runneruprav.Top = this.Top;
            runneruprav.Show();
            this.Close();
        }
    }
}

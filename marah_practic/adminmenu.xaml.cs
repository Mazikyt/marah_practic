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
    /// Логика взаимодействия для adminmenu.xaml
    /// </summary>
    public partial class adminmenu : Window
    {
        private int cur_user_id;

        public adminmenu()
        {
            InitializeComponent();
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Left = this.Left;
            mainWindow.Top = this.Top;
            mainWindow.Show();
            this.Close();
        }

        private void volonteuprav_button_Click(object sender, RoutedEventArgs e)
        {
            upravvolonter upravvolonter = new upravvolonter();
            upravvolonter.Left = this.Left;
            upravvolonter.Top = this.Top;
            upravvolonter.Show();
            this.Close();
        }

        private void user_adminButton_Click(object sender, RoutedEventArgs e)
        {
            upravusersadmin upravusersadmin = new upravusersadmin();
            upravusersadmin.Left = this.Left;
            upravusersadmin.Top = this.Top;
            upravusersadmin.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

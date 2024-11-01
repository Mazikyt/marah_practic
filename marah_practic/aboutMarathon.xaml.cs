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
    /// Логика взаимодействия для aboutMarathon.xaml
    /// </summary>
    public partial class aboutMarathon : Window
    {
        public aboutMarathon()
        {
            InitializeComponent();
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            DopInfo dopInfo = new DopInfo();
            dopInfo.Top = this.Top;
            dopInfo.Left = this.Left;
            dopInfo.Show();
            this.Close();
        }

        private void interactMap_button_Click(object sender, RoutedEventArgs e)
        {
            InteractMap interactMap = new InteractMap();
            interactMap.Top = this.Top;
            interactMap.Left = this.Left;
            interactMap.Show();
            this.Close();
        }
    }
}

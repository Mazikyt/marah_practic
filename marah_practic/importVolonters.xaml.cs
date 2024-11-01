using Microsoft.Win32;
using Npgsql;
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
    /// Логика взаимодействия для importVolonters.xaml
    /// </summary>
    public partial class importVolonters : Window
    {
        private static string connStr = "Host=Localhost;Username=postgres;Password=1204;Database=wss";

        private static NpgsqlConnection conn = new NpgsqlConnection(connStr);

        string filePath = "";
        public importVolonters()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
            InitializeComponent();
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            upravvolonter upravvolonter = new upravvolonter();
            upravvolonter.Left = this.Left;
            upravvolonter.Top = this.Top;
            upravvolonter.Show();
            this.Close();
        }

        private void importVolonters_button_Click(object sender, RoutedEventArgs e)
        {
            string copyCommand = $@"
            COPY users (last_name, first_name, country_id, gender_id, role_id)
            FROM '{filePath}'
            DELIMITER '|'
            CSV HEADER;";

            var cmd = new NpgsqlCommand(copyCommand, conn);
            cmd.ExecuteNonQuery();
        }

        private void insertVolonters_button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.Title = "Выберите CSV файл";

            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;

            }

            filePath_text.Text = filePath;

        }

        private void logout_button_Click(object sender, RoutedEventArgs e)
        {
            adminmenu adminmenu = new adminmenu();
            adminmenu.Left = this.Left;
            adminmenu.Top = this.Top;
            adminmenu.Show();
            this.Close();
        }
    }
}

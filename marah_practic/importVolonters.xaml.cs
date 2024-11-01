using System.Data;
using System.Windows;
using Microsoft.Win32;
using Npgsql;

namespace marah_practic;

/// <summary>
///     Логика взаимодействия для importVolonters.xaml
/// </summary>
public partial class importVolonters : Window
{
    private static readonly string connStr = "Host=84.21.173.156:5435;Username=postgres;Password=DBDfJN3Vz9;Database=wss";

    private static readonly NpgsqlConnection conn = new(connStr);

    private string filePath = "";

    public importVolonters()
    {
        if (conn.State == ConnectionState.Closed) conn.Open();
        InitializeComponent();
    }

    private void back_button_Click(object sender, RoutedEventArgs e)
    {
        var upravvolonter = new upravvolonter();
        upravvolonter.Left = Left;
        upravvolonter.Top = Top;
        upravvolonter.Show();
        Close();
    }

    private void importVolonters_button_Click(object sender, RoutedEventArgs e)
    {
        var copyCommand = $@"
            COPY users (last_name, first_name, country_id, gender_id, role_id)
            FROM '{filePath}'
            DELIMITER '|'
            CSV HEADER;";

        var cmd = new NpgsqlCommand(copyCommand, conn);
        cmd.ExecuteNonQuery();
    }

    private void insertVolonters_button_Click(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog();

        openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
        openFileDialog.Title = "Выберите CSV файл";

        if (openFileDialog.ShowDialog() == true) filePath = openFileDialog.FileName;

        filePath_text.Text = filePath;
    }

    private void logout_button_Click(object sender, RoutedEventArgs e)
    {
        var adminmenu = new adminmenu();
        adminmenu.Left = Left;
        adminmenu.Top = Top;
        adminmenu.Show();
        Close();
    }
}
using System.Data;
using System.Windows;
using Npgsql;

namespace marah_practic;

/// <summary>
///     Логика взаимодействия для upravvolonter.xaml
/// </summary>
public partial class upravvolonter : Window
{
    private static readonly string connStr = "Host=84.21.173.156:5435;Username=postgres;Password=DBDfJN3Vz9;Database=wss";

    private static readonly NpgsqlConnection conn = new(connStr);

    public upravvolonter()
    {
        if (conn.State == ConnectionState.Closed) conn.Open();
        InitializeComponent();

        var refresh_comboBox = new List<string>();
        refresh_comboBox.Add("Фамилия");
        refresh_comboBox.Add("Имя");
        refresh_comboBox.Add("Страна");
        refresh_comboBox.Add("Пол");

        sort_comboBox.ItemsSource = refresh_comboBox;
    }

    private void back_button_Click(object sender, RoutedEventArgs e)
    {
        var adminmenu = new adminmenu();
    }

    private void refresh_button_Click(object sender, RoutedEventArgs e)
    {
        var volontersCount = 0;

        var select_string = "select u.last_name, u.first_name, c.name, g.name from users u " +
                            "join countries c ON c.id = u.country_id " +
                            "join genders g ON g.id = u.gender_id " +
                            "where role_id = 5 ";

        if (sort_comboBox.Text != "")
        {
            if (sort_comboBox.Text == "Фамилия")
                select_string = select_string + "order by u.last_name";
            else if (sort_comboBox.Text == "Имя")
                select_string = select_string + "order by u.first_name";
            else if (sort_comboBox.Text == "Страна")
                select_string = select_string + "order by 3";
            else if (sort_comboBox.Text == "Пол") select_string = select_string + "order by 4";
        }

        var volonters = new List<Volonter>();

        var cmd = new NpgsqlCommand(select_string, conn);
        var reader = cmd.ExecuteReader();
        if (reader.HasRows)
            while (reader.Read())
            {
                volonters.Add(new Volonter(reader.GetString(0), reader.GetString(1), reader.GetString(2),
                    reader.GetString(3)));
                volontersCount++;
            }

        reader.Close();

        volonterCount_text.Text = volontersCount.ToString();

        var ListView = volonters.Select(x => new
        {
            x.last_name,
            x.first_name,
            x.country,
            x.gender
        }).ToList();

        volonters_ListView.ItemsSource = ListView;
    }

    private void importVolonters_button_Click(object sender, RoutedEventArgs e)
    {
        var importVolonters = new importVolonters();
        importVolonters.Left = Left;
        importVolonters.Top = Top;
        importVolonters.Show();
        Close();
    }

    private class Volonter
    {
        public readonly string country;
        public readonly string first_name;
        public readonly string gender;
        public readonly string last_name;

        public Volonter(string last_name, string first_name, string country, string gender)
        {
            this.last_name = last_name;
            this.first_name = first_name;
            this.country = country;
            this.gender = gender;
        }
    }
}
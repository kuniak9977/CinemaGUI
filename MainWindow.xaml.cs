using Cinema;
using Cinema.Controllers;
using Cinema.Models;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CinemaManagerGUI
{
    public partial class MainWindow : Window
    {
        string path = "CinemaDB.json";
        Database database;

        public MainWindow()
        {
            InitializeComponent();
            database = LoadOrCreateDatabase(path);
            //database.AddEmployee("Jan", "Kowalski", 1234, (int)Employee.Occupation.Kierownik);
            //database.FilmsList.Clear();
           //Room room = new Room("Sala 121", 200);
            //database.AddRoom(room);

            //this.Closing += MainWindow_Closing;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string userCode = UserCode.Text;

            if (ValidateUserCode(userCode))
            {
                Manager manager = new Manager(database);
                manager.Show();
                this.Close();
            }
            else
                MessageBox.Show("Niepoprawny kod. Spróbuj ponownie.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        private bool ValidateUserCode(string code)
        {
            // Sprawdź, czy kod jest liczbą
            if (!short.TryParse(code, out short parsedCode))
                return false;

            // Sprawdź, czy kod istnieje w liście pracowników
            return database.EmployeeList.Any(e => e.EmployeePrivateCode == parsedCode);
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            // Ścieżka do pliku JSON, w którym zapiszesz bazę danych

            try
            {
                SaveDatabase(database, path);
                MessageBox.Show("Baza danych została zapisana pomyślnie.", "Zapis danych", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas zapisywania bazy danych: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        Database LoadOrCreateDatabase(string _path)
        {
            if (!File.Exists(_path))
            {
                return new Database();
            }

            string json = File.ReadAllText(_path);
            var option = new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<Database>(json, option);
        }

        private void SaveDatabase(Database _database, string _path)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNameCaseInsensitive = true
            };

            string json = JsonSerializer.Serialize(_database, options);
            File.WriteAllText(_path, json);
        }
    }
}
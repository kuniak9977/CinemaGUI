using Cinema;
using Cinema.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace CinemaManagerGUI
{
    public partial class Manager : Window
    {
        private Database database;
        private Film selectedFilm;
        private Room selectedRoom;
        private Employee selectedEmployee;

        public Manager(Database _database)
        {
            InitializeComponent();
            database = _database;

            LoadFilmsComboBox();
            LoadRoomsComboBox();
            LoadEmployeeListBox();
        }

        // Wczytaj filmy do ListBox
        private void LoadFilmsComboBox()
        {
            FilmListBox.ItemsSource = null;
            FilmListBox.ItemsSource = database.FilmsList;
            FilmListBox.DisplayMemberPath = "Name";
        }

        // Wczytaj sale kinowe do ComboBox
        private void LoadRoomsComboBox()
        {
            RoomsComboBox.ItemsSource = database.RoomList;
            RoomsComboBox.DisplayMemberPath = "Name";
        }

        // Wczytaj pracowników do ListBox
        private void LoadEmployeeListBox()
        {
            EmployeeListBox.ItemsSource = database.EmployeeList.OrderBy(e => e.Role).ToList();
            EmployeeListBox.DisplayMemberPath = "FullName";
        }

        // Dodanie nowego filmu
        private void AddFilm_Click(object sender, RoutedEventArgs e)
        {
            AddFilmDialog addFilmDialog = new AddFilmDialog();

            if (addFilmDialog.ShowDialog() == true)
            {
                Film newFilm = addFilmDialog.NewFilm;
                database.AddFilm(newFilm);
                LoadFilmsComboBox();
            }
        }

        // Usunięcie filmu
        private void RemoveFilm_Click(object sender, RoutedEventArgs e)
        {
            if (selectedFilm != null)
            {
                database.RemoveFilm(selectedFilm);
                LoadFilmsComboBox();
            }
        }

        private void ViewFilms_Click(object sender, RoutedEventArgs e)
        {
            // Możesz rozszerzyć tę metodę, aby wyświetlała pełne dane filmu
            string filmsList = string.Join("\n", database.FilmsList.Select(f => f.ToString(f.LengthSec)));
            MessageBox.Show(filmsList);
        }

        // Przypisanie filmu do sali
        private void AssignFilmToRoom_Click(object sender, RoutedEventArgs e)
        {
            selectedFilm = (Film)FilmListBox.SelectedItem;
            selectedRoom = (Room)RoomsComboBox.SelectedItem;

            if (selectedFilm != null && selectedRoom != null)
            {
                database.AddMovieToRoom(selectedRoom, selectedFilm, DateTime.Now);
                MessageBox.Show($"Film '{selectedFilm.Name}' został przypisany do sali '{selectedRoom.Name}'.");
            }
            else
            {
                MessageBox.Show("Wybierz film i salę przed przypisaniem.");
            }
        }

        // Wyświetlanie szczegółów sali
        private void ViewRoomDetails_Click(object sender, RoutedEventArgs e)
        {
            selectedRoom = (Room)RoomsComboBox.SelectedItem;

            if (selectedRoom != null)
            {
                StringBuilder roomDetails = new StringBuilder();
                roomDetails.AppendLine($"Sala: {selectedRoom.Name}");
                roomDetails.AppendLine($"Liczba miejsc: {selectedRoom.ChairsQuantity}");
                roomDetails.AppendLine("Status miejsc:");
                for (int i = 0; i < selectedRoom.States.Length; i++)
                {
                    roomDetails.AppendLine($"Miejsce {i + 1}: {selectedRoom.States[i]}");
                }
                MessageBox.Show(roomDetails.ToString(), "Szczegóły sali");
            }
            else
            {
                MessageBox.Show("Wybierz salę, aby zobaczyć szczegóły.");
            }
        }

        // Dodanie nowego pracownika
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeDialog addEmployeeDialog = new AddEmployeeDialog();

            if (addEmployeeDialog.ShowDialog() == true)
            {
                Employee newEmployee = addEmployeeDialog.NewEmployee;
                database.AddEmployee(newEmployee.Name, newEmployee.Surname, newEmployee.EmployeePrivateCode, (int)newEmployee.Role);
                LoadEmployeeListBox();
            }
        }

        // Przypisanie podwładnego do pracownika
        private void AssignSubordinate_Click(object sender, RoutedEventArgs e)
        {
            AssignSubordinateDialog assignSubordinateDialog = new AssignSubordinateDialog(database);
            assignSubordinateDialog.ShowDialog();
        }

        // Modyfikowanie danych pracownika
        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            EditEmployeeDialog editEmployeeDialog = new EditEmployeeDialog(selectedEmployee);
            bool? result = editEmployeeDialog.ShowDialog();

            if (result == true)
            {
                LoadEmployeeListBox();
            }
        }

        // Wyświetlanie pracowników
        private void ViewEmployees_Click(object sender, RoutedEventArgs e)
        {
            string employeeList = string.Join("\n", database.EmployeeList.OrderBy(e => e.Role).Select(e => $"{e.Name} {e.Surname} ({e.Role})"));
            MessageBox.Show(employeeList);
        }

        // Ustawienie wybranego filmu
        private void FilmListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedFilm = (Film)FilmListBox.SelectedItem;
        }

        // Ustawienie wybranego pracownika
        private void EmployeeListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedEmployee = (Employee)EmployeeListBox.SelectedItem;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string _path = "CinemaDB.json";
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNameCaseInsensitive = true
            };

            string json = JsonSerializer.Serialize(database, options);
            File.WriteAllText(_path, json);
        }
    }
}

using Cinema;
using Cinema.Models;
using System;
using System.Linq;
using System.Windows;

namespace CinemaManagerGUI
{
    public partial class AssignSubordinateDialog : Window
    {
        private Employee _selectedManager;
        private Employee _selectedSubordinate;
        private Database database;

        public AssignSubordinateDialog(Database _database)
        {
            database = _database;

            InitializeComponent();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            // Wczytujemy pracowników do lewej listy (wszyscy pracownicy)
            EmployeeListBox.ItemsSource = database.EmployeeList;

            // Wczytujemy pracowników, którzy nie mają przypisanych podwładnych
            AvailableEmployeeListBox.ItemsSource = database.EmployeeList
            .Where(e =>
                !database.EmployeeList.Any(mgr => mgr.SubordinateIds.Contains(e.Id))
            ).ToList();

        }

        // Wybór menedżera
        private void EmployeeListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedManager = (Employee)EmployeeListBox.SelectedItem;

            if (_selectedManager != null)
            {
                // Wyświetl pracowników, którzy znajdują się na liście podwładnych wybranego menedżera
                SubordinateListBox.ItemsSource = database.EmployeeList
                    .Where(e => _selectedManager.SubordinateIds.Contains(e.Id))
                    .ToList();
            }
        }

        // Przypisanie podwładnego
        private void AssignSubordinateButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedSubordinate = (Employee)AvailableEmployeeListBox.SelectedItem;

            if (_selectedSubordinate != null && _selectedManager != null)
            {
                // Dodajemy podwładnego do menedżera
                _selectedManager.AddSubordinate(_selectedSubordinate.Id);

                // Odświeżamy listy
                LoadEmployees();

                MessageBox.Show($"Pracownik {_selectedSubordinate.Name} został przypisany do {_selectedManager.Name}.");
            }
            else
            {
                MessageBox.Show("Wybierz menedżera i pracownika do przypisania.");
            }
        }

        // Usunięcie podwładnego
        private void RemoveSubordinateButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedManager != null)
            {
                _selectedSubordinate = (Employee)SubordinateListBox.SelectedItem;

                if (_selectedSubordinate != null)
                {
                    // Usuwamy podwładnego z menedżera
                    _selectedManager.SubordinateIds.Remove(_selectedSubordinate.Id);

                    // Odświeżamy listy
                    LoadEmployees();

                    MessageBox.Show($"Podwładny {_selectedSubordinate.Name} został usunięty.");
                }
                else
                {
                    MessageBox.Show("Wybierz podwładnego do usunięcia.");
                }
            }
            else
            {
                MessageBox.Show("Wybierz menedżera.");
            }
        }
    }
}

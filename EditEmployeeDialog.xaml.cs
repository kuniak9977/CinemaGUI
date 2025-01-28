using System;
using System.Windows;
using Cinema.Models;

namespace CinemaManagerGUI
{
    public partial class EditEmployeeDialog : Window
    {
        private Employee _employeeToEdit;

        public EditEmployeeDialog(Employee employee)
        {
            InitializeComponent();
            _employeeToEdit = employee;

            // Inicjalizacja pól z danymi pracownika
            EmployeeNameTextBox.Text = _employeeToEdit.Name;
            EmployeeSurnameTextBox.Text = _employeeToEdit.Surname;
            EmployeeCodeTextBox.Text = _employeeToEdit.EmployeePrivateCode.ToString();
            EmployeeRoleComboBox.SelectedIndex = (int)_employeeToEdit.Role;  // Ustawienie roli pracownika w ComboBox
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Pobranie danych z pól tekstowych
            _employeeToEdit.Name = EmployeeNameTextBox.Text;
            _employeeToEdit.Surname = EmployeeSurnameTextBox.Text;
            _employeeToEdit.EmployeePrivateCode = short.Parse(EmployeeCodeTextBox.Text);
            _employeeToEdit.Role = (Employee.Occupation)EmployeeRoleComboBox.SelectedIndex;

            // Normalizacja imienia i nazwiska
            _employeeToEdit.NormalizedName = _employeeToEdit.NormalizeName(_employeeToEdit.Name, _employeeToEdit.Surname);

            // Zamykamy okno po zatwierdzeniu zmian
            DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}

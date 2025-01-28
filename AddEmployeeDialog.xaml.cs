using Cinema.Models;
using System;
using System.Windows;

namespace CinemaManagerGUI
{
    public partial class AddEmployeeDialog : Window
    {
        public Employee NewEmployee { get; private set; }

        public AddEmployeeDialog()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string name = EmployeeNameTextBox.Text;
            string surname = EmployeeSurnameTextBox.Text;
            short code = short.Parse(EmployeeCodeTextBox.Text);

            // Używamy metody statycznej ConvertFromInt z klasy Employee
            var role = Employee.ConvertFromInt(EmployeeRoleComboBox.SelectedIndex); // Zmienione tutaj

            // Tworzymy nowego pracownika
            NewEmployee = new Employee(name, surname, code, (int)role);

            // Zamykamy okno
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

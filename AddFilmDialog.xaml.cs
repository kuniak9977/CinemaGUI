using Cinema.Models;
using System;
using System.Windows;

namespace CinemaManagerGUI
{
    public partial class AddFilmDialog : Window
    {
        public Film NewFilm { get; private set; }
        private int _movieLengthInSeconds = 0;

        public AddFilmDialog()
        {
            InitializeComponent();
        }

        private void SetTimeButton_Click(object sender, RoutedEventArgs e)
        {
            // Otwieramy okno SetMovieTimeForRoom i przekazujemy czas w sekundach, gdy zostanie zatwierdzone
            SetMovieTimeForRoom setMovieTimeWindow = new SetMovieTimeForRoom();
            setMovieTimeWindow.ShowDialog();

            // Pobieramy czas w sekundach z okna SetMovieTimeForRoom po jego zamknięciu
            if (setMovieTimeWindow.DialogResult == true)
            {
                _movieLengthInSeconds = setMovieTimeWindow.SelectedTimeInSeconds;
                FilmLengthTextBox.Text = _movieLengthInSeconds.ToString(); // Wyświetlamy czas w sekundach w TextBox
            }
        }
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Pobranie danych z pól tekstowych
            string name = FilmNameTextBox.Text;
            string description = FilmDescriptionTextBox.Text;
            string type = FilmTypeTextBox.Text;
            int lengthSec = int.TryParse(FilmLengthTextBox.Text, out int tempLength) ? tempLength : 0;
            double points = double.TryParse(FilmPointsTextBox.Text, out double tempPoints) ? tempPoints : 0.0;

            // Utworzenie nowego filmu
            NewFilm = new Film(name, description, type, lengthSec, points);

            // Zamknięcie okna
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

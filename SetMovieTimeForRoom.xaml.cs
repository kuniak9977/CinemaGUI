using System;
using System.Windows;

namespace CinemaManagerGUI
{
    public partial class SetMovieTimeForRoom : Window
    {
        public int SelectedTimeInSeconds { get; private set; }
        private int _hoursTens = 0;
        private int _hoursOnes = 0;
        private int _minutesTens = 0;
        private int _minutesOnes = 0;

        public SetMovieTimeForRoom()
        {
            InitializeComponent();

            // Zdarzenia kliknięć przycisków
            HoursIntButtonInc.Click += HoursIntButtonInc_Click;
            HoursDecButtonInc.Click += HoursDecButtonInc_Click;
            HoursIntButtonDec.Click += HoursIntButtonDec_Click;
            HoursDecButtonDec.Click += HoursDecButtonDec_Click;

            MinuteIntButtonInc.Click += MinuteIntButtonInc_Click;
            MinuteDecButtonInc.Click += MinuteDecButtonInc_Click;
            MinuteIntButtonDec.Click += MinuteIntButtonDec_Click;
            MinuteDecButtonDec.Click += MinuteDecButtonDec_Click;

            Confirm.Click += Confirm_Click;
        }

        // Zwiększenie dziesiątek godzin
        private void HoursIntButtonInc_Click(object sender, RoutedEventArgs e)
        {
            if (_hoursTens < 9)
            {
                _hoursTens++;
                HoursDecimal.Text = _hoursTens.ToString();
            }
        }

        // Zmniejszenie dziesiątek godzin
        private void HoursDecButtonInc_Click(object sender, RoutedEventArgs e)
        {
            if (_hoursTens > 0)
            {
                _hoursTens--;
                HoursDecimal.Text = _hoursTens.ToString();
            }
        }

        // Zwiększenie jednostek godzin
        private void HoursIntButtonDec_Click(object sender, RoutedEventArgs e)
        {
            if (_hoursOnes < 9)
            {
                _hoursOnes++;
                HoursSingle.Text = _hoursOnes.ToString();
            }
        }

        // Zmniejszenie jednostek godzin
        private void HoursDecButtonDec_Click(object sender, RoutedEventArgs e)
        {
            if (_hoursOnes > 0)
            {
                _hoursOnes--;
                HoursSingle.Text = _hoursOnes.ToString();
            }
        }

        // Zwiększenie dziesiątek minut
        private void MinuteIntButtonInc_Click(object sender, RoutedEventArgs e)
        {
            if (_minutesTens < 5)
            {
                _minutesTens++;
                MinutesDecimal.Text = _minutesTens.ToString();
            }
        }

        // Zmniejszenie dziesiątek minut
        private void MinuteDecButtonInc_Click(object sender, RoutedEventArgs e)
        {
            if (_minutesTens > 0)
            {
                _minutesTens--;
                MinutesDecimal.Text = _minutesTens.ToString();
            }
        }

        // Zwiększenie jednostek minut
        private void MinuteIntButtonDec_Click(object sender, RoutedEventArgs e)
        {
            if (_minutesOnes < 9)
            {
                _minutesOnes++;
                MinutesSingle.Text = _minutesOnes.ToString();
            }
        }

        // Zmniejszenie jednostek minut
        private void MinuteDecButtonDec_Click(object sender, RoutedEventArgs e)
        {
            if (_minutesOnes > 0)
            {
                _minutesOnes--;
                MinutesSingle.Text = _minutesOnes.ToString();
            }
        }

        // Zatwierdzenie wartości i obliczenie czasu w sekundach
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            // Obliczamy czas w sekundach
            int totalHours = _hoursTens * 10 + _hoursOnes;
            int totalMinutes = _minutesTens * 10 + _minutesOnes;
            SelectedTimeInSeconds = (totalHours * 3600) + (totalMinutes * 60);

            // Zatwierdzamy wynik i zamykamy okno
            DialogResult = true;
            this.Close();
        }
    }
}

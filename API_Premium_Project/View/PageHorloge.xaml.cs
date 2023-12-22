using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace API_Premium_Project.View
{
    /// <summary>
    /// Logique d'interaction pour PageHorloge.xaml
    /// </summary>
    public partial class PageHorloge : UserControl
    {
        private const string ApiUrl = "http://worldtimeapi.org/api/timezone/Europe/Paris";
        public PageHorloge()
        {
            InitializeComponent();

            // Démarrez une minuterie pour mettre à jour l'horloge chaque seconde
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            // Appelez l'API pour la première fois au chargement de la fenêtre
            MiseAJourHorloge().GetAwaiter().GetResult();
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            // Mettez à jour le contenu du TextBlock avec l'heure actuelle
            await MiseAJourHorloge();
        }

        private async Task MiseAJourHorloge()
        {
            try
            {
                // Utilisation de l'API WorldTimeAPI pour obtenir l'heure actuelle
                HttpClient client = new HttpClient();
                string jsonResponse = await client.GetStringAsync(ApiUrl);

                // Analyse du JSON pour extraire l'heure
                DateTime currentTime = ParseJsonResponse(jsonResponse);

                // Mettez à jour les lignes de l'horloge en fonction de l'heure actuelle
                MiseAJourAiguilles(currentTime.Hour, currentTime.Minute, currentTime.Second);
            }
            catch (Exception ex)
            {
                // Gestion des erreurs
                MessageBox.Show($"Erreur lors de la mise à jour de l'horloge : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private DateTime ParseJsonResponse(string jsonResponse)
        {
            // Vous devrez utiliser une bibliothèque de sérialisation JSON, par exemple, System.Text.Json
            // Dans cet exemple simple, nous utilisons simplement une recherche de sous-chaîne
            int startIndex = jsonResponse.IndexOf("\"datetime\":\"") + "\"datetime\":\"".Length;
            int endIndex = jsonResponse.IndexOf("\"", startIndex);
            string dateTimeString = jsonResponse.Substring(startIndex, endIndex - startIndex);

            return DateTime.Parse(dateTimeString);
        }

        private void MiseAJourAiguilles(int heures, int minutes, int secondes)
        {
            // Mettez à jour l'aiguille des heures
            double angleHeures = (heures % 12 + minutes / 60.0) * 30; // Chaque heure représente un angle de 30 degrés
            RotationAiguille(AiguilleHeures, angleHeures);

            // Mettez à jour l'aiguille des minutes
            double angleMinutes = minutes * 6; // Chaque minute représente un angle de 6 degrés
            RotationAiguille(AiguilleMinutes, angleMinutes);

            // Mettez à jour l'aiguille des secondes
            double angleSecondes = secondes * 6; // Chaque seconde représente un angle de 6 degrés
            RotationAiguille(AiguilleSecondes, angleSecondes);
        }

        private void RotationAiguille(Line aiguille, double angle)
        {
            aiguille.RenderTransform = new RotateTransform(angle, aiguille.ActualWidth / 2, aiguille.ActualHeight / 2);
        }
    }
}
